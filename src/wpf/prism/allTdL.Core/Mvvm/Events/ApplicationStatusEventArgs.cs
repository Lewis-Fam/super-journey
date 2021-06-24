using System;

namespace allTdL.Mvvm.Events
{
    public class ApplicationStatusEventArgs : EventArgs
    {
        public ApplicationStatusEventArgs(string message, double progress = 0, bool isError = false, Exception exception = null)
        {
            Message = message;
            Progress = progress;
            IsError = isError;
            Exception = exception;
        }

        public double Progress { get; }
        public Exception Exception { get; }
        public bool IsError { get; }
        public string Message { get;}
    }
}