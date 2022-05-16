using AtgDev.Voicemeeter;
using AtgDev.Voicemeeter.Types;
using PW.VoicemeeterPlugin;
using PW.VoicemeeterPlugin.Models;
using PW.VoicemeeterPlugin.Services.Voicemeeter;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Timers;

namespace PW.VoicemeeterPlugin.Services.Voicemeeter
{
    public sealed partial class Control
    {
        private bool loginCalled;
        private Timer timer;
        private string ConnectedVersion => TryGetVoicemeeterVersion(out string version) ? version : ControlHelpers.ErrorStr;
        private VoicemeeterType ConnectedType
        {
            get
            {
                if (VmrApi is null)
                {
                    return VoicemeeterType.None;
                }
                
                ControlHelpers.TestResultInfo(VmrApi.GetVoicemeeterType(out VoicemeeterType type));
                return type;
            }
        }


        private bool TryGetVoicemeeterVersion(out string version)
        {
            version = string.Empty;
            return VmrApi?.GetVoicemeeterVersion(out version) == ResultCodes.Ok;
        }

        private void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            Close();
        }

        private void Close()
        {
            Logout();
            VmrApi?.Dispose();
            timer?.Dispose();
        }

        private void Logout()
        {
            if (timer != null)
            {
                timer.Enabled = false;
                timer.Elapsed -= Poll;
            }
            VmrApi?.Logout();
        }

        private void Login()
        {
            loginCalled = true;
            ControlHelpers.TestLogin(VmrApi.Login());//, RunVoicemeeter);
        }

        //private void RunVoicemeeter(int loginResult)
        //{
        //    int runRes = VmrApi.GetVoicemeeterType(out VoicemeeterType type);

        //    if (runRes == ResultCodes.Ok && loginResult == ResultCodes.OkVmNotLaunched)
        //    {
        //        if (Config.RunVoicemeeter)
        //        {
        //            System.Threading.Thread.Sleep(100);
        //            runRes = VmrApi.RunVoicemeeter(type);
        //        }
        //    }

        //    SuchByte.MacroDeck.Logging.MacroDeckLogger.Trace(PluginInstance.Plugin, $"{nameof(RunVoicemeeter)}: Type:{runRes}, {type}, Login Result:{loginResult}");
        //    if (runRes == ResultCodes.Ok)
        //    {
        //        return;
        //    }
        //    throw new Exception("An error occurred. Manually start Voicemeeter and try again.");
        //}

        private void StartPolling()
        {
            timer ??= new Timer
            {
                Interval = 300,
                AutoReset = true,
            };
            timer.Elapsed += Poll;
            timer.Start();
        }

        private void Poll(object sender, ElapsedEventArgs e)
        {
            Polling?.Invoke(this, null);
            if (VmrApi is null)
            {
                Close();
                return;
            }
            if (VmrApi.IsParametersDirty() > 0)
            {
                UpdateVariables();
            }
        }

        internal static event EventHandler Polling;

        public static bool CheckConnected(out string connectedVersion)
        {
            bool connected = false;
            connectedVersion = ControlHelpers.ErrorStr;

            if (PluginInstance.VoicemeeterControl != null)
            {
                connectedVersion = PluginInstance.VoicemeeterControl.ConnectedVersion;
                connected = !connectedVersion.Equals(ControlHelpers.ErrorStr);
            }
            return connected;
        }
    }
}
