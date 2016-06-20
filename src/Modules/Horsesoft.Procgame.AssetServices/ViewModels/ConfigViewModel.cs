using Horsesoft.Procgame.Config;
using Horsesoft.Procgame.Manager.Base.Constants;
using Horsesoft.Procgame.Manager.Base.Events;
using Horsesoft.Procgame.Manager.Base.Interfaces;
using Horsesoft.Procgame.Manager.Base.PrismBase;
using Horsesoft.Procgame.Manager.Base.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace Horsesoft.Procgame.AssetServices.ViewModels
{
    public class ConfigViewModel : ViewModelBase
    {

        private IRegionManager _regionManager;
        private IGameConfigSvc _gameConfig;
        private IEventAggregator _eventAggregator;

        public DelegateCommand<string> NavigateCommand { get; private set; }
        public DelegateCommand SaveConfigCommand { get; private set; }

        private GameConfig config;        
        public GameConfig Config
        {
            get { return config; }
            set { SetProperty(ref config, value); }
        }

        public ConfigViewModel(IRegionManager regionManager,
            IGameConfigSvc gameConfig)
        {
            _regionManager = regionManager;
            _gameConfig = gameConfig;            

            NavigateCommand = new DelegateCommand<string>(x =>
            {
                _regionManager.RequestNavigate(RegionNames.ContentRegion, x);
            });

            SaveConfigCommand = new DelegateCommand(() =>
            {
                if (_gameConfig != null) _gameConfig.SaveGameConfig(Config);                    
            });

            _gameConfig.LoadGameConfig("config.yaml");

            Config = _gameConfig.Config;

        }
    }
}
