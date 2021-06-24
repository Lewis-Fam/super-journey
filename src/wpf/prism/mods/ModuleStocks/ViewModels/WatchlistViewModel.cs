using System.IO;
using allTdL.Core.Mvvm;
using allTdL.Mvvm;
using LewisFam.Stocks.Models;
using LewisFam.Stocks.ThirdParty.Services;
using LewisFam.Stocks.ThirdParty.Webull.Models;
using LewisFam.Utils;
using ModuleStocks.Models;
using Prism.Commands;
using Prism.Regions;

namespace ModuleStocks.ViewModels
{

    /// <summary>
    /// The watchlist view model.
    /// </summary>
    public partial class WatchlistViewModel : NavigationViewModelBase
    {
        /// <summary>
        /// The watchlist.json path.
        /// </summary>
        private const string PathWatchlist = "watchlist.json";

        private readonly IWebullDataService _webull;

        private string _searchSymbol;

        private StockCollection _stocks = new StockCollection();

        /// <summary>
        /// Initializes a new instance of the <see cref="WatchlistViewModel"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="webull">The webull.</param>
        public WatchlistViewModel(IRegionManager regionManager, IWebullDataService webull) : base(regionManager)
        {
            _webull = webull;
            Init();
        }
        /// <summary>
        /// Gets the save command.
        /// </summary>
        /// <remarks><seealso cref="DelegateCommand"/></remarks>
        public DelegateCommand SaveCommand => new DelegateCommand(Save);

        /// <summary>
        /// Gets the search command.
        /// </summary>
        /// <remarks><seealso cref="DelegateCommand"/></remarks>
        public DelegateCommand SearchCommand => new DelegateCommand(Search);

        /// <summary>
        /// Gets or sets the search symbol.
        /// </summary>
        public string SearchSymbol
        {
            get { return _searchSymbol; }
            set { SetProperty(ref _searchSymbol, value); }
        }

        /// <summary>
        /// Gets or sets the stocks collection.
        /// </summary>
        public StockCollection Stocks
        {
            get { return _stocks; }
            set { SetProperty(ref _stocks, value); }
        }

        ///<inheritdoc/>
        protected sealed override async void Init()
        {
            if (!File.Exists(PathWatchlist)) return;

            var json = await File.ReadAllTextAsync(PathWatchlist);
            var stocks = json.DeserializeObject<Stock[]>();

            Stocks?.Clear();
            foreach (var stock in stocks)
            {
                Stocks?.Add(stock);
            }
        }

        private async void Save()
        {
            await File.WriteAllTextAsync(PathWatchlist, await Stocks.SerializeObjectToJsonAsync());
        }

        private async void Search()
        {
            var stock = await _webull.FindStockAsync(SearchSymbol);

            if (stock != null)
                Stocks.Add(stock);
        }
    }
}