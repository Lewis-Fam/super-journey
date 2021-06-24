using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using allTdL.Core.Data;
using allTdL.Core.Data.Models;
using allTdL.Core.Mvvm;
using allTdL.Mods.ToDo.Properties;
using allTdL.Mods.ToDo.Views;
using allTdL.Mvvm;
using LewisFam.Utils;
using Prism.Commands;
using Prism.Regions;

namespace allTdL.Mods.ToDo.ViewModels
{   
    public class ViewToDoItemListViewModel : NavigationViewModelBase
    {
        private ICommand _saveTaskCommand;
        private AppConfig _config;
        private DelegateCommand _createCommand;
        private DelegateCommand _deleteTaskCommand;

        public ViewToDoItemListViewModel(IRegionManager regionManager, AppConfig config) : base(regionManager)
        {
            Debug.WriteLine($"{nameof(ViewToDoItemListViewModel)}");
            _config = config;
            RegionManager.RegisterViewWithRegion("ToDoItemDetailRegion", typeof(ViewToDoItem));
            Init();
        }

        protected sealed override async void Init()
        {
            Debug.WriteLine("Init");
            Items = await GenerateSourceAsync();
        }

        async Task<ObservableCollection<ToDoItem>> GenerateSourceAsync(bool useSampleData = false)
        {
            Debug.WriteLine($"{nameof(GenerateSourceAsync)}={useSampleData}");

            var rtn = new ObservableCollection<ToDoItem>();

            if (useSampleData)
            {
                rtn.Add(new ToDoItem{Description = "A sample task item."});
                rtn.Add(new ToDoItem{Description = "A second sample item."});
                rtn.Add(new ToDoItem{Description = "A completed sample item.", IsCompleted = true});
                return rtn;
            }

            if (File.Exists(_config.TodoFileNamePath))
            {
                Debug.WriteLine("GenerateSource from config file.");
                //await using var fs = File.OpenRead(_config.TodoFileNamePath);

                //if (fs != null)
                //{
                //    var items = await JsonSerializer.DeserializeAsync<ToDoItem[]>(fs);

                //    if (items != null) 
                //        rtn.AddRange(items);

                //    return rtn;
                //}

                var json = await File.ReadAllTextAsync(_config.TodoFileNamePath);

                var items = await json.DeserializeObjectAsync<ToDoItem[]>();
                rtn.AddRange(items);
            }

            return rtn;
        }

       

        private ObservableCollection<ToDoItem> _items;
        public ObservableCollection<ToDoItem> Items
        {
            get { return _items; }
            set
            {
                if (SetProperty(ref _items, value))
                {
                    //Settings.Default.ViewToDoList_Items = JsonSerializer.Serialize(Items.ToList());
                    //OnSave();
                }
            }
        }


        public ICommand SaveCommand => _saveTaskCommand ??= new DelegateCommand(OnSave);
        public ICommand DeleteCommand => _deleteTaskCommand ??= new DelegateCommand(OnDelete);


        private void OnDelete()
        {
            Items.RemoveAt(SelectedIndex);
        }

        private async void OnSave()
        {
            Debug.WriteLine("OnSave");
            Settings.Default.Save();
            await using var fileStream = File.Create(_config.TodoFileNamePath); 
            //await JsonSerializer.SerializeAsync(fileStream, Items.ToArray());
        }

        public void Test()
        {
            TreeView tv = new TreeView();
            DirectoryInfo di = new DirectoryInfo("C:\\");
            
        }

        public ICommand CreateCommand => _createCommand ??= new DelegateCommand(OnCreate);
        
        private void OnCreate()
        {
            Items.Add(new ToDoItem{ Description = "New item"});
        }
    }
}