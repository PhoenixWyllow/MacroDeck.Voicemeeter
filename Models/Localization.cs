using System.Text.Json.Serialization;

namespace PW.VoicemeeterPlugin.Models
{
    internal sealed class Localization
    {
        public string Attribution { get; set; } = "built-in values";
        public string Language { get; set; } = "English (default)";
        public string PluginDescription { get; set; } = "A Voicemeeter plugin for Macro Deck";
        public string VoiceMeeterConnected { get; set; } = "Voicemeeter Connected";
        public string VoiceMeeterDisconnected { get; set; } = "Voicemeeter Disconnected";
        public string Device { get; set; } = "Device";
        public string Action { get; set; } = "Action";
        public string ToggleActionName { get; set; } = "Toggle device";
        public string ToggleActionDescription { get; set; } = "Toggle an option on a strip or bus";
        public string AdvancedActionName { get; set; } = "Advanced/Custom";
        public string AdvancedActionDescription { get; set; } = "Advanced/Custom options for controlling Voicemeeter using the Voicemeeter API language. \nPlease read the Voicemeeter docs for instructions.";
        public string Commands { get; set; } = "Commands (separated by ';' or new line)";
    }
}
