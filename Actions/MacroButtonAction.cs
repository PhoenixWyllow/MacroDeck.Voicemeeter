using PW.VoicemeeterPlugin.Models;
using PW.VoicemeeterPlugin.Services;
using PW.VoicemeeterPlugin.ViewModels;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using SuchByte.MacroDeck.Variables;

using System.Diagnostics;

namespace PW.VoicemeeterPlugin.Actions;

public class MacroButtonAction : PluginAction
{
    /// <summary>
    /// Name of the action
    /// </summary>
    public override string Name => LocalizationManager.Instance.MacroButtonActionName;

    /// <summary>
    /// A short description what this action does
    /// </summary>
    public override string Description => LocalizationManager.Instance.MacroButtonActionDescription;

    /// <summary>
    /// Set true if the plugin can be configured.
    /// </summary>
    public override bool CanConfigure => true;

    /// <summary>
    /// Return the ActionConfigControl for this action.
    /// </summary>
    /// <returns></returns>
    public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
    {
        return new Views.MacroButtonActionConfigView(new MacroButtonActionConfigViewModel(this));
    }

    /// <summary>
    /// Gets called when the button with this action gets pressed.
    /// </summary>
    /// <param name="clientId">Returns the client id</param>
    /// <param name="actionButton">Returns the pressed action button</param>
    public override void Trigger(string clientId, ActionButton actionButton)
    {
        if (string.IsNullOrWhiteSpace(Configuration))
        {
            return;
        }
        var config = MacroButtonActionConfigModel.Deserialize(Configuration);
        var value = VariableManager.GetVariable(PluginInstance.Plugin, config.AsVariable());
        if (value is null)
        {
            MacroDeckLogger.Info(PluginInstance.Plugin, typeof(DeviceToggleAction), $"Please report a bug to the developer of the plugin. Expected value: {Configuration}");
            return;
        }
        PluginInstance.VoicemeeterControl.SetButtonState(config.ButtonId, value.Value.Equals(bool.FalseString), config.ButtonType);
        //if (config.ButtonType == ButtonType.Push)
        //{
        //    var sw = Stopwatch.StartNew();
        //    var elapsed = sw.ElapsedMilliseconds;
        //    while (elapsed < 100)
        //    {
        //        elapsed = sw.ElapsedMilliseconds;
        //    }
        //    PluginInstance.VoicemeeterControl.SetButtonState(config.ButtonId, value.Value.Equals(bool.FalseString));
        //}
    }
}