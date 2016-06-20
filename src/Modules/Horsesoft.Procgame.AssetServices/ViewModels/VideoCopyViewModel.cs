using GongSolutions.Wpf.DragDrop;
using Horsesoft.Procgame.Manager.Base.Events;
using Horsesoft.Procgame.Manager.Base.PrismBase;
using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace Horsesoft.Procgame.AssetServices.ViewModels
{
    public class VideoCopyViewModel : ViewModelBase, IDropTarget
    {
        CustomDialog customDialog;

        private IDialogCoordinator _dialogService;
        private IEventAggregator _evtAgg;

        public DelegateCommand CloseAllPendingFileDropCommand { get; private set; } 
        public VideoCopyViewModel(IDialogCoordinator dialogService, IEventAggregator evtAgg)
        {

            _evtAgg = evtAgg;
           
            _dialogService = dialogService;            

            CloseAllPendingFileDropCommand = new DelegateCommand(() =>
            {
                //cancelPending = true;

                //await _dialogService.HideMetroDialogAsync(this, customDialog);

                _evtAgg.GetEvent<CloseCustomDialogAnimsEvent>().Publish("");

                // cancelPending = false;
            });

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

            //foreach (var file in dragFileList)
            //{
            //    switch (Path.GetExtension(file))
            //    {
            //        case ".mp4":
            //        case ".flv":
            //        case ".avi":
            //            _droppedFile = file;
            //            var runCustom = await DroppedFileCustomDialogAsync(file);
            //            if (runCustom)
            //                await customDialog.WaitUntilUnloadedAsync();
            //            //if (cancelPending)
            //            //    break;
            //            break;
            //        default:
            //            break;
            //    }
            //}
        }

    }
}
