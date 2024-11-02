using System;

namespace PW.VoicemeeterPlugin.Models;

public sealed record VmIoInfo(string Id, string Name, VmIoType Type, bool IsPhysical)
{

    public bool Equals(VmIoInfo? other)
    {
        return other is not null && Id == other.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }

    public override string ToString()
    {
        return string.IsNullOrEmpty(Name) ? Id : $"{Id} \"{Name}\"";
    }
}

public enum VmIoType { Strip, Bus, Recorder }