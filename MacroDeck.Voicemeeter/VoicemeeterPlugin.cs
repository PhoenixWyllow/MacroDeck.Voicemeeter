using SuchByte.MacroDeck.Plugins;
using System.Collections.Generic;
using System.Drawing;
using PW.MacroDeck.VoicemeeterPlugin.Actions;
using System;
using PW.MacroDeck.VoicemeeterPlugin.Properties;
using SuchByte.MacroDeck.GUI.CustomControls;
using PW.MacroDeck.VoicemeeterPlugin.Services;

namespace PW.MacroDeck.VoicemeeterPlugin
{

    internal static class PluginInstance
    {
        public static MacroDeckPlugin Plugin { get; set; }
        public static VoicemeeterControl VoicemeeterControl { get; set; }
    }

    public class VoicemeeterPlugin : MacroDeckPlugin
    {
        public override string Description => LocalizationManager.Instance.PluginDescription;

        public override Image Icon => Resources.MacroDeckVoicemeeter;

        public override bool CanConfigure => false;

        /// <summary>
        /// Gets called when Macro Deck enables the plugin
        /// </summary>
        public override void Enable()
        {
            PluginInstance.VoicemeeterControl = new VoicemeeterControl();

            Actions = new List<PluginAction>
            {
                new DeviceMuteAction(),
            };
        }

        /// <summary>
        /// Gets called when the user wants to configure the plugin
        /// </summary>
        public override void OpenConfigurator()
        {
        }

        public VoicemeeterPlugin()
        {
            PluginInstance.Plugin ??= this;

            LocalizationManager.CreateInstance();

            SuchByte.MacroDeck.MacroDeck.OnMainWindowLoad += MacroDeck_OnMainWindowLoad;
        }

        private ContentSelectorButton contentButton;
        private void MacroDeck_OnMainWindowLoad(object sender, EventArgs e)
        {
            if (sender != null &&
                sender is SuchByte.MacroDeck.GUI.MainWindow mainWindow)
            {
                contentButton = new Views.StatusButtonControl();
                mainWindow.contentButtonPanel.Controls.Add(contentButton);
            }

            SuchByte.MacroDeck.Logging.MacroDeckLogger.Trace(PluginInstance.Plugin, "loaded button");
        }

    }
}
