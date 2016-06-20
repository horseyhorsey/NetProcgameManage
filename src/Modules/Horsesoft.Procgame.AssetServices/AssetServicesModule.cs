using Horsesoft.Procgame.AssetServices.Views;
using Horsesoft.Procgame.Manager.Base.Constants;
using Horsesoft.Procgame.Manager.Base.PrismBase;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace Horsesoft.Procgame.AssetServices
{
    public class AssetServicesModule : PrismBaseModule
    {
        IRegionManager _regionManager;

        public AssetServicesModule(IUnityContainer unityContainer, IRegionManager regionManager) :
            base(unityContainer, regionManager)
        {
            _regionManager = regionManager;
        }

        public override void Initialize()
        {
            base.Initialize();

            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(AnimationView));
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(FontView));
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(SoundView));
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(LampsView));
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(MachineView));
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ConfigView));
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(VideoCopyView));

            _regionManager.Regions[RegionNames.ContentRegion].RequestNavigate("MenuView");

        }

    }
}