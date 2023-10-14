using SuchByte.MacroDeck.Variables;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace PW.VoicemeeterPlugin.Models;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public sealed record VmIoOptions(string Id, string Option, VariableType Type)
{
    [JsonIgnore]
    public string AsParameter => $"{Id}.{Option}";

    [JsonIgnore]
    public string AsVariable => VariableManager.ConvertNameString($"vm_{Id.Replace("(", null).Replace(")", null)}_{Option}");

    private string GetDebuggerDisplay()
    {
        return $"{Type}: {AsParameter}";
    }

    public override string ToString()
    {
        return AsParameter;
    }

    public bool Equals(VmIoOptions? options)
    {
        return options is not null && AsParameter == options.AsParameter;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(AsParameter);
    }
}