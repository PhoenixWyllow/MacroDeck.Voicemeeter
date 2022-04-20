using PW.VoicemeeterPlugin.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PW.VoicemeeterPlugin.Services.Voicemeeter
{
    public sealed partial class Control
    {
        private static class VoicemeeterCommand
        {
            public const string Shutdown = "Command.Shutdown";
            public const string Restart = "Command.Restart";
            public const string Show = "Command.Show";
            public const string Eject = "Command.Eject";
            public const string Reset = "Command.Reset";
            public const string Save = "Command.Save";
            public const string Load = "Command.Load";
        }

        /// <summary>
        /// Shutdown VoiceMeeter
        /// </summary>
        /// <param name="voicemeeterType">The Voicemeeter program to run</param>
        public void Shutdown() => SetParameter(VoicemeeterCommand.Shutdown, 1);

        /// <summary>
        /// Restart Audio Engine
        /// </summary>
        public void Restart() => SetParameter(VoicemeeterCommand.Restart, 1);

        /// <summary>
        /// Show Voicemeeter
        /// </summary>
        public void Show() => SetParameter(VoicemeeterCommand.Show, 1);

        /// <summary>
        /// Eject Cassette 
        /// </summary>
        public void Eject() => SetParameter(VoicemeeterCommand.Eject, 1);

        /// <summary>
        /// Reset all configuration 
        /// </summary>
        public void Reset() => SetParameter(VoicemeeterCommand.Reset, 1);

        /// <summary>
        /// Load a configuation by file name (xml)
        /// </summary>
        /// <param name="configurationFileName">Full path to file</param>
        public void Load(string configurationFileName) => SetTextParameter(VoicemeeterCommand.Load, configurationFileName);

        /// <summary>
        /// Save a configuration to the given file name (xml)
        /// </summary>
        /// <param name="configurationFileName">Full path to file</param>
        public void Save(string configurationFileName) => SetTextParameter(VoicemeeterCommand.Save, configurationFileName);
    }
}
