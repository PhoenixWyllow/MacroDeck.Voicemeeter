using SuchByte.MacroDeck.Plugins;
using System.Collections.Generic;
using System.Drawing;
using PW.MacroDeck.VoicemeeterPlugin.Actions;
using System;
using PW.MacroDeck.VoicemeeterPlugin.Properties;
using SuchByte.MacroDeck.GUI.CustomControls;
using PW.MacroDeck.VoicemeeterPlugin.Services;
using VoicemeeterControl = PW.MacroDeck.VoicemeeterPlugin.Services.Voicemeeter.Control;

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

        public override bool CanConfigure => false;//true;

        /// <summary>
        /// Gets called when Macro Deck enables the plugin
        /// </summary>
        public override void Enable()
        {
            //optimised initilization - this adds 2s to program startup!
            //PluginInstance.VoicemeeterControl = new VoicemeeterControl();
            new System.Threading.Tasks.Task(() => PluginInstance.VoicemeeterControl = new VoicemeeterControl()).Start();

            Actions = new List<PluginAction>
            {
                new DeviceMuteAction(),
                new AdvancedAction(),
            };
        }

        /// <summary>
        /// Gets called when the user wants to configure the plugin
        /// </summary>
        public override void OpenConfigurator()
        {
            //new Views.VoicemeeterGlobalConfigView(this).ShowDialog();
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
