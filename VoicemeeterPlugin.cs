using SuchByte.MacroDeck.Plugins;
using PW.VoicemeeterPlugin.Actions;
using System;
using SuchByte.MacroDeck.GUI.CustomControls;
using VoicemeeterControl = PW.VoicemeeterPlugin.Services.Voicemeeter.Control;
using PW.VoicemeeterPlugin.Services;
using System.Net.Sockets;
using System.Windows.Forms;

namespace PW.VoicemeeterPlugin;

internal static class PluginInstance
{
    public static MacroDeckPlugin Plugin { get; set; }
    public static VoicemeeterControl VoicemeeterControl { get; set; }
}

public class VoicemeeterPlugin : MacroDeckPlugin
{
    public override bool CanConfigure => true;

    /// <summary>
    /// Gets called when Macro Deck enables the plugin
    /// </summary>
    public override void Enable()
    {
        //optimized initialization
        new System.Threading.Tasks.Task(() => PluginInstance.VoicemeeterControl = new()).Start();

        Actions = new()
        {
            new DeviceToggleAction(),
            new DeviceSliderAction(),
            new CommandAction(),
            new AdvancedAction(),
        };
    }

    /// <summary>
    /// Gets called when the user wants to configure the plugin
    /// </summary>
    public override void OpenConfigurator()
    {
        //using var config = new Views.VoicemeeterGlobalConfigView(this);
        using var config = new Views.AddAdditionalVariablesConfigView();
        config.ShowDialog();
    }

    public VoicemeeterPlugin()
    {
        PluginInstance.Plugin = this;

        LocalizationManager.CreateInstance();

        SuchByte.MacroDeck.MacroDeck.OnMainWindowLoad += MacroDeck_OnMainWindowLoad;
    }

    private ViewModels.StatusButtonViewModel? _statusButton;
    private void MacroDeck_OnMainWindowLoad(object? sender, EventArgs e)
    {
        if (sender is SuchByte.MacroDeck.GUI.MainWindow mainWindow)
        {
            _statusButton = new ViewModels.StatusButtonViewModel();
            mainWindow.contentButtonPanel.Controls.Add(_statusButton.StatusButton);
        }
    }

}