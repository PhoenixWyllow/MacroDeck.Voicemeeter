using PW.VoicemeeterPlugin.Models;

using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;

using System;

namespace PW.VoicemeeterPlugin.ViewModels;
public class MacroButtonActionConfigViewModel : ISavableConfigViewModel
{
    private readonly PluginAction _action;
    private readonly MacroButtonActionConfigModel _configuration;

    ISerializableConfiguration ISavableConfigViewModel.SerializableConfiguration => _configuration;

    public MacroButtonActionConfigViewModel(PluginAction action)
    {
        _action = action;
        _configuration = MacroButtonActionConfigModel.Deserialize(action.Configuration);
        
        ButtonId = _configuration.ButtonId;
        ButtonType = _configuration.ButtonType;
    }

    public bool ValidConfig { get; private set; }

    public int ButtonId { get; set; }

    public ButtonType ButtonType { get; set; }

    public void SetConfig()
    {
        //check button id is between 0 and 80
        ValidConfig = ButtonId >= 0 || ButtonId < 80;
        if (!ValidConfig)
        {
            return;
        }
        _configuration.ButtonId = ButtonId;
        _configuration.ButtonType = ButtonType;
        _action.BindableVariable = _configuration.AsVariable(); //note: this only does something when the user creates a new button, not when they edit an existing one
        _action.ConfigurationSummary = _configuration.ToString();
        _action.Configuration = _configuration.Serialize();
    }

    public void SaveConfig()
    {
        try
        {
            SetConfig();
        }
        catch (Exception ex)
        {
            MacroDeckLogger.Warning(PluginInstance.Plugin, ex.Message);
            MacroDeckLogger.Trace(PluginInstance.Plugin, ex.StackTrace ?? "No stack");
        }
    } 

}
