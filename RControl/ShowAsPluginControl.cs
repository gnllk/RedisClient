using System.Windows.Forms;

namespace Gnllk.RControl
{
    public abstract partial class ShowAsPluginControl : UserControl
    {
        public ShowAsPluginControl()
        {
            InitializeComponent();
        }

        public abstract string GetName();

        public abstract string GetDescription();

        public abstract string GetConfig();

        public abstract bool ShouldShowAs(ShowData data);

        public abstract void OnShowAs(ShowData data);

        public abstract void OnBlur();

        public abstract void OnAppClosing();

        public abstract void OnSetConfig(string config);
    }

    public class ShowData
    {
        public string Id { get; set; }

        public string DbName { get; set; }

        public string Key { get; set; }

        public byte[] Value { get; set; }

        public override int GetHashCode()
        {
            return Id == null ? base.GetHashCode() : Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj == null ? false : GetHashCode() == obj.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0},{1}", DbName, Key);
        }
    }
}
