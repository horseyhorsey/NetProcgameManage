using Horsesoft.Procgame.AssetModel;
using Horsesoft.Procgame.Manager.Base.Constants;
using Horsesoft.Procgame.Manager.Base.Interfaces;
using Horsesoft.Procgame.Manager.Base.PrismBase;
using Prism.Commands;
using Prism.Regions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Windows.Data;
using System.Windows.Media;

namespace Horsesoft.Procgame.AssetServices.ViewModels
{
    public class FontViewModel : ViewModelBase
    {

        public DelegateCommand<string> NavigateCommand { get; private set; }
        public DelegateCommand AddFontCommand { get; private set; } 

        #region Services
        private IAssetRepo _assetRepo;
        private IRegionManager _regionManager;
        #endregion

        private ICollectionView fonts;
        public ICollectionView Fonts
        {
            get { return fonts; }
            set { SetProperty(ref fonts, value); }
        }

        private ICollectionView fontstyles;
        public ICollectionView FontStyles
        {
            get { return fontstyles; }
            set { SetProperty(ref fontstyles, value); }
        }

        private ICollectionView folderFonts;
        public ICollectionView FolderFonts
        {
            get { return folderFonts; }
            set { SetProperty(ref folderFonts, value); }
        }
        

        private Color testColor;
        public Color TestColor
        {
            get { return testColor; }
            set
            {
                SetProperty(ref testColor, value);
                //var ints = new List<int>() { testColor.R, testColor.G, testColor.B };
                //Ui.text.color = ints;
            }
        }

        public FontViewModel(IAssetRepo assetRepo, IRegionManager regionManager)
        {
            _assetRepo = assetRepo;

            Fonts = new ListCollectionView(_assetRepo.Font.SdlFonts);

            CreateStyleList();

            _regionManager = regionManager;

            NavigateCommand = new DelegateCommand<string>(x =>
            {
                _regionManager.RequestNavigate(RegionNames.ContentRegion, x);
            });

            AddFontCommand = new DelegateCommand(() =>
            {

                var font = FolderFonts.CurrentItem as string;

                System.Windows.MessageBox.Show(font);
            });

            List<string> files = new List<string>();
            foreach (var item in Directory.EnumerateFiles("asset\\fonts","*.ttf"))
            {
                files.Add(Path.GetFileNameWithoutExtension(item));                
            }

            FolderFonts = new ListCollectionView(files);
        }

        private void CreateStyleList()
        {            
            var styleList = new List<StyleColor>();

            foreach (var style in _assetRepo.Style.FontStyles)
            {
                styleList.Add(new StyleColor()
                {
                    InteriorColor = MediaColorConvert(style.InteriorColor),
                    LineColor = MediaColorConvert(style.LineColor),
                    Name = style.Name,
                    LineWidth = style.LineWidth
                });
            }

            FontStyles = new ListCollectionView(styleList);
        }

        public byte[] ColorConvert(string colorString)
        {
            var c = colorString.Split(',');
            byte[] color = new byte[c.Length];

            for (int i = 0; i < c.Length; i++)
            {
                byte result;
                byte.TryParse(c[i],out result);
                color[i] = result;
            }

            return color;
        }

        public Color MediaColorConvert(string colorString)
        {
            var c = colorString.Split(',');
            byte[] color = new byte[c.Length];

            for (int i = 0; i < c.Length; i++)
            {
                byte result;
                byte.TryParse(c[i], out result);
                color[i] = result;
            }

            var mediaColor = new Color()
            {
                R = color[0],
                G = color[1],
                B = color[2],
                A = 255
            };

            return mediaColor;
        }

    }

    public class StyleColor
    {
        public string Name { get; set; }
        public Color InteriorColor { get; set; }
        public Color LineColor { get; set; }
        public int LineWidth { get; set; }
    }
}
