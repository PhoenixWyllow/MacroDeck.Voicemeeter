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
            //RemoveUnavailableVariables();
        }

        private static void RemoveUnavailableVariables()
        {
            bool unavailableVariables(Variable v) => v.Creator.Equals("Voicemeeter Plugin")
                                                     && !AvailableValues.Options.Any(o => o.AsVariable.Equals(v.Name));
            var variablesNotFound = VariableManager.Variables.Where(unavailableVariables).Select(v => v.Name).ToArray();
            if (variablesNotFound.Length > 0)
            {
                int removedCount = VariableManager.Variables.RemoveAll(unavailableVariables);
                if (removedCount > 0)
                {
                    MacroDeckLogger.Info(PluginInstance.Plugin, $"Deleted {removedCount} variable(s): {string.Join(", ", variablesNotFound)}");
                }
                //foreach (var variable in variablesNotFound)
                //{
                //    VariableManager.DeleteVariable(variable);
                //}
            }
        }

        private void UpdateVariables()
        {
            foreach (var option in AvailableValues.Options)
            {
                SetVariable(option.AsParameter, option.AsVariable, option.Type);
            }
        }

        private void SetVariable(string parameter, string variable, VariableType type)
        {
            if (TryGetValue(parameter, type, out object val))
            {
                VariableManager.SetValue(variable, val, type, PluginInstance.Plugin);
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
                    val = GetParameter(variable) == Constants.On;
                    break;
                default:
                    val = null;
                    return false;
            }
            return true;
        }
    }
}
