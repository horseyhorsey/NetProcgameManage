using Horsesoft.Procgame.Manager.Base.Constants;
using Horsesoft.Procgame.Manager.Base.Events;
using Horsesoft.Procgame.Manager.Base.Interfaces;
using Horsesoft.Procgame.Manager.Base.PrismBase;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System.ComponentModel;
using System.Windows.Data;

namespace Horsesoft.Procgame.AssetServices.ViewModels
{
    public class SoundViewModel : ViewModelBase
    {
        private IRegionManager _regionManager;
        private IAssetRepo _assetRepo;

        public DelegateCommand<string> NavigateCommand { get; private set; }

        private ICollectionView music;
        public ICollectionView Music
        {
            get { return music; }
            set { SetProperty(ref music, value); }
        }

        private ICollectionView voice;
        public ICollectionView Voice
        {
            get { return voice; }
            set { SetProperty(ref voice, value); }
        }

        private ICollectionView sfx;
        private IEventAggregator _eventAggregator;

        public ICollectionView Sfx
        {
            get { return sfx; }
            set { SetProperty(ref sfx, value); }
        }

        public SoundViewModel(IRegionManager regionManager, IAssetRepo assetRepo, IEventAggregator evtAgg)
        {
            _regionManager = regionManager;
            _assetRepo = assetRepo;
            _eventAggregator = evtAgg;

            Music = new ListCollectionView(_assetRepo.Audio.Music);
            Voice = new ListCollectionView(_assetRepo.Audio.Voice);
            Sfx = new ListCollectionView(_assetRepo.Audio.Sfx);

            NavigateCommand = new DelegateCommand<string>(x =>
            {
                _regionManager.RequestNavigate(RegionNames.ContentRegion, x);
            });

            _eventAggregator.GetEvent<AssetUpdateEvent>().Subscribe(X =>
            {
                Voice.Refresh();
            });

        }
    }
}
