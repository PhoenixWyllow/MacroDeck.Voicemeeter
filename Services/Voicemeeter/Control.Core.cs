using AtgDev.Voicemeeter;
using PW.VoicemeeterPlugin.Models;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using SuchByte.MacroDeck.Variables;
using System;
using System.Linq;
using AtgDev.Voicemeeter.Extensions;
using AtgDev.Voicemeeter.Types;

namespace PW.VoicemeeterPlugin.Services.Voicemeeter;

public sealed partial class Control
{
    private RemoteApiWrapper? VmrApi { get; }
    private VoicemeeterGlobalConfigModel Config { get; }

    public Control()
    {
        Config = VoicemeeterGlobalConfigModel.Deserialize(PluginConfiguration.GetValue(PluginInstance.Plugin, nameof(VoicemeeterGlobalConfigModel)));
        try
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            VmrApi = new RemoteApiWrapper(AtgDev.Voicemeeter.Utils.PathHelper.GetDllPath());
            StartPolling();
        }
        catch (Exception ex)
        {
            MacroDeckLogger.Warning(PluginInstance.Plugin, ex.Message);
            MacroDeckLogger.Trace(PluginInstance.Plugin, ex.StackTrace ?? "No stack");
        }
    }

    private void InitAvailableValues()
    {
        AvailableValues.InitIoCommands();
        AvailableValues.ConnectedType = ConnectedType;
        if (!_connected)
        {
            AvailableValues.Reset();
            return;
        }
        bool initValues = AvailableValues.IoInfo == null;
        if (initValues)
        {
            AvailableValues.InitIoInfo(VmrApi!);
        }
        if (initValues && AvailableValues.IoInfo != null)
        {
            AvailableValues.InitIoOptions();
            RemoveUnavailableVariables();
        }
    }

    private static void RemoveUnavailableVariables()
    {
        try
        {
            bool IsUnavailableVariable(Variable v) => !AvailableValues.IoOptions!.Any(o => o.AsVariable.Equals(v.Name));
            var variablesNotFound = VariableManager.GetVariables(PluginInstance.Plugin).Where(IsUnavailableVariable).Select(v => v.Name);

            foreach (var variable in variablesNotFound)
            {
                VariableManager.DeleteVariable(variable);
            }
        }
        catch (Exception ex)
        {
            MacroDeckLogger.Warning(PluginInstance.Plugin, typeof(Control), $"{nameof(RemoveUnavailableVariables)}: {ex.Message}");
        }
    }

    private void UpdateVariables()
    {
        if (AvailableValues.IoOptions is null)
        {
            return;
        }
        _connected = CheckConnected(out _);
            if (!_connected)
            {
                break;
            }
        foreach (var option in AvailableValues.IoOptions)
        {
            SetVariable(option.AsParameter, option.AsVariable, option.Type);
        }
    }

    private void SetVariable(string parameter, string variable, VariableType type)
    {
        if (TryGetValue(parameter, type, out var val, infoOnly: true))
        {
            VariableManager.SetValue(variable, val, type, PluginInstance.Plugin, Array.Empty<string>());
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
                val = Constants.On.Equals(valb);
                break;
            default:
                val = string.Empty;
                return false;
        }
        return ok;
    }
}