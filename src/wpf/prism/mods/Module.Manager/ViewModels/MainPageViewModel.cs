using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using allTdL.Core;
using allTdL.Core.Mvvm;
using allTdL.Mvvm;
using allTdL.Mvvm.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace Module.Manager.ViewModels
{
    public class MainPageViewModel : NavigationViewModelBase
    {
        public MainPageViewModel(IRegionManager regionManager, IEventAggregator ea) : base(regionManager)
        {
            _ea = ea;
            Messages = new ObservableCollection<string>();
            _ea.GetEvent<ApplicationStatusEvent>().Subscribe(ApplicationStatusReceived);
            _viewName = "StockQuotes";
        }

        private void ApplicationStatusReceived(ApplicationStatusEventArgs obj)
        {
            HandleMessageReceived(obj.Message);
        }

        private string _lastMessage;
        public string LastMessage
        {
            get { return _lastMessage; }
            set { SetProperty(ref _lastMessage, value); }
        }


        private void HandleMessageReceived(string message)
        {
            Messages?.Add(message);
            LastMessage = message;
        }

        private ObservableCollection<string> _messages;
        public ObservableCollection<string> Messages
        {
            get { return _messages; }
            set { SetProperty(ref _messages, value); }
        }

        private string _viewName;
        private readonly IEventAggregator _ea;

        public string ViewNameTextbox
        {
            get { return _viewName; }
            set { SetProperty(ref _viewName, value); }
        }

        private Menu MainMenu { get; set; }

        protected override void OnNavigate(string viewName)
        {
            var cmd = viewName.Split(",");
            if (cmd.Length == 2)
            {
                var one = cmd[0];
                var two = cmd[1];

                Debug.WriteLine("MainPage.Override.OnNavigate " + one + " " + two);
                RegionManager.RequestNavigate(one, two);
                return;
            }

            base.OnNavigate(viewName);
        }
    }
}