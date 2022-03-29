using PW.VoicemeeterPlugin.Properties;
using PW.VoicemeeterPlugin.Services.Voicemeeter;
using PW.VoicemeeterPlugin.Models;
using PW.VoicemeeterPlugin.Services;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VoicemeeterControl = PW.VoicemeeterPlugin.Services.Voicemeeter.Control;

namespace PW.VoicemeeterPlugin.Views
{
    public class StatusButtonControl : ContentSelectorButton
    {
        private bool? isConnected;
        private readonly ToolTip _statusToolTip;

        public StatusButtonControl() : base()
        {
            _statusToolTip = new ToolTip();
            UpdateStatusButton();
            Click += StatusButton_Click;
            VoicemeeterControl.Polling += VoicemeeterControl_Polling;
            SuchByte.MacroDeck.MacroDeck.MainWindow.FormClosed += MainWindow_FormClosed;
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e) => VoicemeeterControl.Polling -= VoicemeeterControl_Polling;

        private void StatusButton_Click(object sender, EventArgs e)
        {
            if (PluginInstance.Plugin.CanConfigure)
            {
                PluginInstance.Plugin.OpenConfigurator();
            }
        }

        private void VoicemeeterControl_Polling(object sender, EventArgs e) => UpdateStatusButton();

        private void UpdateStatusButton()
        {
            try
            {
                bool connected = VoicemeeterControl.CheckConnected(out string connectedVersion);

                if (isConnected is null || isConnected != connected)
                {
                    isConnected = connected;
                    BackgroundImage = connected ? Resources.VoiceMeeterConnected : Resources.VoiceMeeterDisconnected;
                    string toolip = connected
                                  ? $"{LocalizationManager.Instance.VoiceMeeterConnected}{Environment.NewLine}{connectedVersion} ({AvailableValues.ConnectedType})"
                                  : LocalizationManager.Instance.VoiceMeeterDisconnected;
                    _statusToolTip.SetToolTip(this, toolip);
                }
            }
            catch { }
        }
    }
}
