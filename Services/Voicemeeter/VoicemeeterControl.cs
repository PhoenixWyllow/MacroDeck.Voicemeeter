using AtgDev.Voicemeeter;
using PW.VoicemeeterPlugin;
using PW.VoicemeeterPlugin.Models;
using PW.VoicemeeterPlugin.Services.Voicemeeter;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Variables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;

namespace PW.VoicemeeterPlugin.Services.Voicemeeter
{
    public sealed partial class Control
    {
        private RemoteApiExtender VmrApi { get; }

        public Control()
        {
            try
            {
                VmrApi = new RemoteApiExtender(AtgDev.Voicemeeter.Utils.PathHelper.GetDllPath());
                Login();
                AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
                InitAvailableValues();
                Start();
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Plugin, ex.Message);
                MacroDeckLogger.Trace(PluginInstance.Plugin, ex.StackTrace);
            }
        }

        private void InitAvailableValues()
        {
            AvailableValues.ConnectedType = ConnectedType;
            AvailableValues.InitIOInfo(VmrApi);
            AvailableValues.InitOptions();
        }

        private void UpdateVariables()
        {
            foreach (var option in AvailableValues.Options)
            {
                SetVariable(option.AsVariable, option.Type);
            }
        }

        private void SetVariable(string variable, VariableType type)
        {
            if (TryGetValue(variable, type, out object val))
            {
                VariableManager.SetValue($"{variable}", val, type, PluginInstance.Plugin);
            }
        }

        private bool TryGetValue(string variable, VariableType type, out object val)
        {
            switch (type)
            {
                case VariableType.Integer:
                case VariableType.Float:
                    val = GetParameter(variable);
                    break;
                case VariableType.String:
                    val = GetTextParameter(variable);
                    break;
                case VariableType.Bool:
                    val = GetParameter(variable) == VoicemeeterValues.On;
                    break;
                default:
                    val = null;
                    return false;
            }
            return true;
        }
    }
}
