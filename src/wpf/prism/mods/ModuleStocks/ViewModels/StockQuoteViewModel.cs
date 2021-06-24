using System.Diagnostics;

using allTdL.Core.Mvvm;
using allTdL.Mvvm;
using LewisFam.Stocks.Models;
using LewisFam.Stocks.ThirdParty.Services;

using Prism.Commands;
using Prism.Regions;

namespace ModuleStocks.ViewModels
{
    /// <summary>The stock quote view model.</summary>
    public partial class StockQuoteViewModel : NavigationViewModelBase
    {
        private IRealTimeStockQuote _selectedQuote;

        private IWebullDataService _webull;

        /// <summary>Initializes a new instance of the <see cref="StockQuoteViewModel"/> class.</summary>
        /// <param name="regionManager">The region manager.</param>
        public StockQuoteViewModel(IRegionManager regionManager, IWebullDataService webull) : base(regionManager)
        {
            Debug.WriteLine($"{nameof(StockQuoteViewModel)}");
            _webull = webull;
        }

        /// <summary>Gets or sets the selected quote.</summary>
        public IRealTimeStockQuote SelectedQuote
        {
            get { return _selectedQuote; }
            set { SetProperty(ref _selectedQuote, value); }
        }

        public DelegateCommand UpdateQuoteCommand => new DelegateCommand(OnUpdateQuote);

        ///<inheritdoc/>
        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            Debug.WriteLine($"{nameof(StockQuoteViewModel)} : {nameof(IsNavigationTarget)}");

            if (navigationContext.Parameters["StockQuote"] is IRealTimeStockQuote quote)
                return SelectedQuote != null && SelectedQuote.Name == quote.Name;

            return true;
        }

        ///<inheritdoc/>
        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Debug.WriteLine($"{nameof(StockQuoteViewModel)} : {nameof(OnNavigatedFrom)}");
        }

        ///<inheritdoc/>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            Debug.WriteLine($"{nameof(StockQuoteViewModel)} : {nameof(OnNavigatedTo)}");

            if (navigationContext.Parameters["StockQuote"] is IRealTimeStockQuote quote)
                SelectedQuote = quote;
        }

        protected virtual async void OnUpdateQuote()
        {
            Debug.WriteLine($"{nameof(OnUpdateQuote)}");
            SelectedQuote = await _webull.GetRealTimeMarketQuoteAsync(SelectedQuote.TickerId);
        }
    }
}