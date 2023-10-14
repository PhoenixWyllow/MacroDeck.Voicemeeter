using System.Linq;
using PW.VoicemeeterPlugin.Models;
using PW.VoicemeeterPlugin.Services.Voicemeeter;
using SuchByte.MacroDeck.Plugins;
using SuchByte.MacroDeck.Variables;

namespace PW.VoicemeeterPlugin.ViewModels;

internal class DeviceSliderViewModel : DeviceSelectorViewModel
{
    public DeviceSliderViewModel(PluginAction action) : base(action)
    {
    }

    protected override string[] GetAvailableActionsForDevice(VmIoInfo? device)
    {
        return AvailableValues.IoOptions!
            .Where(opt => opt.Id.Equals(device?.Id) && opt.Type == VariableType.Float)
            .Select(opt => opt.Option)
            .ToArray();
    }
}