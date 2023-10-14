using AtgDev.Voicemeeter.Types;
using PW.VoicemeeterPlugin.Models;
using SuchByte.MacroDeck.Variables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PW.VoicemeeterPlugin.Services.Voicemeeter;

internal static class AvailableValues
{
    public static VoicemeeterType ConnectedType { get; set; }
    public static int MaxStrips => ConnectedType switch
    {
        VoicemeeterType.Standard => 3,
        VoicemeeterType.Banana => 5,
        VoicemeeterType.Potato => 8,
        VoicemeeterType.Potato64 => 8,
        _ => 0,
    };

    public static int MaxBuses => ConnectedType switch
    {
        VoicemeeterType.Standard => 2,
        VoicemeeterType.Banana => 5,
        VoicemeeterType.Potato => 8,
        VoicemeeterType.Potato64 => 8,
        _ => 0,
    };

    public static int MaxPhysicalStrips => ConnectedType switch
    {
        VoicemeeterType.Standard => 2,
        VoicemeeterType.Banana => 3,
        VoicemeeterType.Potato => 5,
        VoicemeeterType.Potato64 => 5,
        _ => 0,
    };

    public static int MaxPhysicalBuses => ConnectedType switch
    {
        VoicemeeterType.Standard => 1,
        VoicemeeterType.Banana => 3,
        VoicemeeterType.Potato => 5,
        VoicemeeterType.Potato64 => 5,
        _ => 0,
    };

    internal static void Reset()
    {
        IoInfo = null;
        IoOptions = null;
        IoCommands = null;
    }

    public static List<VmIoInfo>? IoInfo { get; private set; }

    internal static void InitIoInfo(AtgDev.Voicemeeter.RemoteApiExtender vmrApi)
    {
        void AddChannel(ICollection<VmIoInfo> ioInfo, VmIoType type, int num)
        {
            var channelId = $"{type}({num})";
            _ = vmrApi.GetParameter($"{channelId}.Label", out string label);
            VmIoInfo channel = new(channelId, label, type, num < (type == VmIoType.Strip ? MaxPhysicalStrips : MaxPhysicalBuses));
            ioInfo.Add(channel);
        }

        if (IoInfo is null)
        {
            List<VmIoInfo> ioInfo = new();
            for (int i = 0; i < MaxStrips; i++)
            {
                AddChannel(ioInfo, VmIoType.Strip, i);
            }

            for (int i = 0; i < MaxBuses; i++)
            {
                AddChannel(ioInfo, VmIoType.Bus, i);
            }


            ioInfo.Add(new(nameof(VmIoType.Recorder), string.Empty, VmIoType.Recorder, false));
            IoInfo = ioInfo;
        }
    }

    public static List<VmIoOptions>? IoOptions { get; private set; }

    internal static void InitIoOptions()
    {
        void AddOption(ICollection<VmIoOptions> ioOptions, string channelId, string option, VariableType type)
        {
            ioOptions.Add(new(channelId, option, type));
        }

        if (IoOptions is null)
        {
            int maxPhysical = MaxPhysicalBuses;
            List<VmIoOptions> ioOptions = new();
            if (IoInfo is null)
            {
                return;
            }
            foreach (var channel in IoInfo)
            {
                string channelId = channel.Id;
                if (channel.Type != VmIoType.Recorder)
                {
                    AddOption(ioOptions, channelId, "Mute", VariableType.Bool);
                    AddOption(ioOptions, channelId, "Mono", VariableType.Bool);
                    AddOption(ioOptions, channelId, "Gain", VariableType.Float);
                    AddOption(ioOptions, channelId, "Label", VariableType.String);
                    if (channel.Type == VmIoType.Strip)
                    {
                        if (channel.IsPhysical)
                        {
                            AddOption(ioOptions, channelId,  "Solo", VariableType.Bool);
                        }

                        for (int i = 1; i <= MaxBuses; i++)
                        {
                            string busOut = i <= maxPhysical ? $"A{i}" : $"B{i - maxPhysical}";
                            AddOption(ioOptions, channelId, busOut, VariableType.Bool);
                        }
                    }
                    else
                    {
                        if (IsBananaOrPotato)
                        {
                            AddOption(ioOptions, channelId,  "EQ.on", VariableType.Bool);

                            if (ConnectedType == VoicemeeterType.Potato || ConnectedType == VoicemeeterType.Potato64)
                            {
                                AddOption(ioOptions, channelId,  "Sel", VariableType.Bool);
                            }
                        }
                    }
                }
                else
                {
                    //Add Recorder options
                    if (IsBananaOrPotato)
                    {
                        for (int i = 1; i <= MaxBuses; i++)
                        {
                            string busOut = i <= maxPhysical ? $"A{i}" : $"B{i - maxPhysical}";
                            AddOption(ioOptions, channelId, busOut, VariableType.Bool);
                        }
                        AddOption(ioOptions, channelId, "Stop", VariableType.Bool);
                        AddOption(ioOptions, channelId, "Play", VariableType.Bool);
                        AddOption(ioOptions, channelId, "Record", VariableType.Bool);
                        AddOption(ioOptions, channelId, "Pause", VariableType.Bool);
                        AddOption(ioOptions, channelId, "Gain", VariableType.Float);
                    }
                }
            }

            //Additional variables
            var config = SuchByte.MacroDeck.Plugins.PluginConfiguration.GetValue(PluginInstance.Plugin, nameof(AdditionalVariablesModel));
            var variables = string.IsNullOrEmpty(config) ? null : AdditionalVariablesModel.Deserialize(config);
            if (variables != null)
            {
                foreach (var addedVariable in variables.Options)
                {
                    if (!ioOptions.Contains(addedVariable))
                    {
                        ioOptions.Add(addedVariable);
                    }
                }
            }
            IoOptions = ioOptions;
        }
    }
    
    private static bool IsBananaOrPotato => ConnectedType is VoicemeeterType.Banana or VoicemeeterType.Potato or VoicemeeterType.Potato64;

    public static List<VmIoCommand>? IoCommands { get; private set; }

    internal static void InitIoCommands()
    {
        if (IoCommands is null)
        {
            var commands = Enum.GetValues(typeof(Commands));
            IoCommands = (from Commands command in commands
                select new VmIoCommand(command, command is Commands.ConfigLoad or Commands.ConfigSave or Commands.RecorderLoad)).ToList();
        }
    }
}