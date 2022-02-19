using AtgDev.Voicemeeter;
using AtgDev.Voicemeeter.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;

namespace PW.MacroDeck.VoicemeeterPlugin.Services
{
    public sealed class VoicemeeterControl
    {
        private readonly RemoteApiExtender _vmrApi;

        private RemoteApiExtender VmrApi => _vmrApi;

        private Timer timer;
        public string ConnectedVersion => TryGetVoicemeeterVersion(out string version) ? VoicemeeterControlHelpers.ErrorStr : version;

        private bool TryGetVoicemeeterVersion(out string version)
        {
            version = string.Empty;
            return VmrApi is null || VmrApi.GetVoicemeeterVersion(out version) < 0;
        }

        public VoicemeeterControl()
        {
            try
            {
                _vmrApi = new RemoteApiExtender(VoicemeeterControlHelpers.GetDllPath());
                Login();
                AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;

                Start();
            }
            catch (Exception ex)
            {
                SuchByte.MacroDeck.Logging.MacroDeckLogger.Error(PluginInstance.Plugin, ex.Message);
                SuchByte.MacroDeck.Logging.MacroDeckLogger.Trace(PluginInstance.Plugin, ex.StackTrace);
            }
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
            if (VmrApi.GetVoicemeeterType(out VoicemeeterType type) == 0)
            {
                if (VmrApi.RunVoicemeeter(type) == 0)
                {
                    return;
                }
            }

            throw new Exception("An error occurred. Manually start Voicemeeter and try again.");
        }

        private void Start()
        {
            timer ??= new Timer
            {
                Interval = 100,
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
                OnUpdating?.Invoke(this, new EventArgs());
                UpdateVariables();
            }
        }

        public event EventHandler OnUpdating;

        private void UpdateVariables()
        {
            //TODO
        }

        #region GetSetParameters
        /// <summary>
        /// Gets a text value
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public string GetTextParameter(string parameter)
        {
            VoicemeeterControlHelpers.TestResult(VmrApi.GetParameter(parameter, out string buffer));
            return buffer;
        }

        /// <summary>
        /// Set a text value
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public void SetTextParameter(string parameter, string value)
        {
            VoicemeeterControlHelpers.TestResult(VmrApi.SetParameter(parameter, value));
        }

        /// <summary>
        /// Get a named parameter
        /// </summary>
        /// <param name="parameter">Parameter name</param>
        /// <returns>float value</returns>
        public float GetParameter(string parameter)
        {
            VoicemeeterControlHelpers.TestResult(VmrApi.GetParameter(parameter, out float value));
            return value;
        }

        /// <summary>
        /// Set a named parameter
        /// </summary>
        /// <param name="parameter">Parameter name</param>
        /// <param name="value">float value</param>
        public void SetParameter(string parameter, float value)
        {
            VoicemeeterControlHelpers.TestResult(VmrApi.SetParameter(parameter, value));
        }


        /// <summary>
        /// Set one or several parameters by a script
        /// </summary>
        /// <param name="parameters">One or more instructions separated by comma, semicolon or newline</param>
        public void SetParameters(string parameters)
        {
            VoicemeeterControlHelpers.TestResult(VmrApi.SetParameters(parameters));
        }

        public void GetLevel(ref VoicemeeterLevel type)
        {
            VoicemeeterControlHelpers.TestLevelResult(VmrApi.GetLevel(ref type));
        }

        public float GetLevel(VoicemeeterLevelType type, VoicemeeterChannel channel)
        {
            VoicemeeterControlHelpers.TestLevelResult(VmrApi.GetLevel(type, channel, out float value));
            return value;
        }

        #endregion
        /*
        #region Commands

        /// <summary>
        /// Shutdown the VoiceMeeter program
        /// </summary>
        /// <param name="voicemeeterType">The Voicemeeter program to run</param>
        public void Shutdown() => SetParameter(VoicemeeterCommand.Shutdown, 1);

        /// <summary>
        /// Restart the audio engine
        /// </summary>
        public void Restart() => SetParameter(VoicemeeterCommand.Restart, 1);

        /// <summary>
        /// Shows the running Voicemeeter application if minimized.
        /// </summary>
        public void Show() => SetParameter(VoicemeeterCommand.Show, 1);

        /// <summary>
        /// Eject Cassette 
        /// </summary>
        public void Eject() => SetParameter(VoicemeeterCommand.Eject, 1);

        /// <summary>
        /// Load a configuation file name
        /// </summary>
        /// <param name="configurationFileName">Full path to file</param>
        public void Load(string configurationFileName) => SetTextParameter(VoicemeeterCommand.Load, configurationFileName);

        /// <summary>
        /// Save a configuration to the given file name
        /// </summary>
        /// <param name="configurationFileName">Full path to file</param>
        public void Save(string configurationFileName) => SetTextParameter(VoicemeeterCommand.Load, configurationFileName);

        #endregion
        */
    }
}
