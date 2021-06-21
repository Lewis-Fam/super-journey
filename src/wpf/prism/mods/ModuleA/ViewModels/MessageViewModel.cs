﻿using System;
using System.Collections.Generic;
using System.Text;

using allTdL.Core.Mvvm.Events;

using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace ModuleA.ViewModels
{
    public class MessageViewModel : BindableBase
    {
        IEventAggregator _ea;

        private string _message = "Message to Send";
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public DelegateCommand SendMessageCommand { get; private set; }

        public MessageViewModel(IEventAggregator ea)
        {
            _ea = ea;
            SendMessageCommand = new DelegateCommand(SendMessage);
        }

        private void SendMessage()
        {
            _ea.GetEvent<MessageSentEvent>().Publish(Message);
        }
    }
}