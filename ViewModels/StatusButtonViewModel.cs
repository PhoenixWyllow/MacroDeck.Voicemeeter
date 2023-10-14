using PW.VoicemeeterPlugin.Services.Voicemeeter;
using PW.VoicemeeterPlugin.Services;
using System;
using System.Windows.Forms;
using VoicemeeterControl = PW.VoicemeeterPlugin.Services.Voicemeeter.Control;
using SuchByte.MacroDeck.GUI.CustomControls;
using PW.VoicemeeterPlugin.Properties;

namespace PW.VoicemeeterPlugin.ViewModels;
internal class StatusButtonViewModel
{
    public ContentSelectorButton StatusButton { get; }
    private bool? _isConnected;
    private readonly ToolTip _statusToolTip;

    public StatusButtonViewModel()
    {
        StatusButton = new() 
        {
            BackgroundImageLayout = ImageLayout.Zoom
        };

        _statusToolTip = new();
        UpdateStatusButton();
        StatusButton.Click += StatusButton_Click;
        VoicemeeterControl.Polling += VoicemeeterControl_Polling;
        SuchByte.MacroDeck.MacroDeck.MainWindow!.FormClosed += MainWindow_FormClosed;
    }

    private void MainWindow_FormClosed(object? sender, FormClosedEventArgs e) => VoicemeeterControl.Polling -= VoicemeeterControl_Polling;

    private void StatusButton_Click(object? sender, EventArgs e)
    {
        if (PluginInstance.Plugin.CanConfigure)
        {
            PluginInstance.Plugin.OpenConfigurator();
        }
    }

    private void VoicemeeterControl_Polling(object? sender, EventArgs e) => UpdateStatusButton();

    private void UpdateStatusButton()
    {
        try
        {
            bool connected = VoicemeeterControl.CheckConnected(out string connectedVersion);

            if (_isConnected is null || _isConnected != connected)
            {
                _isConnected = connected;
                StatusButton.BackgroundImage = connected ? Resources.VoiceMeeterConnected : Resources.VoiceMeeterDisconnected;
                string toolip = connected
                    ? $"{LocalizationManager.Instance.VoiceMeeterConnected}{Environment.NewLine}{connectedVersion} ({AvailableValues.ConnectedType})"
                    : LocalizationManager.Instance.VoiceMeeterDisconnected;
                _statusToolTip.SetToolTip(StatusButton, toolip);
            }
        }
        catch { }
    }
}