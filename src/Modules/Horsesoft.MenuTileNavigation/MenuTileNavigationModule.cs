using Horsesoft.MenuTileNavigation.Views;
using Horsesoft.Procgame.Manager.Base.Constants;
using Horsesoft.Procgame.Manager.Base.PrismBase;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace Horsesoft.MenuTileNavigation
{
    public class MenuTileNavigationModule : PrismBaseModule
    {
        IRegionManager _regionManager;

        public MenuTileNavigationModule(IUnityContainer unityContainer, IRegionManager regionManager) :
            base(unityContainer, regionManager)
        {
            _regionManager = regionManager;
        }

        public override void Initialize()
        {
            base.Initialize();

            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(MenuView));
        }
    }
}