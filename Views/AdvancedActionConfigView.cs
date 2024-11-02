using PW.VoicemeeterPlugin.Actions;
using PW.VoicemeeterPlugin.Services;
using SuchByte.MacroDeck.GUI.CustomControls;
using System;

namespace PW.VoicemeeterPlugin.Views;

public partial class AdvancedActionConfigView : ActionConfigControl
{
    private readonly AdvancedAction _action;

    public AdvancedActionConfigView(AdvancedAction action)
    {
        _action = action;
        InitializeComponent();
        ApplyLocalization();

        commandsBox.Text = action.Configuration;
    }

    private void ApplyLocalization()
    {
        labelCommands.Text = LocalizationManager.Instance.Commands;
    }

    public override bool OnActionSave()
    {
        _action.Configuration = commandsBox.Text;
        var commands = _action.Configuration.Split([Environment.NewLine, ";", ","], StringSplitOptions.RemoveEmptyEntries);
        _action.ConfigurationSummary = commands.Length switch
        {
            0 => LocalizationManager.Instance.NoCommandsMsg,
            1 => commands[0],
            _ => string.Format(LocalizationManager.Instance.NCommandsMsg, commands.Length),
        };
        return true;
    }
}