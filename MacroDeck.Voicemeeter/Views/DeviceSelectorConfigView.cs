using PW.MacroDeck.VoicemeeterPlugin.Models;
using SuchByte.MacroDeck.GUI.CustomControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PW.MacroDeck.VoicemeeterPlugin.Views
{
    public partial class DeviceSelectorConfigView : ActionConfigControl
    {
        public DeviceSelectorConfigView()
        {
            InitializeComponent();

            deviceSelectorBox.Items.AddRange(AvailableValues.IOInfo.Select(c => c.Name).ToArray());
        }
    }
}
