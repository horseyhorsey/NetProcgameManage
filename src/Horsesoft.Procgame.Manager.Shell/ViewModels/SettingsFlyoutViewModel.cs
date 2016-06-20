using Prism.Commands;
using System.Collections.ObjectModel;
using Horsesoft.Procgame.Manager.Shell.Models;
using Horsesoft.Procgame.Manager.Base.PrismBase;

namespace Horsesoft.Procgame.Manager.Shell.ViewModels
{
    public class SettingsFlyoutViewModel : ViewModelBase
    {
        public ObservableCollection<string> GuiThemes { get; set; }

        public DelegateCommand SaveSettingsCommand { get; private set; }

        public SettingsFlyoutViewModel()
        {
            CurrentThemeColor = Properties.Settings.Default.GuiColor;
            IsDarkTheme = Properties.Settings.Default.GuiTheme;

            //Setup themes for combobox binding            
            GuiThemes = new ObservableCollection<string>(MahAppTheme.AvailableThemes);

            SaveSettingsCommand = new DelegateCommand(SaveSettings);
        }

        #region Theme Properties            
        private string _currentThemeColor;
        public string CurrentThemeColor
        {
            get { return _currentThemeColor; }
            set
            {
                SetProperty(ref _currentThemeColor, value);
                changeGuiTheme();
            }
        }

        private bool _isDarkTheme;
        public bool IsDarkTheme
        {
            get { return _isDarkTheme; }
            set
            {
                SetProperty(ref _isDarkTheme, value);
                changeGuiTheme();
            }
        }
        #endregion

        #region Theme Methods
        private void changeGuiTheme()
        {
            string darkOrLight = string.Empty;
            if (IsDarkTheme)
                darkOrLight = "BaseDark";
            else
                darkOrLight = "BaseLight";

            // now set the theme
            try
            {
                MahApps.Metro.ThemeManager.ChangeAppStyle(System.Windows.Application.Current,
                            MahApps.Metro.ThemeManager.GetAccent(CurrentThemeColor),
                            MahApps.Metro.ThemeManager.GetAppTheme(darkOrLight));
            }
            catch (System.Exception) { }

        }

        public void SaveSettings()
        {
            Properties.Settings.Default.GuiColor = CurrentThemeColor;
            Properties.Settings.Default.GuiTheme = IsDarkTheme;
            Properties.Settings.Default.Save();

        }
        #endregion
    }
}
