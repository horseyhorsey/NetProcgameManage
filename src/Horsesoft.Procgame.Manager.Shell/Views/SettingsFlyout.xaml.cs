using Horsesoft.Procgame.Manager.Base.Constants;
using Horsesoft.Procgame.Manager.Base.Interfaces;

namespace Horsesoft.Procgame.Manager.Shell.Views
{
    /// <summary>
    /// Interaction logic for SettingsFlyout
    /// </summary>
    public partial class SettingsFlyout : IFlyoutView
    {
        public SettingsFlyout()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The flyout name
        /// </summary>
        public string FlyoutName
        {
            get { return FlyoutNames.SettingsFlyout; }
        }
    }
}
