namespace PW.VoicemeeterPlugin.Models;

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
    public string LabelParameter { get; set; } = "Parameter";
    public string ParameterError { get; set; } = "An error occurred while validating the parameter.";
    public string ParameterExists { get; set; } = "This parameter already exists.";
    public string NoCommandsMsg { get; set; } = "No commands to execute";
    public string NCommandsMsg { get; set; } = "{0} commands";
    public string CommandActionName { get; set; } = "Execute command";
    public string CommandActionDescription { get; set; } = "Select a command to execute";
    public string Command { get; set; } = "Command";
    public string CommandValue { get; set; } = "Command value";
    public string CommandError { get; set; } = "An error occurred while validating the command.";
    public string SliderActionName { get; set; } = "Slider control";
    public string SliderActionDescription { get; set; } = "Change a slider by a given amount";
    public string SliderValue { get; set; } = "Change slider by amount";
    public string ErrorZeroSliderValue { get; set; } = "Amount cannot be zero";
    public string ErrorNoDeviceSelected { get; set; } = "No device selected";
    public string MacroButtonActionName { get; set; } = "Use Macro Button";
    public string MacroButtonActionDescription { get; set; } = "Use a Voicemeeter Macro Button";
    public string MacroButtonButtonId { get; set; } = "Button ID";
    public string MacroButtonButtonType { get; set; } = "Button Type";
    public string MacroButtonIdError { get; set; } = "Button Id is not valid. Valid values are 0 to 79";
}