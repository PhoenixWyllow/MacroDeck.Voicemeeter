using AtgDev.Voicemeeter.Types;
using PW.VoicemeeterPlugin.Services.Voicemeeter;
using SuchByte.MacroDeck.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace PW.VoicemeeterPlugin.Services.Voicemeeter
{
    public sealed partial class Control
    {
        /// <summary>
        /// Gets a text value
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public string GetTextParameter(string parameter)
        {
            ControlHelpers.TestResult(VmrApi.GetParameter(parameter, out string buffer));
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
            ControlHelpers.TestResult(VmrApi.SetParameter(parameter, value));
        }

        /// <summary>
        /// Get a named parameter
        /// </summary>
        /// <param name="parameter">Parameter name</param>
        /// <returns>float value</returns>
        public float GetParameter(string parameter)
        {
            ControlHelpers.TestResult(VmrApi.GetParameter(parameter, out float value));
            return value;
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
