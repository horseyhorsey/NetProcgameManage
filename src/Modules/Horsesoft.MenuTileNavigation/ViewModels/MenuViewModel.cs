using Horsesoft.Procgame.Manager.Base.Constants;
using Horsesoft.Procgame.Manager.Base.PrismBase;
using Prism.Commands;
using Prism.Regions;

namespace Horsesoft.MenuTileNavigation.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private IRegionManager _regionManager;

        public DelegateCommand<string> NavigateCommand { get; private set; } 

        public MenuViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            NavigateCommand = new DelegateCommand<string>(viewName =>
            {
                _regionManager.RequestNavigate(RegionNames.ContentRegion, viewName);
            });
        }
    }
}
