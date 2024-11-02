using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;

namespace PW.VoicemeeterPlugin.Models;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public sealed class AdditionalVariablesModel : ISerializableConfiguration
{
    public List<VmIoOptions> Options { get; init; } = [];

    public string Serialize()
    {
        return JsonSerializer.Serialize(this);
    }

    public static AdditionalVariablesModel Deserialize(string config)
    {
        return ISerializableConfiguration.Deserialize<AdditionalVariablesModel>(config);
    }

    public override string ToString()
    {
        return Options.ToString() ?? string.Empty;
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}