﻿using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using PW.VoicemeeterPlugin.Services;

namespace PW.VoicemeeterPlugin.Actions;

public sealed class AdvancedAction : PluginAction
{
    /// <summary>
    /// Name of the action
    /// </summary>
    public override string Name => LocalizationManager.Instance.AdvancedActionName;

    /// <summary>
    /// A short description what this action does
    /// </summary>
    public override string Description => LocalizationManager.Instance.AdvancedActionDescription;

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
        return new Views.AdvancedActionConfigView(this);
    }

    /// <summary>
    /// Gets called when the button with this action gets pressed.
    /// </summary>
    /// <param name="clientId">Returns the client id</param>
    /// <param name="actionButton">Returns the pressed action button</param>
    public override void Trigger(string clientId, ActionButton actionButton)
    {
        if (!string.IsNullOrWhiteSpace(Configuration))
        {
            PluginInstance.VoicemeeterControl.SetParameters(Configuration);
        }
    }
}