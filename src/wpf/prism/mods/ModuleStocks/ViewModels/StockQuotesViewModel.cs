using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using allTdL.Core.Mvvm;
using allTdL.Mvvm;
using LewisFam.Stocks;
using LewisFam.Stocks.Models;
using LewisFam.Stocks.ThirdParty.Services;
using LewisFam.Stocks.ThirdParty.Webull.Models;

using ModuleStocks.Properties;

using Prism.Commands;
using Prism.Regions;

namespace ModuleStocks.ViewModels
{  
    /// <summary>The stock quotes view model.</summary>
    public class StockQuotesViewModel : NavigationViewModelBase
    {
        private readonly IWebullDataService _webull;

        private bool _appendQuoteResults;

        private bool _appendSearchSymbolResults;

        private bool _autoRefreshQuotes;

        private ObservableCollection<IChartData> _chartData = new ObservableCollection<IChartData>();

        private Visibility _gridViewVisibility;

        private string _search;

        private Stock _stock;

        private ObservableCollection<IRealTimeStockQuote> _stockQuotes = new ObservableCollection<IRealTimeStockQuote>();

        private ObservableCollection<Stock> _stocks = new ObservableCollection<Stock>();

        private Stopwatch _stopwatch;
        /// <summary>Initializes a new instance of the <see cref="StockQuotesViewModel"/> class.</summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="webull">       The webull.</param>
        public StockQuotesViewModel(IRegionManager regionManager, IWebullDataService webull) : base(regionManager)
        {
            Debug.WriteLine($"{nameof(StockQuoteViewModel)}");
            _webull = webull;
            Init();

            PropertyChanged += (sender, args) =>
            {
                Debug.WriteLine($"****PropertyChanged: {args.PropertyName}");
            };
        }
        
        /// <summary>Gets or sets a value indicating whether append quote results.</summary>
        public bool AppendQuoteResults
        {
            get { return _appendQuoteResults; }
            set { SetProperty(ref _appendQuoteResults, value); }
        }

        /// <summary>Gets or sets a value indicating whether append search symbol results.</summary>
        public bool AppendSearchSymbolResults
        {
            get { return _appendSearchSymbolResults; }
            set { SetProperty(ref _appendSearchSymbolResults, value); }
        }

        /// <summary>Gets or sets a value indicating whether auto refresh quotes.</summary>
        public bool AutoRefreshQuotes
        {
            get { return _autoRefreshQuotes; }
            set { SetProperty(ref _autoRefreshQuotes, value); }
        }

        /// <summary>Gets or sets the chart data.</summary>
        public ObservableCollection<IChartData> ChartData
        {
            get { return _chartData; }
            set { SetProperty(ref _chartData, value); }
        }

        /// <summary>
        /// Gets the clear quotes command.
        /// </summary>
        public DelegateCommand ClearQuotesCommand => new DelegateCommand(ClearQuotes);

        /// <summary>Gets the get stock quote command button.</summary>
        public DelegateCommand GetStockQuoteCommand => new DelegateCommand(GetStockQuote);

        /// <summary>Gets or sets the grid view visibility.</summary>
        public Visibility GridViewVisibility
        {
            get { return _gridViewVisibility; }
            set { SetProperty(ref _gridViewVisibility, value); }
        }

        /// <summary>Gets or sets the search.</summary>
        public string Search
        {
            get { return _search; }
            set { SetProperty(ref _search, value); }
        }

        /// <summary>Gets the search stock symbols command button.</summary>
        public DelegateCommand SearchStockSymbolsCommand => new DelegateCommand(SearchStockSymbols);

        /// <summary>Gets or sets the stock.</summary>
        public Stock SelectedStock
        {
            get { return _stock; }
            set { SetProperty(ref _stock, value); }
        }

        private IStockQuote _selectedQuote;
        public IStockQuote SelectedQuote
        {
            get { return _selectedQuote; }
            set { SetProperty(ref _selectedQuote, value); }
        }


        /// <summary>Gets or sets the stock quotes collection.</summary>
        public ObservableCollection<IRealTimeStockQuote> StockQuotes
        {
            get { return _stockQuotes; }
            set { SetProperty(ref _stockQuotes, value); }
        }

        /// <summary>Gets the stock quote selected command.</summary>
        public DelegateCommand<IRealTimeStockQuote> StockQuoteSelectedCommand => new DelegateCommand<IRealTimeStockQuote>(OnStockQuoteSelected);

        /// <summary>Gets or sets the stocks collection.</summary>
        public ObservableCollection<Stock> Stocks
        {
            get { return _stocks; }
            set { SetProperty(ref _stocks, value); }
        }
        ///<inheritdoc/>
        public override void Destroy()
        {
            Debug.WriteLine($"{nameof(Destroy)}");
            _webull.Dispose();
        }

        /// <summary>Gets the stock chart data async.</summary>
        public async void GetStockChartDataAsync()
        {
            Debug.WriteLine($"{nameof(GetStockChartDataAsync)}");
            var data = await _webull.GetStockChartDataAsync(SelectedStock.TickerId);
            ChartData.Clear();
            ChartData.AddRange(data);
        }

