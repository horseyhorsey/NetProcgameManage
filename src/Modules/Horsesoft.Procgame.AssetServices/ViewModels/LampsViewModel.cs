using Horsesoft.Procgame.Manager.Base.Constants;
using Horsesoft.Procgame.Manager.Base.Interfaces;
using Horsesoft.Procgame.Manager.Base.PrismBase;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace Horsesoft.Procgame.AssetServices.ViewModels
{
    public class LampsViewModel : ViewModelBase
    {
        private ICollectionView lampShows;
        public ICollectionView LampShows
        {
            get { return lampShows; }
            set { SetProperty(ref lampShows, value); }
        }

        private IRegionManager _regionManager;
        private IAssetRepo _assetRepo;

        public DelegateCommand<string> NavigateCommand { get; private set; }

        public LampsViewModel(IRegionManager regionManager, IAssetRepo assetRepo)
        {
            _regionManager = regionManager;
            _assetRepo = assetRepo;

            LampShows = new ListCollectionView(_assetRepo.Lamp.Lampshows);

            NavigateCommand = new DelegateCommand<string>(x =>
            {
                _regionManager.RequestNavigate(RegionNames.ContentRegion, x);
            });

        }
    }
}
