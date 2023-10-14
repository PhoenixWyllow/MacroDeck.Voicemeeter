using PW.VoicemeeterPlugin.Models;
using PW.VoicemeeterPlugin.Services.Voicemeeter;
using SuchByte.MacroDeck.Plugins;
using SuchByte.MacroDeck.Variables;
using System.Linq;

namespace PW.VoicemeeterPlugin.ViewModels;

internal class DeviceToggleViewModel : DeviceSelectorViewModel
{
    public DeviceToggleViewModel(PluginAction action) : base(action)
    {
    }

    protected override string[] GetAvailableActionsForDevice(VmIoInfo? device)
    {
        return AvailableValues.IoOptions!.Where(opt => opt.Id.Equals(device?.Id) && opt.Type == VariableType.Bool)
            .Select(opt => opt.Option)
            .ToArray();
    }
}