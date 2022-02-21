using AtgDev.Voicemeeter;
using PW.MacroDeck.VoicemeeterPlugin.Models;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Variables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;

namespace PW.MacroDeck.VoicemeeterPlugin.Services.Voicemeeter
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
                SetVariable(option.AsParameter, option.Type);
            }
        }

        private void SetVariable(string parameter, VariableType type)
        {
            if (TryGetValue(parameter, type, out object val))
            {
                VariableManager.SetValue($"vm_{parameter}", val, type, PluginInstance.Plugin);
            }
        }

        private bool TryGetValue(string parameter, VariableType type, out object val)
        {
            switch (type)
            {
                case VariableType.Integer:
                case VariableType.Float:
                    val = GetParameter(parameter);
                    break;
                case VariableType.String:
                    val = GetTextParameter(parameter);
                    break;
                case VariableType.Bool:
                    val = GetParameter(parameter) == VoicemeeterValues.On;
                    break;
                default:
                    val = null;
                    return false;
            }
            return true;
        }
    }
}
