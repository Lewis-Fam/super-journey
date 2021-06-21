using allTdL.Core.Data.Models;
using allTdL.Core.Mvvm;
using Prism.Regions;

namespace allTdL.Mods.ToDo.ViewModels
{

    public class ViewToDoItemViewModel : ViewModelBase
    {
        private ToDoItem _selectedItem;
        
        public ToDoItem SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        public ViewToDoItemViewModel()
        {
        }
    }
}