using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace PW.VoicemeeterPlugin.Models
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class VmIOCommand : IEquatable<VmIOCommand>
    {
        public Commands? CommandType { get; set; }
        public bool RequiresValue { get; set; }

        public override bool Equals(object obj)
        {
            return obj is VmIOCommand command && Equals(command);
        }

        public bool Equals(VmIOCommand other)
        {
            return CommandType == other?.CommandType;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CommandType);
        }

        //public static bool operator ==(VmIOCommand left, VmIOCommand right)
        //{
        //    return left.Equals(right);
        //}

        //public static bool operator !=(VmIOCommand left, VmIOCommand right)
        //{
        //    return !(left == right);
        //}

        public override string ToString()
        {
            return CommandType.GetType()
                          .GetMember(CommandType.ToString())
                          .FirstOrDefault()?.GetCustomAttribute<DisplayAttribute>()
                          .GetDescription() ?? CommandType.ToString();
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }

    public enum Commands
    {
        [Display(Description = VoicemeeterCommand.Shutdown)]
        Shutdown,
        [Display(Description = VoicemeeterCommand.Restart)]
        Restart,
        [Display(Description = VoicemeeterCommand.Show)]
        Show,
        [Display(Description = VoicemeeterCommand.Reset)]
        Reset,
        [Display(Description = VoicemeeterCommand.Save)]
        ConfigSave,
        [Display(Description = VoicemeeterCommand.Load)]
        ConfigLoad,
        [Display(Description = VoicemeeterCommand.Eject)]
        RecorderEject,
        [Display(Description = VoicemeeterCommand.RecLoad)]
        RecorderLoad
    }

    public static class VoicemeeterCommand
    {
        public const string Shutdown = "Command.Shutdown";
        public const string Restart = "Command.Restart";
        public const string Show = "Command.Show";
        public const string Reset = "Command.Reset";
        public const string Save = "Command.Save";
        public const string Load = "Command.Load";
        public const string Eject = "Command.Eject";
        public const string RecLoad = "Recorder.Load";
    }
}
