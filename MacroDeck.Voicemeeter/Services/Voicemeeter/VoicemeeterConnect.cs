using AtgDev.Voicemeeter;
using AtgDev.Voicemeeter.Types;
using PW.MacroDeck.VoicemeeterPlugin.Models;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Timers;

namespace PW.MacroDeck.VoicemeeterPlugin.Services.Voicemeeter
{
    public sealed partial class Control
    {

        private Timer timer;
        public string ConnectedVersion => TryGetVoicemeeterVersion(out string version) ? version : VoicemeeterControlHelpers.ErrorStr;
        private VoicemeeterType ConnectedType
        {
            get
            {
                if (VmrApi is null)
                {
                    return VoicemeeterType.None;
                }
                VmrApi.GetVoicemeeterVersion(out VoicemeeterVersion version);
                return (VoicemeeterType)version.v1;
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
            VoicemeeterControlHelpers.TestLogin(VmrApi.Login(), RunVoicemeeter);
        }

        private void RunVoicemeeter()
        {
            int runRes = ResultCodes.Ok;
            int vmTypeRes = VmrApi.GetVoicemeeterType(out VoicemeeterType type);
            if (vmTypeRes == ResultCodes.Ok)
            {
                return;
            }
            else if (vmTypeRes == ResultCodes.OkVmNotLaunched)
            {
                if (VoicemeeterGlobalConfigModel.Deserialize(PluginConfiguration.GetValue(PluginInstance.Plugin, nameof(VoicemeeterGlobalConfigModel))).RunVoicemeeter)
                {
                    VmrApi.RunVoicemeeter(type);
                }
                return;
            }

            SuchByte.MacroDeck.Logging.MacroDeckLogger.Trace(PluginInstance.Plugin, $"{nameof(RunVoicemeeter)}: {vmTypeRes}, {type}, {runRes}");
            throw new Exception("An error occurred. Manually start Voicemeeter and try again.");
        }

        private void Start()
        {
            timer ??= new Timer
            {
                Interval = 300,
                AutoReset = true,
            };
            timer.Elapsed += Poll;
            timer.Enabled = true;
        }

        private void Poll(object sender, ElapsedEventArgs e)
        {
            if (VmrApi is null)
            {
                Close();
                return;
            }
            if (VmrApi.IsParametersDirty() > 0)
            {
                OnUpdating?.Invoke(this, null);
                UpdateVariables();
            }
        }

        public static event EventHandler OnUpdating;
    }
}
