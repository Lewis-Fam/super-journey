using LewisFam.Stocks.ThirdParty.Services;
using ModuleStocks.Models;
using Prism.Regions;

namespace ModuleStocks.ViewModels
{
    /// <inheritdoc />
    public class WatchlistsViewModel : WatchlistViewModel
    {
        public WatchlistsViewModel(IRegionManager regionManager, IWebullDataService webull) : base(regionManager, webull)
        {
        }

        private WatchlistCollection _watchlists;
        
        public WatchlistCollection Watchlists
        {
            get { return _watchlists; }
            set { SetProperty(ref _watchlists, value); }
        }
    }
}