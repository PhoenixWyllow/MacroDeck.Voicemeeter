using System;
using System.Diagnostics;
using System.Text.Json;

namespace PW.VoicemeeterPlugin.Models;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public sealed class DeviceConfigModel : ISerializableConfiguration
{
    public string? Name { get; set; }
    public string? Action { get; set; }
    public VmIoOptions? Option { get; set; }
    public float Value { get; set; } = 0;

    public string Serialize()
    {
        return JsonSerializer.Serialize(this);
    }

    public static DeviceConfigModel Deserialize(string config)
    {
        return ISerializableConfiguration.Deserialize<DeviceConfigModel>(config);
    }

    public override string ToString()
    {
        string value = Value switch
        {
            0 => string.Empty,
            < 0 => $" -={Math.Abs(Value)}",
            _ => $" +={Value}"
        };
        return $"{Name}: {Action}{value}";
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}