using Prism.Navigation;

namespace allTdL.Mvvm
{
    /// <summary>A view model base class.</summary>
    public abstract class ViewModelBase : BindableObject, IDestructible
    {
        private object _selected;

        private int _selectedIndex;

        private string _title;

        /// <summary>Gets or sets the selected object index.</summary>
        public virtual int SelectedIndex
        {
            get { return _selectedIndex; }
            set { SetProperty(ref _selectedIndex, value); }
        }

        /// <summary>Gets or sets the selected object.</summary>
        public virtual object SelectedItem
        {
            get { return _selected; }
            set { SetProperty(ref _selected, value); }
        }

        /// <summary>Gets or sets the title.</summary>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        /// <inheritdoc/>
        public virtual void Destroy()
        {
        }

        /// <summary>Initialization override helper.</summary>
        protected virtual void Init()
        {
        }
    }
}