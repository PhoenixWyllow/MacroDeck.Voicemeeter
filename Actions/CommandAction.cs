using PW.VoicemeeterPlugin.Services;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.Plugins;
using PW.VoicemeeterPlugin.Models;
using SuchByte.MacroDeck.Logging;

namespace PW.VoicemeeterPlugin.Actions;

public sealed class CommandAction : PluginAction
{
    public override string Name => LocalizationManager.Instance.CommandActionName;

    public override string Description => LocalizationManager.Instance.CommandActionDescription;

    public override bool CanConfigure => true;

    public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
    {
        return new Views.CommandActionConfigView(new(this));
    }

    public override void Trigger(string clientId, ActionButton actionButton)
    {
        if (string.IsNullOrWhiteSpace(Configuration))
        {
            return;
        }
        var config = CommandActionConfigModel.Deserialize(Configuration);
        switch (config.Command.CommandType)
        {
            case Commands.Shutdown:
                PluginInstance.VoicemeeterControl.Shutdown();
                break;
            case Commands.Restart:
                PluginInstance.VoicemeeterControl.Restart();
                break;
            case Commands.Show:
                PluginInstance.VoicemeeterControl.Show();
                break;
            case Commands.Reset:
                PluginInstance.VoicemeeterControl.Reset();
                break;
            case Commands.ConfigSave:
                PluginInstance.VoicemeeterControl.Save(config.CommandValue);
                break;
            case Commands.ConfigLoad:
                PluginInstance.VoicemeeterControl.Load(config.CommandValue);
                break;
            case Commands.RecorderEject:
                PluginInstance.VoicemeeterControl.Eject();
                break;
            case Commands.RecorderLoad:
                PluginInstance.VoicemeeterControl.RecLoad(config.CommandValue);
                break;
            default:
                MacroDeckLogger.Warning(PluginInstance.Plugin, "No command. Check button configuration.");
                break;
        }
    }
}