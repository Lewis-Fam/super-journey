using allTdL.Core.Data.Models;
using allTdL.Core.Mvvm;
using allTdL.Mvvm;
using Prism.Regions;

namespace allTdL.Mods.ToDo.ViewModels
{

    public class ViewFtbListViewModel : NavigationViewModelBase
    {

        public ViewFtbListViewModel(IRegionManager regionManager) : base(regionManager)
        {
            //Items.Add(new Card{FrontText = "Some front text.", BackText = "The back text."});
            Card = new Card{FrontText = "Some front text.", BackText = "The back text."};
        }

        //public ObservableCollection<Card> Items { get; private set; } = new ObservableCollection<Card>();

        private Card _card;

        public Card Card
        {
            get { return _card; }
            set { SetProperty(ref _card, value); }
        }

    }
}