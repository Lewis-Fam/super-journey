using System;
using System.Windows.Input;

namespace allTdL.Core.Mvvm.Commands
{
    /// <summary>An System.Windows.Input.ICommand whose delegates can be attached for DelegateCommand`1.Execute(`0) and DelegateCommand`1.CanExecute(`0)</summary>
    /// <typeparam name="T"></typeparam>
    public class DelegateCommand<T> : ICommand
    {
        private Predicate<T> _canExecute;

        private bool _canExecuteCache = true;

        private Action<T> _method;

        /// <summary>Initializes a new instance of the <see cref="DelegateCommand"/> class.</summary>
        /// <param name="method">The method.</param>
        public DelegateCommand(Action<T> method)
            : this(method, null)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="DelegateCommand"/> class.</summary>
        /// <param name="method">    The method.</param>
        /// <param name="canExecute">The can execute.</param>
        public DelegateCommand(Action<T> method, Predicate<T> canExecute)
        {
            _method = method;
            _canExecute = canExecute;
        }

        /// <summary>Defines the method that determines whether the command can execute in its current state.</summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
            {
                bool tempCanExecute = _canExecute((T)parameter);

                if (_canExecuteCache != tempCanExecute)
                {
                    _canExecuteCache = tempCanExecute;
                    this.RaiseCanExecuteChanged();
                }
            }

            return _canExecuteCache;
        }

        /// <summary>Defines the method to be called when the command is invoked.</summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            if (_method != null)
                _method.Invoke((T)parameter);
        }

        /// <summary>Raises CanExecuteChanged event to notify changes in command status.</summary>
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, new EventArgs());
            }
        }

        #region ICommand Members

        /// <summary></summary>
        public event EventHandler CanExecuteChanged;

        #endregion ICommand Members
    }

    /// <summary></summary>
    public class DelegateCommand : ICommand
    {
        /// <summary></summary>
        public event EventHandler CanExecuteChanged;

        private Predicate<object> canExecute;

        private bool canExecuteCache = true;

        private Action<object> executeAction;

        /// <summary>Initializes a new instance of the <see cref="DelegateCommand"/> class.</summary>
        /// <param name="executeAction">The execute action.</param>
        public DelegateCommand(Action<object> executeAction)
        {
            this.executeAction = executeAction;
        }

        /// <summary>Initializes a new instance of the <see cref="DelegateCommand"/> class.</summary>
        /// <param name="executeAction">The execute action.</param>
        /// <param name="canExecute">   The can execute.</param>
        public DelegateCommand(Action<object> executeAction,
                               Predicate<object> canExecute)
        {
            this.executeAction = executeAction;
            this.canExecute = canExecute;
        }

        #region ICommand Members

        /// <summary>Defines the method that determines whether the command can execute in its current state.</summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
            {
                var tempCanExecute = canExecute(parameter);

                if (canExecuteCache != tempCanExecute)
                {
                    canExecuteCache = tempCanExecute;
                    RaiseCanExecuteChanged();
                }
            }
            return canExecuteCache;
        }

        /// <summary>Defines the method to be called when the command is invoked.</summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            executeAction(parameter);
        }

        /// <summary>Raises CanExecuteChanged event to notify changes in command status.</summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        #endregion ICommand Members
    }
}