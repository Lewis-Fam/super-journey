using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using allTdL.Core.Mvvm;
using allTdL.Mvvm;
using LewisFam.Utils;
using LewisFam.Stocks.Models;
using LewisFam.Stocks.ThirdParty.Cnbc.Models;
using LewisFam.Stocks.ThirdParty.Services;
using LewisFam.Stocks.ThirdParty.Webull.Models;
using ModuleStocks.Models;

using Prism.Commands;
using Prism.Regions;



namespace ModuleStocks.ViewModels
{
    /// <summary>The view cnbc stock quote view model.</summary>
    class CnbcStockQuoteViewModel : NavigationViewModelBase
    {
        /// <summary>The watchlist.json path.</summary>
        private const string PathWatchlist = "watchlist.json";

        private readonly ICnbcDataService _cnbc;

        private readonly IWebullDataService _webull;

        private ObservableCollection<ICnbcRealTimeStockQuote> _cnbcStockQuotes;

        private bool _isLoaded;

        private string _searchSymbol;

        private Stock[] _stocks;

        /// <summary>Initializes a new instance of the <see cref="CnbcStockQuoteViewModel"/> class.</summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="cnbc">         The cnbc.</param>
        /// <param name="webull">       The webull.</param>
        public CnbcStockQuoteViewModel(IRegionManager regionManager, ICnbcDataService cnbc, IWebullDataService webull) : base(regionManager)
        {
            CnbcStockQuotes = new CnbcStockQuoteCollection();
            _cnbc = cnbc;
            _webull = webull;
            LoadQuotes();
        }

        /// <summary>Gets or sets the cnbc stock quotes.</summary>
        public ObservableCollection<ICnbcRealTimeStockQuote> CnbcStockQuotes
        {
            get { return _cnbcStockQuotes; }
            set { SetProperty(ref _cnbcStockQuotes, value); }
        }

        /// <summary>Gets the get quote command.</summary>
        /// <remarks><seealso cref="DelegateCommand"/></remarks>
        public DelegateCommand GetQuoteCommand => new DelegateCommand(GetQuote);

        /// <summary>Gets the reload quotes command.</summary>
        /// <remarks><seealso cref="DelegateCommand"/></remarks>
        public DelegateCommand ReloadQuotesCommand => new DelegateCommand(ReloadQuotes);

        /// <summary>Gets or sets the search symbol.</summary>
        public string SearchSymbol
        {
            get { return _searchSymbol; }
            set { SetProperty(ref _searchSymbol, value); }
        }

        /// <summary>Gets the update quotes command.</summary>
        /// <remarks><seealso cref="DelegateCommand"/></remarks>
        public DelegateCommand UpdateQuotesCommand => new DelegateCommand(updateQuotes);

        private string[] _symbols => _stocks.Select(s => s.Symbol).ToArray();

        ///<inheritdoc/>
        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            _isLoaded = false;
        }

        ///<inheritdoc/>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            LoadQuotes();
        }

        private async void GetQuote()
        {
            if (string.IsNullOrEmpty(SearchSymbol))
                return;

            var quote = await _cnbc.GetRealTimeMarketQuoteAsync(SearchSymbol);
            CnbcStockQuotes?.Add(quote);
        }

        private async Task<IEnumerable<ICnbcRealTimeStockQuote>> GetQuotes()
        {
            return await _cnbc.GetRealTimeMarketQuotesAsync(_symbols);
        }

        private async void LoadQuotes()
        {
            if (_isLoaded)
                return;

            _isLoaded = true;
            _stocks = await LoadWatchlistAsync();

            if (_stocks == null)
                return;

            CnbcStockQuotes?.Clear();
            CnbcStockQuotes?.AddRange(await GetQuotes());
        }

        private async Task<Stock[]> LoadWatchlistAsync(string path = PathWatchlist)
        {
            if (!File.Exists(path)) return null;

            var json = await File.ReadAllTextAsync(path);
            _stocks = await json.DeserializeObjectAsync<Stock[]>();
            return _stocks;
        }

        private async void ReloadQuotes()
        {
            if (_symbols == null)
                return;

            //var quotes = await _cnbc.GetMarketQuotesAsync(_symbols);
            var quotes = await GetQuotes();
            CnbcStockQuotes?.Clear();
            CnbcStockQuotes?.AddRange(quotes);
        }

        private void SaveWatchlist(string path = PathWatchlist)
        {
            File.WriteAllText(path, _stocks.ToJson());
        }

        private void UpdateQuote(int index, ICnbcRealTimeStockQuote quote)
        {
            CnbcStockQuotes[index] = quote;
        }

        /// <summary>Updates the quotes.</summary>
        private async void updateQuotes()
        {
            if (_symbols == null)
                return;

            var quotes = await GetQuotes();

            //ToDo: Update to index. //UpdateQuote(index, quote);
            CnbcStockQuotes?.Clear();
            CnbcStockQuotes?.AddRange(quotes);
        }
    }
}