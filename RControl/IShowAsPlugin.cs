using System.ComponentModel;

namespace Gnllk.RControl
{
    public interface IShowAsPlugin : IComponent
    {
        string GetName();

        string GetDescription();

        string GetConfig();

        bool ShouldShowAs(string key, byte[] data);

        void OnShowAs(string key, byte[] data);

        void OnBlur();

        void OnAppClosing();

        void OnSetConfig(string config);
    }
}
