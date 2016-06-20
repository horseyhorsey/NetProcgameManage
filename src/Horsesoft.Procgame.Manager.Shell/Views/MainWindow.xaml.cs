using Horsesoft.Procgame.Manager.Base.Constants;
using MahApps.Metro.Controls;
using Prism.Regions;
using System.Windows;

namespace Horsesoft.Procgame.Manager.Shell.Views
{

    public partial class MainWindow : MetroWindow
    {
        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();

            if (regionManager != null)
                SetRegionManager(regionManager, flyoutsControlRegion, RegionNames.FlyoutRegion);

        }

        void SetRegionManager(IRegionManager regionManager, DependencyObject regionTarget, string regionName)
        {
            RegionManager.SetRegionName(regionTarget, regionName);
            RegionManager.SetRegionManager(regionTarget, regionManager);
        }

    }
}
