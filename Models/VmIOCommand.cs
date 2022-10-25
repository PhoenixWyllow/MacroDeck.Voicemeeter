using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace PW.VoicemeeterPlugin.Models
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public sealed class VmIoCommand : IEquatable<VmIoCommand>
    {
        public Commands? CommandType { get; init; }
        public bool RequiresValue { get; init; }

        public override bool Equals(object obj)
        {
            return obj is VmIoCommand command && Equals(command);
        }

        public bool Equals(VmIoCommand other)
        {
            return CommandType == other?.CommandType;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CommandType);
        }

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
