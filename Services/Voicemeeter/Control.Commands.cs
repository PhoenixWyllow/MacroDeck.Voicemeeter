using PW.VoicemeeterPlugin.Models;

namespace PW.VoicemeeterPlugin.Services.Voicemeeter;

public sealed partial class Control
{
    /// <summary>
    /// Shutdown VoiceMeeter
    /// </summary>
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
    /// Reset all configuration 
    /// </summary>
    public void Reset() => SetParameter(VoicemeeterCommand.Reset, 1);

    /// <summary>
    /// Load a configuration by file name (xml)
    /// </summary>
    /// <param name="configurationFileName">Full path to file</param>
    public void Load(string configurationFileName) => SetTextParameter(VoicemeeterCommand.Load, configurationFileName);

    /// <summary>
    /// Save a configuration to the given file name (xml)
    /// </summary>
    /// <param name="configurationFileName">Full path to file</param>
    public void Save(string configurationFileName) => SetTextParameter(VoicemeeterCommand.Save, configurationFileName);

    /// <summary>
    /// Eject Cassette 
    /// </summary>
    public void Eject() => SetParameter(VoicemeeterCommand.Eject, 1);

    /// <summary>
    /// Load Cassette 
    /// </summary>
    public void RecLoad(string fileName) => SetTextParameter(VoicemeeterCommand.RecLoad, fileName);
}