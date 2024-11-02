using System.Diagnostics;
using System.Text.Json;

namespace PW.VoicemeeterPlugin.Models;

public enum ButtonType
{
    Push,
    TwoPositions,
}

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
internal class MacroButtonActionConfigModel : ISerializableConfiguration
{
    public int ButtonId { get; set; } = -1;

    public ButtonType ButtonType { get; set; }

    public string Serialize()
    {
        return JsonSerializer.Serialize(this);
    }

    public static MacroButtonActionConfigModel Deserialize(string config)
    {
        return ISerializableConfiguration.Deserialize<MacroButtonActionConfigModel>(config);
    }

    public override string ToString()
    {
        if (ButtonType == ButtonType.Push)
        {
            return $"Button {ButtonId}: Press";
        }
        else
        {
            return $"Button {ButtonId}: Toggle";
        }
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }

    public string AsVariable()
    {
        return GetVariable(ButtonId);
    }

    public static string GetVariable(int buttonId)
    {
        return $"vm_macrobutton{buttonId:d2}_pressed";
    }
}
