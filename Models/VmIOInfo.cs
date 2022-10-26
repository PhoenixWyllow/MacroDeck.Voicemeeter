using System;
using System.Collections.Generic;

namespace PW.VoicemeeterPlugin.Models;

public sealed class VmIoInfo : IEquatable<VmIoInfo>
{
    public string Id { get; init; }
    public string Name { get; init; }
    public VmIoType Type { get; init; }
    public bool IsPhysical { get; init; }

    public override bool Equals(object obj)
    {
        return Equals(obj as VmIoInfo);
    }

    public bool Equals(VmIoInfo other)
    {
        return !(other is null) &&
               Id == other.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }

    public override string ToString()
    {
        if (string.IsNullOrEmpty(Name))
        {
            return Id;
        }
        return $"{Id} \"{Name}\"";
    }

    public static bool operator ==(VmIoInfo left, VmIoInfo right)
    {
        return EqualityComparer<VmIoInfo>.Default.Equals(left, right);
    }

    public static bool operator !=(VmIoInfo left, VmIoInfo right)
    {
        return !(left == right);
    }
}

public enum VmIoType { Strip, Bus, Recorder }