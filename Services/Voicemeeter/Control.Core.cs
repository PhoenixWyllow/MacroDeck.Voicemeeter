using AtgDev.Voicemeeter;
using PW.VoicemeeterPlugin.Models;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using SuchByte.MacroDeck.Variables;
using System;
using System.Linq;
using static System.Windows.Forms.Design.AxImporter;

namespace PW.VoicemeeterPlugin.Services.Voicemeeter
{
    public sealed partial class Control
    {
        private RemoteApiExtender VmrApi { get; }
        private VoicemeeterGlobalConfigModel Config { get; }

        public Control()
        {
            Config = VoicemeeterGlobalConfigModel.Deserialize(PluginConfiguration.GetValue(PluginInstance.Plugin, nameof(VoicemeeterGlobalConfigModel)));
            try
            {
                AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
                VmrApi = new RemoteApiExtender(AtgDev.Voicemeeter.Utils.PathHelper.GetDllPath());
                StartPolling();
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Warning(PluginInstance.Plugin, ex.Message);
                MacroDeckLogger.Trace(PluginInstance.Plugin, ex.StackTrace);
            }
        }

        private void InitAvailableValues()
        {
            AvailableValues.ConnectedType = ConnectedType;
            if (!connected)
            {
                AvailableValues.Reset();
                return;
            }
            bool initValues = AvailableValues.IOInfo == null;
            if (initValues)
            {
                AvailableValues.InitIOInfo(VmrApi);
            }
            if (initValues && AvailableValues.IOInfo != null)
            {
                AvailableValues.InitIOOptions();
                RemoveUnavailableVariables();
            }
        }

        private static void RemoveUnavailableVariables()
        {
            bool unavailableVariables(Variable v) => v.Creator.Equals("Voicemeeter Plugin")
                                                     && !AvailableValues.IOOptions.Any(o => o.AsVariable.Equals(v.Name));
            var variablesNotFound = VariableManager.ListVariables.Where(unavailableVariables).Select(v => v.Name);

            foreach (var variable in variablesNotFound)
            {
                VariableManager.DeleteVariable(variable);
            }
            //if (variablesNotFound.Any())
            //{
            //    int removedCount = VariableManager.Variables.RemoveAll(unavailableVariables);
            //    if (removedCount > 0)
            //    {
            //        MacroDeckLogger.Info(PluginInstance.Plugin, $"Deleted {removedCount} variable(s): {string.Join(", ", variablesNotFound)}");
            //    }
            //}
        }

        private void UpdateVariables()
        {
            if (AvailableValues.IOOptions is null)
            {
                return;
            }
            foreach (var option in AvailableValues.IOOptions)
            {
                if (!connected)
                {
                    break;
                }
                SetVariable(option.AsParameter, option.AsVariable, option.Type);
            }
        }

        private void SetVariable(string parameter, string variable, VariableType type)
        {
            if ((connected = CheckConnected(out _)) && TryGetValue(parameter, type, out object val, infoOnly: true))
            {
                VariableManager.SetValue(variable, val, type, PluginInstance.Plugin, null);
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
