using PW.MacroDeck.VoicemeeterPlugin.Models;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.Text;

namespace PW.MacroDeck.VoicemeeterPlugin.ViewModels
{
    public class VoicemeeterGlobalConfigViewModel : ISoundboardBaseConfigViewModel
    {
        private readonly MacroDeckPlugin _plugin;
        private VoicemeeterGlobalConfigModel OutputConfiguration { get; }

        public VoicemeeterGlobalConfigViewModel(MacroDeckPlugin plugin)
        {
            _plugin = plugin;

            OutputConfiguration = VoicemeeterGlobalConfigModel.Deserialize(PluginConfiguration.GetValue(plugin, nameof(VoicemeeterGlobalConfigModel)));
        }

        ISerializableConfiguration ISoundboardBaseConfigViewModel.SerializableConfiguration => OutputConfiguration;

        public bool RunVoicemeeter { get => OutputConfiguration.RunVoicemeeter; set => OutputConfiguration.RunVoicemeeter = value; }

        public void SaveConfig()
        {
            try
            {
                SetConfig();
                MacroDeckLogger.Info(_plugin, $"{GetType().Name}: config saved");
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(_plugin, $"{GetType().Name}: config NOT saved");
                MacroDeckLogger.Error(_plugin, $"{GetType().Name}: {ex.Message}");
            }
        }

        public void SetConfig()
        {
            PluginConfiguration.SetValue(_plugin, nameof(VoicemeeterGlobalConfigModel), OutputConfiguration.Serialize());
        }
    }
}