        /// <summary>Ons the update quotes.</summary>
        public async void OnUpdateQuotes()
        {
            Debug.WriteLine($"{nameof(OnUpdateQuotes)}");
            var symbols = StockQuotes.Select(s=>s.TickerId).ToList().ToList();

            var newQuotes = (await _webull.GetRealTimeMarketQuotesAsync(symbols));

            foreach (var quote in newQuotes)
            {
                var found = StockQuotes.FirstOrDefault(x => x.TickerId == quote.TickerId);
                if (found != null)
                {
                    StockQuotes.Remove(found);
                    Debug.WriteLine("Removed!");
                    StockQuotes.Add(quote);
                }
            }
        }

        /// <summary>
        /// Executes the clears quotes command.
        /// </summary>
        protected virtual void ClearQuotes()
        {
            Debug.WriteLine($"{nameof(ClearQuotes)}");
            StockQuotes?.Clear();
        }

        ///<inheritdoc/>
        protected sealed override void Init()
        {   
            Debug.WriteLine($"{nameof(Init)}");
            Search = Settings.Default.LastSearch;
            Get2021RealTimeQuotes();
            AppendQuoteResults = true;
            GridViewVisibility = Visibility.Visible;
        }

        /// <summary>Ons the stock quote selected.</summary>
        /// <param name="quote">The quote.</param>
        protected virtual void OnStockQuoteSelected(IRealTimeStockQuote quote)
        {
            Debug.WriteLine($"{nameof(OnStockQuoteSelected)}:{quote?.Symbol}");
            var parameters = new NavigationParameters
            {
                {
                    "StockQuote", quote
                }
            };

            if (quote != null)
                RegionManager.RequestNavigate("StockQuoteDetailsRegion", "StockQuote", parameters);
        }

        /// <summary>Searches the symbols.</summary>
        protected virtual async void SearchStockSymbols()
        {
            Debug.WriteLine($"{nameof(SearchStockSymbols)}|{nameof(Search)}={Search}|{nameof(AppendSearchSymbolResults)}={AppendSearchSymbolResults}");
            if (string.IsNullOrEmpty(Search))
                return;

            Settings.Default.LastSearch = Search;
            Settings.Default.Save();

            var results = await _webull.SearchSymbolAsync(Search);
            if (!AppendSearchSymbolResults)
                Stocks.Clear();

            Stocks.AddRange(results);
        }

        private Dictionary<Stock, IRealTimeStockQuote> _quoteCollection = new Dictionary<Stock, IRealTimeStockQuote>();
        
        public Dictionary<Stock, IRealTimeStockQuote> QuoteCollection
        {
            get { return _quoteCollection; }
            set { SetProperty(ref _quoteCollection, value); }
        }


        /// <summary>Get2021S the real time quotes.</summary>
        /// <returns>A Task.</returns>
        private async void Get2021RealTimeQuotes()
        {
            Debug.WriteLine($"{nameof(Get2021RealTimeQuotes)}");
            var ids = StocksUtil.StockList2021.ToTickerIdList();
            var quotes = await _webull.GetRealTimeMarketQuotesAsync(ids);
            if (quotes != null)
            {
                foreach (var realTimeStockQuote in quotes)
                {
                    QuoteCollection.Add(new Stock(realTimeStockQuote.Symbol, realTimeStockQuote.TickerId), realTimeStockQuote);
                }
                StockQuotes.AddRange(quotes);
                Stocks.AddRange(quotes.Select(s => new Stock(s.Symbol, s.TickerId)));
            }
            Console.WriteLine();
        }

        /// <summary>Gets the stock chart data async.</summary>
        /// <param name="tickerId">The ticker id.</param>
        /// <param name="time">    The time.</param>
        /// <returns>A Task.</returns>
        private async Task<object> GetStockChartDataAsync(long tickerId, string time)
        {
            Debug.WriteLine($"{nameof(GetStockChartDataAsync)}|{nameof(tickerId)}={tickerId}|{nameof(time)}={time}");
            return await _webull.GetStockChartDataAsync(tickerId);
        }

        /// <summary>Gets the stock quote.</summary>
        private async void GetStockQuote()
        {
            Debug.WriteLine($"{nameof(GetStockQuote)}|{nameof(Search)}={Search}|{nameof(AppendQuoteResults)}={AppendQuoteResults}");
            if (string.IsNullOrEmpty(Search))
                return;

            Settings.Default.LastSearch = Search;
            Settings.Default.Save();

            var ids = new List<long>();
            var id = await _webull.FindStockIdAsync(Search);
            if (id != null)
            {
                ids.Add(id.Value);
                var data = await _webull.GetRealTimeMarketQuotesAsync(ids);
                if (!AppendQuoteResults)
                    StockQuotes.Clear();

                StockQuotes.AddRange(data);
            }
        }

        void RefactorStockList()
        {

        }
    }
}