using AtgDev.Voicemeeter.Types;

namespace PW.VoicemeeterPlugin.Services.Voicemeeter
{
    public sealed partial class Control
    {
        /// <summary>
        /// Gets a text value
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="buffer"></param>
        /// <param name="infoOnly"></param>
        /// <returns></returns>
        public bool GetTextParameter(string parameter, out string buffer, bool infoOnly = false)
        {
            bool ok = infoOnly
                ? ControlHelpers.TestResultInfo(VmrApi.GetParameter(parameter, out buffer), parameter)
                : ControlHelpers.TestResult(VmrApi.GetParameter(parameter, out buffer), parameter);
            return ok;
        }

        /// <summary>
        /// Set a text value
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public void SetTextParameter(string parameter, string value)
        {
            ControlHelpers.TestResult(VmrApi.SetParameter(parameter, value));
        }

        /// <summary>
        /// Get a named parameter
        /// </summary>
        /// <param name="parameter">Parameter name</param>
        /// <param name="value"></param>
        /// <param name="infoOnly"></param>
        /// <returns>float value</returns>
        public bool GetParameter(string parameter, out float value, bool infoOnly = false)
        {
            bool ok = infoOnly
                ? ControlHelpers.TestResultInfo(VmrApi.GetParameter(parameter, out value), parameter)
                : ControlHelpers.TestResult(VmrApi.GetParameter(parameter, out value), parameter);
            return ok;
        }

        /// <summary>
        /// Set a named parameter
        /// </summary>
        /// <param name="parameter">Parameter name</param>
        /// <param name="value">float value</param>
        public void SetParameter(string parameter, float value)
        {
            ControlHelpers.TestResult(VmrApi.SetParameter(parameter, value));
        }


        /// <summary>
        /// Set one or several parameters by a script
        /// </summary>
        /// <param name="parameters">One or more instructions separated by comma, semicolon or newline</param>
        public void SetParameters(string parameters)
        {
            ControlHelpers.TestResult(VmrApi.SetParameters(parameters));
        }

        public void GetLevel(ref VoicemeeterLevel type)
        {
            ControlHelpers.TestLevelResult(VmrApi.GetLevel(ref type));
        }

        public float GetLevel(VoicemeeterLevelType type, VoicemeeterChannel channel)
        {
            ControlHelpers.TestLevelResult(VmrApi.GetLevel(type, channel, out float value));
            return value;
        }
    }
}
