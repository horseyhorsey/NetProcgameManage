using GongSolutions.Wpf.DragDrop;
using Horsesoft.Procgame.AssetModel;
using Horsesoft.Procgame.AssetServices.Dialog;
using Horsesoft.Procgame.AssetServices.Model;
using Horsesoft.Procgame.AssetServices.Service;
using Horsesoft.Procgame.AssetServices.Views;
using Horsesoft.Procgame.Manager.Base.Constants;
using Horsesoft.Procgame.Manager.Base.Events;
using Horsesoft.Procgame.Manager.Base.Interfaces;
using Horsesoft.Procgame.Manager.Base.PrismBase;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Horsesoft.Procgame.AssetServices.ViewModels
{
    public class AnimationViewModel : ViewModelBase, IDropTarget
    {
        public DelegateCommand<string> NavigateCommand { get; private set; }
        public DelegateCommand CloseAllPendingFileDropCommand { get; private set; }
        public DelegateCommand SaveCommand { get; private set; } 

        CustomDialog customDialog;

        #region Services
        private IAssetRepo _assetRepo;
        private IRegionManager _regionManager;
        private IDialogCoordinator _dialogService;
        private IGameConfigSvc _gameConfig;
        #endregion

        #region Properties
        private ICollectionView animations;
        public ICollectionView Animations
        {
            get { return animations; }
            set { SetProperty(ref animations, value); }
        }

        private ICollectionView videos;
        public ICollectionView Videos
        {
            get { return videos; }
            set { SetProperty(ref videos, value); }
        }

        private ImageSource gfxImage;
        public ImageSource GfxImage
        {
            get { return gfxImage; }
            set { SetProperty(ref gfxImage, value); }
        }

        private ConvertSettings convertSettings;
        public ConvertSettings ConvertSettings
        {
            get { return convertSettings; }
            set { SetProperty(ref convertSettings, value); }
        }

        private string _droppedFile = "";
        private IEventAggregator _eventAggregator;

        #endregion

        public AnimationViewModel(IAssetRepo assetRepo,
            IRegionManager regionManager,IEventAggregator eventAggregator,
            IDialogCoordinator dialogService, IGameConfigSvc gameConfig)
        {
            //< add key = "assets:AssetLocation" value = "..\asset" />
            var assetLocation = ConfigurationManager.AppSettings["assets:AssetLocation"].ToString();

            _regionManager = regionManager;            
            _dialogService = dialogService;
            _gameConfig = gameConfig;
            _assetRepo = assetRepo;
            _eventAggregator = eventAggregator;

            ConvertSettings = new ConvertSettings();

            _assetRepo.DeserializeAssets(assetLocation);

            Animations = new ListCollectionView(_assetRepo.Animation.Animations);
            Animations.MoveCurrentTo("");
            Animations.CurrentChanged += Animations_CurrentChanged;

            Videos = new ListCollectionView(_assetRepo.Animation.Videos);
            Videos.MoveCurrentTo("");
            Videos.CurrentChanged += Videos_CurrentChanged; ;

            NavigateCommand = new DelegateCommand<string>(x =>
            {
                _regionManager.RequestNavigate(RegionNames.ContentRegion, x);
            });

            CloseAllPendingFileDropCommand = new DelegateCommand(async () =>
            {
                //cancelPending = true;

                await _dialogService.HideMetroDialogAsync(this, customDialog);

                // cancelPending = false;
            });

            SaveCommand = new DelegateCommand(async () =>
            {
                await SaveDroppedFileAsync();

            });

            _eventAggregator.GetEvent<CloseCustomDialogAnimsEvent>().Subscribe(x =>
            {
                CloseDialogs();
            });


        }

        private void CloseDialogs()
        {
            _dialogService.HideMetroDialogAsync(this, customDialog);
        }

        private void Videos_CurrentChanged(object sender, EventArgs e)
        {
            var video = Videos.CurrentItem as Animations;

            if (video == null) return;

            Animations.MoveCurrentTo("");

            var fileName = Path.GetFileName(video.FileName);
            var ext = Path.GetExtension(fileName);
            var fullPath = Path.GetFullPath(_gameConfig.Config.ImagesPath + "\\" + video.FileName);

            try
            {
               // GfxImage = SetBitmapFromUri(new Uri(fullPath.MatchAndReplaceFileNameWithDigits()));
            }
            catch (System.Exception ex)
            {
                System.Windows.MessageBox.Show(video.FileName + " " + ex.Message);
            }
        }

        private void Animations_CurrentChanged(object sender, System.EventArgs e)
        {
            var anim = Animations.CurrentItem as Animations;

            if (anim == null) return;

            Videos.MoveCurrentTo("");

            var fileName = Path.GetFileName(anim.FileName);
            var ext = Path.GetExtension(fileName);
            var fullPath = Path.GetFullPath(_gameConfig.Config.ImagesPath + "\\" + anim.FileName);

            try
            {
                GfxImage = SetBitmapFromUri(new Uri(fullPath.MatchAndReplaceFileNameWithDigits()));
            }
            catch (System.Exception ex)
            {
                System.Windows.MessageBox.Show(anim.FileName + " " + ex.Message);
            }

        }

        /// <summary>
        /// Get imagesource from URI file link
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ImageSource SetBitmapFromUri(Uri source)
        {
            try
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = source;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                bitmap.EndInit();

                return bitmap;

            }
            catch (Exception)
            {
                return null;
            }

        }

        //IDropTarget
        public void DragOver(IDropInfo dropInfo)
        {
            var dragFileList = ((DataObject)dropInfo.Data).GetFileDropList().Cast<string>();
            dropInfo.Effects = dragFileList.Any(item =>
            {
                var extension = Path.GetExtension(item);
                return extension != null;
            }) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        public async void Drop(IDropInfo dropInfo)
        {
            var dragFileList = ((DataObject)dropInfo.Data).GetFileDropList().Cast<string>();
            dropInfo.Effects = dragFileList.Any(item =>
            {
                var extension = Path.GetExtension(item);
                return extension != null && extension.Equals(".*");
            }) ? DragDropEffects.Copy : DragDropEffects.None;

            foreach (var file in dragFileList)
            {
                switch (Path.GetExtension(file))
                {                    
                    case ".mp4":
                    case ".flv":
                    case ".avi":
                        _droppedFile = file;
                        var runCustom = await DroppedFileCustomDialogAsync(file);
                        if (runCustom)
                            await customDialog.WaitUntilUnloadedAsync();
                        //if (cancelPending)
                        //    break;
                        break;
                    default:
                        break;
                }
            }
        }

        private async Task<bool> DroppedFileCustomDialogAsync(string file)
        {
            MessageDialogResult result = await _dialogService.ShowMessageAsync(this, "", "",MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {                
                return false;
            }                        

            var settings = new MetroDialogSettings();
            settings.ColorScheme = MetroDialogColorScheme.Theme;

            var fileNoExt = Path.GetFileNameWithoutExtension(file);
            ConvertSettings.AudioName = fileNoExt;
            ConvertSettings.AnimationName = fileNoExt;

            var info = await MediaToolKit.GetVideoFormat(file);

            var infoSplit = info.Split(',');
            ConvertSettings.Fps = double.Parse(infoSplit[1]);

            customDialog = new CustomDialog() { Title = info };

            customDialog.Content = (VideoCopyView)_regionManager.Regions[RegionNames.ContentRegion].GetView("VideoCopyView");

            await _dialogService.ShowMetroDialogAsync(this, customDialog, settings);

            return true;

        }

        private async Task ShowFilesDialogsAsync(string droppedFile)
        {
            MetroDialogSettings settings = new MetroDialogSettings();
            settings.DialogMessageFontSize = 12;
            settings.DialogTitleFontSize = 12;

            var file = Path.GetFileNameWithoutExtension(droppedFile);
            settings.DefaultText = file;

            var result = await _dialogService.ShowInputAsync(this,
                droppedFile, @"path & name..eg: MyLayer/MyLayer", settings);

            if (result != null)
            {
                await ImagesFromMovie(result + "," + droppedFile);
            }

            var savedName = result + @"/" + result + @"_%04d.png".Trim(' ');

            var count = _assetRepo.Animation.Animations.Where(x => x.FileName == savedName).Count();

            if (count == 0)
            {
                _assetRepo.Animation.Animations.Add(new AssetModel.Animations()
                {
                    Alias = file,
                    ColorKey = "blacksrc",
                    FileName = savedName,
                    PosX = 0,
                    PosY = 0,
                    Repeat = false
                });

                Animations.Refresh();
            }

        }

        #region Saving
        private async Task SaveDroppedFileAsync()
        {
            await ImagesFromMovie(_droppedFile);

            var savedName = ConvertSettings.AnimationName + 
                @"/" + ConvertSettings.AnimationName + @"_%04d." + ConvertSettings.ImageFormat;

            UpdateAnimations(savedName);

            await _dialogService.HideMetroDialogAsync(this, customDialog);
        }

        private void UpdateAnimations(string savedName)
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory())) return;

            var count = _assetRepo.Animation.Animations.Where(x => x.FileName == savedName).Count();
            if (count == 0)
            {
                _assetRepo.Animation.Animations.Add(new AssetModel.Animations()
                {
                    Alias = ConvertSettings.AnimationName,
                    ColorKey = "blacksrc",
                    FileName = savedName,
                    PosX = 0,
                    PosY = 0,
                    Repeat = false
                });

                _assetRepo.SerializeAsset("asset//", _assetRepo.Animation);

                Animations.Refresh();
            }
        }

        private void UpdateVoice(string audioFile)
        {
            if (!File.Exists(_gameConfig.Config.VoicePath + "//" + audioFile)) return;

            _assetRepo.Audio.Voice.Add(new AssetModel.Voice()
            {
                Name = ConvertSettings.AudioName,
                FileName = audioFile,
                Volume = 0.8                
            });

            _assetRepo.SerializeAsset("asset//", _assetRepo.Audio);

            _eventAggregator.GetEvent<AssetUpdateEvent>().Publish("");

        }

        private async Task ImagesFromMovie(string fileData)
        {
            var ffmpegExe = ConfigurationManager.AppSettings["ffmpeg:ExeLocation"].ToString();
            if (ffmpegExe == null) return;                           

            var progressResult = await _dialogService.ShowProgressAsync(this, "Converting video", "");

            if (ConvertSettings.ConvertToImages)
            {

                var animPath = Path.GetFullPath(_gameConfig.Config.ImagesPath + "//" + ConvertSettings.AnimationName);                

                if (string.IsNullOrEmpty(ConvertSettings.ImageFormat))
                    ConvertSettings.ImageFormat = "png";

                await FFmpeg.ImagesFromMovieAsync(ffmpegExe, fileData, animPath, 
                    ConvertSettings.AnimationName, ConvertSettings.Fps, ConvertSettings.ImageFormat);

            }

            if (ConvertSettings.ConvertAudio)
                await SoundFromMovie(fileData, ConvertSettings.AnimationName);

            await progressResult.CloseAsync();
        }

        private async Task SoundFromMovie(string fileData, string audioName)
        {
            var ffmpegExe = ConfigurationManager.AppSettings["ffmpeg:ExeLocation"].ToString();
            if (ffmpegExe == null) return;

            if (!Directory.Exists(_gameConfig.Config.VoicePath))
                Directory.CreateDirectory(_gameConfig.Config.VoicePath);

            if (string.IsNullOrEmpty(ConvertSettings.AudioFormat))
                ConvertSettings.AudioFormat = "ogg";

            await FFmpeg.SoundFromMovieAsync(ffmpegExe, fileData, _gameConfig.Config.VoicePath, audioName, ConvertSettings.AudioFormat);

            UpdateVoice(audioName + "." + ConvertSettings.AudioFormat);

        }
        #endregion

        //ffmpeg -i video.mp4 -vn -ab 256 audio.mp3
    }
}
