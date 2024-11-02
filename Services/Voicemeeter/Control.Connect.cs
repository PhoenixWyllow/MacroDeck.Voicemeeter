using AtgDev.Voicemeeter;
using AtgDev.Voicemeeter.Extensions;
using AtgDev.Voicemeeter.Types;
using System;
using System.Timers;

namespace PW.VoicemeeterPlugin.Services.Voicemeeter;

public sealed partial class Control
{
    private static readonly object key = new();
    private bool _loginCalled;
    private bool _connected;
    private Timer? _timer;
    private string ConnectedVersion => TryGetVoicemeeterVersion(out string version) ? version : ControlHelpers.ErrorStr;
    private VoicemeeterType ConnectedType
    {
        get
        {
            if (VmrApi is null)
            {
                return VoicemeeterType.None;
            }

            _ = VmrApi.GetVoicemeeterType(out VoicemeeterType type);
            return type;
        }
    }


    private bool TryGetVoicemeeterVersion(out string version)
    {
        version = string.Empty;
        return VmrApi is not null && VmrApi.GetVoicemeeterVersion(out version) == ResultCodes.Ok;
    }

    private void CurrentDomain_ProcessExit(object? sender, EventArgs e)
    {
        Close();
    }

    private void Close()
    {
        _timer?.Stop();
        _timer?.Dispose();
        Logout();
        VmrApi?.Dispose();
    }

    private void Logout()
    {
        if (_loginCalled)
        {
            VmrApi?.Logout();
            _loginCalled = false;
        }
    }

    private void Login()
    {
        if (!_connected && !_loginCalled && VmrApi is not null)
        {
            ControlHelpers.TestLogin(
                VmrApi.Login(),
                onLoginSuccess: (_) => _loginCalled = true, //RunVoicemeeter,
                onLoginFail: Logout);
        }
        _connected = CheckConnected(out _);
    }

    //private void RunVoicemeeter(int loginResult)
    //{
    //    loginCalled = true
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
        _timer ??= new Timer
        {
            Interval = 300,
            AutoReset = true,
        };
        _timer.Elapsed += Poll;
        _timer.Start();
    }

    private void Poll(object? sender, ElapsedEventArgs e)
    {
        if (VmrApi is null)
        {
            Close();
            SuchByte.MacroDeck.Logging.MacroDeckLogger.Warning(PluginInstance.Plugin, "Voicemeeter plugin has stopped. You will need to restart Macro Deck to use the features.");
            return;
        }
        lock (key)
        {
            Login();
            InitAvailableValues();
        }
        Polling?.Invoke(this, EventArgs.Empty);
        if (_connected && VmrApi.IsParametersDirty() > 0)
        {
            UpdateVariables();
        }
    }

    internal static event EventHandler? Polling;

    public static bool CheckConnected(out string connectedVersion)
    {
        connectedVersion = PluginInstance.VoicemeeterControl.ConnectedVersion;
        return !connectedVersion.Equals(ControlHelpers.ErrorStr);
    }
}