using PW.MacroDeck.VoicemeeterPlugin.Models;
using PW.MacroDeck.VoicemeeterPlugin.Properties;
using PW.MacroDeck.VoicemeeterPlugin.Services;
using PW.MacroDeck.VoicemeeterPlugin.Services.Voicemeeter;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VoicemeeterControl = PW.MacroDeck.VoicemeeterPlugin.Services.Voicemeeter.Control;

namespace PW.MacroDeck.VoicemeeterPlugin.Views
{
    public class StatusButtonControl : ContentSelectorButton
    {
        private readonly ToolTip _statusToolTip;
        public StatusButtonControl() : base()
        {
            _statusToolTip = new ToolTip();
            UpdateStatusButton();
            Click += StatusButton_Click;
            VoicemeeterControl.OnUpdating += (_, __) => UpdateStatusButton();
        }

        private void StatusButton_Click(object sender, EventArgs e)
        {
            //TODO placeholder
        }

        private void UpdateStatusButton()
        {
            bool connected = false;
            string connectedVersion = VoicemeeterControlHelpers.ErrorStr;

            if (PluginInstance.VoicemeeterControl != null)
            {
                connectedVersion = PluginInstance.VoicemeeterControl.ConnectedVersion;
                connected = !connectedVersion.Equals(VoicemeeterControlHelpers.ErrorStr);
            }

            BackgroundImage = connected ? Resources.VoiceMeeterConnected : Resources.VoiceMeeterDisconnected;
            string toolip = connected
                          ? $"{LocalizationManager.Instance.VoiceMeeterConnected}{Environment.NewLine}{connectedVersion} ({AvailableValues.ConnectedType})"
                          : LocalizationManager.Instance.VoiceMeeterDisconnected;
            _statusToolTip.SetToolTip(this, toolip);
        }
    }
}
