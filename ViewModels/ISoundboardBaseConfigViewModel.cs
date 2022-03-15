using PW.VoicemeeterPlugin.Models;

namespace PW.VoicemeeterPlugin.ViewModels
{
    public interface ISoundboardBaseConfigViewModel
    {
        protected ISerializableConfiguration SerializableConfiguration { get; }

        void SetConfig();

        void SaveConfig();
    }
}
