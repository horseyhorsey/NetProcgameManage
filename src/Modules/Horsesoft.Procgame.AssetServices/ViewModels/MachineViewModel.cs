using Horsesoft.Procgame.Manager.Base.Constants;
using Horsesoft.Procgame.Manager.Base.PrismBase;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using ProcConfig = Procgame.Config;

namespace Horsesoft.Procgame.AssetServices.ViewModels
{
    public class MachineViewModel : ViewModelBase
    {

        private IRegionManager _regionManager;
        public DelegateCommand<string> NavigateCommand { get; private set; }

        private ProcConfig.MachineConfiguration config;
        public ProcConfig.MachineConfiguration Config
        {
            get { return config; }
            set { SetProperty(ref config, value); }
        }

        public MachineViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            Config = new ProcConfig.MachineConfiguration();

            var configPath = Path.GetFullPath("machine.json");
            try
            {
                if (File.Exists(configPath))
                    Config = ProcConfig.MachineConfiguration.FromFile(configPath);
                
            }
            catch (Exception ex)
            {

                System.Windows.MessageBox.Show(configPath + " " +  ex.Message);
            }
                               
            NavigateCommand = new DelegateCommand<string>(x =>
            {
                _regionManager.RequestNavigate(RegionNames.ContentRegion, x);
            });

        }
    }
}
