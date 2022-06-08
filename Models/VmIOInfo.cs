using System;
using System.Collections.Generic;
using System.Text;

namespace PW.VoicemeeterPlugin.Models
{
    public class VmIOInfo : IEquatable<VmIOInfo>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public VmIOType Type { get; set; }
        public bool IsPhysical { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as VmIOInfo);
        }

        public bool Equals(VmIOInfo other)
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
            return string.Format("{0} \"{1}\"", Id, Name);
        }

        public static bool operator ==(VmIOInfo left, VmIOInfo right)
        {
            return EqualityComparer<VmIOInfo>.Default.Equals(left, right);
        }

        public static bool operator !=(VmIOInfo left, VmIOInfo right)
        {
            return !(left == right);
        }
    }

    public enum VmIOType { Strip, Bus }
}
