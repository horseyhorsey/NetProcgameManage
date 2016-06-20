using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Horsesoft.Procgame.Manager.Base.PrismBase;

namespace Horsesoft.Procgame.Manager.Shell.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        #region Properties
        private string _title = "Proc Manage";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        #endregion

        #region Commands        
        public DelegateCommand<string> NavigateCommand { get; private set; }
        #endregion

        #region Events
        private IEventAggregator _eventAggregator;
        #endregion

        private IRegionManager _regionManager;

        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        #region Methods
        private void Navigate(string uri)
        {

        }
        #endregion

    }
}
