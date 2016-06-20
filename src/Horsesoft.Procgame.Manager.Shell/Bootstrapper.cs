using Microsoft.Practices.Unity;
using Prism.Unity;
using Horsesoft.Procgame.Manager.Shell.Views;
using System.Windows;
using Prism.Regions;
using Prism.Modularity;
using Horsesoft.Procgame.Manager.Base;
using Horsesoft.Procgame.Manager.Base.Services;
using Horsesoft.Procgame.Manager.Base.Constants;
using Horsesoft.Procgame.AssetServices;
using Horsesoft.MenuTileNavigation;
using Horsesoft.Procgame.AssetServices.Service;
using Horsesoft.Procgame.Manager.Base.Interfaces;
using Horsesoft.Procgame.AssetServices.Views;
using MahApps.Metro.Controls.Dialogs;

namespace Horsesoft.Procgame.Manager.Shell
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            // Register view
            var regionManager = this.Container.Resolve<IRegionManager>();
            if (regionManager != null)
            {                
                regionManager.RegisterViewWithRegion(RegionNames.FlyoutRegion, typeof(SettingsFlyout));
            }

            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            //base.ConfigureModuleCatalog();
            var moduleCatalog = (ModuleCatalog)ModuleCatalog;

            moduleCatalog.AddModule(typeof(MenuTileNavigationModule));
            moduleCatalog.AddModule(typeof(AssetServicesModule));
            

        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<IApplicationCommands, ApplicationCommandsProxy>();
            Container.RegisterInstance<IFlyoutService>(Container.Resolve<FlyoutService>());
            Container.RegisterType<IDialogCoordinator, DialogCoordinator>();            

            Container.RegisterType<IAssetRepo, AssetRepo>(
            new ContainerControlledLifetimeManager());

            Container.RegisterType<IGameConfigSvc, GameConfigSvc>(
            new ContainerControlledLifetimeManager());

            //IGameConfig g = new GameConfig();

            //Container.RegisterInstance<ISettingsRepo>(Container.Resolve<SettingsRepo>());
            //Container.RegisterTypeForNavigation<DatabaseDetailsView>("DatabaseDetailsView");
        }
    }
}
