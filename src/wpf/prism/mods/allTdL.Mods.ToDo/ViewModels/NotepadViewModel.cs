using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;

using allTdL.Core.Mvvm;
using allTdL.Core.Mvvm.Events;
using Microsoft.Win32;

using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace allTdL.Mods.ToDo.ViewModels
{
    class NotepadViewModel : NavigationViewModelBase
    {
        private ICommand _openCommand;

        public NotepadViewModel(IRegionManager regionManager, IEventAggregator ea) : base(regionManager)
        {
            _ea = ea;
        }

        public ICommand OpenCommand => _openCommand = new DelegateCommand(OnOpenCommand);

        public DelegateCommand SaveCommand => _saveCommand = new DelegateCommand(OnSave, CanSave);

        private void OnSave()
        {
            using var file = new StreamWriter(FilePath, false);
            file.Write(AllFileText);
        }
        private bool CanSave()
        {
            return true;
        }

        private async void OnOpenCommand()
        {
            var dlg = new OpenFileDialog();
            var result = dlg.ShowDialog();
            if (result == true)
            {
                FilePath = dlg.FileName;

                try
                {
                    using var sr = new StreamReader(FilePath);
                    SendStatus(FilePath);
                    AllFileText = await sr.ReadToEndAsync();
                }
                catch (FileNotFoundException ex)
                {
                    AllFileText = ex.Message;
                    SendStatus("Error!");
                }
                
                
            }

            
        }

        private string _allFileText;
        public string AllFileText
        {
            get { return _allFileText; }
            set
            {
                SetProperty(ref _allFileText, value);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        private void SendMessage(string message)
        {
            _ea.GetEvent<MessageSentEvent>().Publish(message);
        }

        private void SendStatus(string message)
        {
            _ea.GetEvent<ApplicationStatusEvent>().Publish(new ApplicationStatusEventArgs(message));
        }

        private string _filePath;
        private IEventAggregator _ea;
        private bool _canSave;
        private DelegateCommand _saveCommand;

        public string FilePath
        {
            get { return _filePath; }
            set { SetProperty(ref _filePath, value); }
        }
    }
}
