using AtgDev.Voicemeeter;
using PW.VoicemeeterPlugin.Models;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using SuchByte.MacroDeck.Variables;
using System;
using System.Linq;

namespace PW.VoicemeeterPlugin.Services.Voicemeeter
{
    public sealed partial class Control
    {
        private RemoteApiExtender VmrApi { get; }
        private VoicemeeterGlobalConfigModel Config { get; }

        public Control()
        {
            Config = VoicemeeterGlobalConfigModel.Deserialize(PluginConfiguration.GetValue(PluginInstance.Plugin, nameof(VoicemeeterGlobalConfigModel)));
            for (; ; )
            {
                int errors = 0;
                try
                {
                    AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
                    VmrApi = new RemoteApiExtender(AtgDev.Voicemeeter.Utils.PathHelper.GetDllPath());
                    Login();
                    InitAvailableValues();
                    StartPolling();
                }
                catch (Exception ex)
                {
                    errors++;
                    MacroDeckLogger.Error(PluginInstance.Plugin, ex.Message);
                    MacroDeckLogger.Trace(PluginInstance.Plugin, ex.StackTrace);
                }
                if (loginCalled && errors == 1 && Config.TryReconnectOnError)
                {
                    AppDomain.CurrentDomain.ProcessExit -= CurrentDomain_ProcessExit;
                    Close();
                }
                else
                {
                    break;
                }
            }
        }

        private void InitAvailableValues()
        {
            AvailableValues.ConnectedType = ConnectedType;
            AvailableValues.InitIOInfo(VmrApi);
            AvailableValues.InitOptions();
            RemoveUnavailableVariables();
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

        public bool TryGetValue(string parameter, VariableType type, out object val, bool infoOnly = false)
        {
            bool ok;
            switch (type)
            {
                case VariableType.Integer:
                case VariableType.Float:
                    ok = GetParameter(parameter, out float valr, infoOnly);
                    val = valr;
                    break;
                case VariableType.String:
                    ok = GetTextParameter(parameter, out string vals, infoOnly);
                    val = vals;
                    break;
                case VariableType.Bool:
                    ok = GetParameter(parameter, out float valb, infoOnly);
                    val = valb == Constants.On;
                    break;
                default:
                    val = null;
                    return false;
            }
            return ok;
        }
    }
}
