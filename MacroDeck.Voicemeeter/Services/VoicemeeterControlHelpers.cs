using Microsoft.Win32;
using System;
using System.IO;

namespace PW.MacroDeck.VoicemeeterPlugin.Services
{
    internal static class VoicemeeterControlHelpers
    {
        public static string ErrorStr = "Communication Error";

        private static string DllName
        {
            get
            {
                string str = "VoicemeeterRemote";
                if (Environment.Is64BitProcess)
                {
                    str += "64";
                }

                return str + ".dll";
            }
        }

        //
        // Exceptions:
        //   T:System.IO.DirectoryNotFoundException:
        //     Thrown when cannot find Voicemeeter registry key
        //
        //   T:System.Security.SecurityException:
        //
        //   T:System.IO.IOException:
        //
        //   T:System.ArgumentException:
        //
        //   T:System.IO.PathTooLongException:
        private static string GetProgramFolder()
        {
            const string regkeyHead = "HKEY_LOCAL_MACHINE\\SOFTWARE\\";
            const string regKey64 = "WOW6432Node\\";
            const string regKeyTail = "Microsoft\\Windows\\CurrentVersion\\Uninstall\\";
            const string vmKey = "VB:Voicemeeter {17359A74-1236-5467}";
            const string valueName = "UninstallString";

            string keyName = $"{regkeyHead}{regKeyTail}{vmKey}";
            object value = Registry.GetValue(keyName, valueName, null);
            if (value == null)
            {
                keyName = $"{regkeyHead}{regKey64}{regKeyTail}{vmKey}";
                value = Registry.GetValue(keyName, valueName, null);
            }
            if (value == null)
            {
                throw new Exception("Could not find Voicemeeter");
            }

            return Path.GetDirectoryName((string)value);
        }

        // Exceptions:
        //   T:System.IO.DirectoryNotFoundException:
        //     Thrown when cannot find Voicemeeter registry key
        //
        //   T:System.Security.SecurityException:
        //
        //   T:System.IO.IOException:
        //
        //   T:System.ArgumentException:
        //
        //   T:System.IO.PathTooLongException:
        public static string GetDllPath()
        {
            return Path.Combine(GetProgramFolder(), DllName);
        }

        public static void TestLogin(int loginResult, Action onSuccessfulLogin = null)
        {
            switch (loginResult)
            {
                case 1:
                    onSuccessfulLogin?.Invoke();
                    break;
                case 0:
                    break;
                case -1:
                    throw new Exception("Not installed or could not connect.");
                default:
                    throw new Exception("Unexpected connection. Connection was not correctly closed previously.");
            }
        }

        // 0: OK(no error).
        // -1: error
        // -2: no server.
        // -3: no level available
        // -4: out of range
        public static void TestLevelResult(int result)
        {
            switch (result)
            {
                case 0: return;
                case -1: throw new Exception("Error");
                case -2: throw new Exception("Not Connected");
                case -3: return;
                case -4: throw new ArgumentException("Channel out of range");
                default: throw new Exception("Unknown");
            }
        }

        //0: OK(no error).
        //-1: error
        //-2: no server.
        //-3: unknown parameter
        //-5: structure mismatch
        public static void TestResult(int result)
        {
            switch (result)
            {
                case 0: return;
                case -1: throw new Exception("Parameter Error");
                case -2: throw new Exception("Not Connected");
                case -3: throw new ArgumentException("Parameter not found");
                case -5: throw new Exception("Structure mismatch");
                default: throw new Exception("Unknown");
            }
        }
    }
}