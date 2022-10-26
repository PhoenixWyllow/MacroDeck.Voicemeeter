using System.Diagnostics;
using System.Text.Json;

namespace PW.VoicemeeterPlugin.Models;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public sealed class CommandActionConfigModel : ISerializableConfiguration
{
    public VmIoCommand Command { get; set; } = new();
    public string CommandValue { get; set; } = string.Empty;

    public string Serialize()
    {
        return JsonSerializer.Serialize(this);
    }
    public static CommandActionConfigModel Deserialize(string config)
    {
        return ISerializableConfiguration.Deserialize<CommandActionConfigModel>(config);
    }

    public override string ToString()
    {
        string val = Command.ToString();
        if (!string.IsNullOrWhiteSpace(CommandValue))
        {
            val += " = " + CommandValue;
        }
        return val;
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}