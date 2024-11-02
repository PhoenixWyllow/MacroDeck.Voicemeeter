using AtgDev.Voicemeeter;
using SuchByte.MacroDeck.Logging;
using System;
using System.Runtime.CompilerServices;

namespace PW.VoicemeeterPlugin.Services.Voicemeeter;

internal static class ControlHelpers
{
    public const string ErrorStr = "Communication Error";

    public static void TestLogin(int loginResult, Action<int>? onLoginSuccess = default, Action? onLoginFail = default)
    {
        switch (loginResult)
        {
            case ResultCodes.Ok:
            case ResultCodes.OkVmNotLaunched:
                onLoginSuccess?.Invoke(loginResult);
                break;
            case ResultCodes.Error:
                MacroDeckLogger.Trace(PluginInstance.Plugin, "Not installed or could not connect.");
                break;//throw new Exception("Not installed or could not connect.");
            default:
                onLoginFail?.Invoke();
                MacroDeckLogger.Trace(PluginInstance.Plugin, "Unexpected connection. Connection was not correctly closed previously.");
                break;//throw new Exception("Unexpected connection. Connection was not correctly closed previously.");
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
            case ResultCodes.Ok: break;
            case ResultCodes.Error: throw new("Error");
            case ResultCodes.NoServer: throw new("Not Connected");
            case ResultCodes.NoLevelAvailable: break;
            case -4: throw new ArgumentException("Channel out of range");
            default: throw UnknownError(result);
        }
    }

    //0: OK(no error).
    //-1: error
    //-2: no server.
    //-3: unknown parameter
    //-4: structure mismatch
    public static bool TestResult(int result, [CallerMemberName] string callerName = "")
    {
        try
        {
            TestResultThrow(result);
            return true;
        }
        catch (Exception ex)
        {
            MacroDeckLogger.Warning(PluginInstance.Plugin, typeof(Control), $"{callerName}: {ex.Message}");
        }
        return false;
    }

    //0: OK(no error).
    //-1: error
    //-2: no server.
    //-3: unknown parameter
    //-4: structure mismatch
    public static bool TestResultInfo(int result, [CallerMemberName] string callerName = "")
    {
        try
        {
            TestResultThrow(result);
            return true;
        }
        catch (Exception ex)
        {
            MacroDeckLogger.Info(PluginInstance.Plugin, typeof(Control), $"{callerName}: {ex.Message}");
        }
        return false;
    }

    //0: OK(no error).
    //>0: error in line
    //-1: error
    //-2: no server
    //-3: unknown parameter
    //-4: structure mismatch
    private static void TestResultThrow(int result)
    {
        if (result > 0)
        {
            throw new Exception("Error in line " + result);
        }
        switch (result)
        {
            case ResultCodes.Ok: break;
            case ResultCodes.Error: throw new Exception("Parameter Error");
            case ResultCodes.NoServer: throw new Exception("Not Connected");
            case ResultCodes.UnexpectedError1: throw new ArgumentException("Parameter not found");
            case ResultCodes.UnexpectedError2: throw new Exception("Structure mismatch");
            default: throw UnknownError(result);
        }
    }

    private static Exception UnknownError(int result) => new($"Unknown ({result})");
}