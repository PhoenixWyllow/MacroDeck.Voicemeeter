using PW.VoicemeeterPlugin.Models;

namespace PW.VoicemeeterPlugin.ViewModels
{
    public interface ISavableConfigViewModel
    {
        protected ISerializableConfiguration SerializableConfiguration { get; }

        void SetConfig();

        void SaveConfig();
    }
}
