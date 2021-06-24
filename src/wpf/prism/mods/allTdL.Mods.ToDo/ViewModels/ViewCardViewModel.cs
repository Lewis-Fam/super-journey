using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using allTdL.Core;
using allTdL.Core.Data.Models;
using allTdL.Core.Mvvm;
using allTdL.Mvvm;
using Prism.Commands;
using Prism.Regions;

namespace allTdL.Mods.ToDo.ViewModels
{
    public class ViewCardViewModel : NavigationViewModelBase
    {

        public ViewCardViewModel(IRegionManager regionManager) : base(regionManager)
        {
            //AddSampleItems();
            SomeTextValue = 1;
        }

        //private int _selectedIndex;

        ///// <summary>
        ///// Gets or sets the selected object index.
        ///// </summary>
        //public virtual int SelectedIndex
        //{
        //    get { return _selectedIndex; }
        //    set { SetProperty(ref _selectedIndex, value); }
        //}

        //private object _selected;

        ///// <summary>
        ///// Gets or sets the selected object.
        ///// </summary>
        //public virtual object SelectedItem
        //{
        //    get { return _selected; }
        //    set { SetProperty(ref _selected, value); }
        //}

        void AddSampleItems()
        {
            
            Items.Add(new Card());
            Items.Add(new Card("", "", "Naming") {FrontText = "Some front text.", BackText = "The back text.", Name = "Some Name", Question = "A question"});
        }

        private int someTextValue;
        public int SomeTextValue
        {
            get { return someTextValue; }
            set { SetProperty(ref someTextValue, value); }
        }


        public ObservableCollection<Card> Items { get; set; } = new ObservableCollection<Card>();

        private ICommand _saveCommand;
        private ICommand _wrongCommand;
        private ICommand _addCommand;
        private ICommand _reloadCommand;
        private ICommand _newItemCommand;
        public ICommand SaveCommand => _saveCommand ??= new DelegateCommand(OnSave);
        public ICommand NewSelectedCommand => _newItemCommand ??= new DelegateCommand(OnCreateNewCard);

        private void OnCreateNewCard()
        {
            var card = new Card("NewCard");
            Items.Add(card);
            SelectedItem = card;
        }

        public ICommand OnReloadListViewItemsCommand => _reloadCommand ??= new DelegateCommand(OnReloadListViewItems);

        private void OnReloadListViewItems()
        {
          
        }

        private void OnSaveCardChanges()
        {
            
        }

        public ICommand WrongCommand => _wrongCommand ??= new DelegateCommand(OnWrong);

        private void OnWrong()
        {
            Items[SelectedIndex].IncreaseCount(RightWrong.Right);
            RaisePropertyChanged("Selected");
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            SelectedIndex = 1;
            OnLoad1();
            //Task.Run((async () =>
            //{
            //    await OnLoad();
            //}));             
        }

        private async void OnSave()
        {
            var fileStream = File.Create(_cardsFileNamePath);
           await JsonSerializer.SerializeAsync(fileStream, Items.ToList());
        }

        private readonly string _cardsFileNamePath = Path.Combine("cards.json");

        private async void OnLoad1()
        {
            //using FileStream openStream = File.OpenRead(_cardsFileNamePath);
            //var cards = await JsonSerializer.DeserializeAsync<List<Card>>(openStream);

            //var cards = await JsonUtil.DeserializeFilestreamAsync<List<Card>>(_cardsFileNamePath);

            Items.Clear();
            //var card = JsonSerializer.Deserialize<List<Card>>(json);

            //foreach (var card in cards)
            //{
            //    Items.Add(card);
            //}

        }

        private async Task OnLoad()
        {
            Items.Clear();
            var fileName = "";
            var cardsPath = Path.Combine("cards.json");
            using FileStream openStream = File.OpenRead(cardsPath);
            var card = await JsonSerializer.DeserializeAsync<List<Card>>(openStream);

            foreach (var card1 in card)
            {
                Items.Add(card1);
            }
        }
        
        
    }
}