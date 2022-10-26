using SuchByte.MacroDeck.Variables;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace PW.VoicemeeterPlugin.Models;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public sealed class VmIoOptions
{
    public string Id { get; set; }
    public string Option { get; set; }
    public VariableType Type { get; init; }

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

    public override bool Equals(object obj)
    {
        return obj is VmIoOptions options &&
               AsParameter == options.AsParameter;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(AsParameter);
    }

    public static bool operator ==(VmIoOptions left, VmIoOptions right)
    {
        return EqualityComparer<VmIoOptions>.Default.Equals(left, right);
    }

    public static bool operator !=(VmIoOptions left, VmIoOptions right)
    {
        return !(left == right);
    }
}