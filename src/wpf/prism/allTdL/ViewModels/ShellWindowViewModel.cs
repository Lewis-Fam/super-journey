using Prism.Mvvm;

namespace allTdL.ViewModels
{
    public class ShellWindowViewModel : BindableBase
    {
        private string _title = "TdL";
        
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}
