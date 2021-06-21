using System.Diagnostics;
using allTdL.Core.Helper;
using allTdL.Core.Mvvm;
using allTdL.Core.Strings;
using LewisFam.Stocks.ThirdParty.Cnbc;
using LewisFam.Stocks.ThirdParty.Services;
using LewisFam.Stocks.ThirdParty.Webull;
using ModuleStocks.ViewModels;
using ModuleStocks.Views;

using Prism.Ioc;
using Prism.Regions;

namespace ModuleStocks
{

    /// <summary>
    /// Defines a ModuleStocksModule module contract.
    /// </summary>
    class ModuleStocksModule : PrismModule
    { 
        ///<inheritdoc/>
        public override void OnInitialized(IContainerProvider containerProvider)
        {
            Debug.WriteLine($"{nameof(ModuleStocksModule)} : {nameof(OnInitialized)}");
            RegionManager.RequestNavigate(RegionNames.ContentRegion, nameof(StockQuotes));
        }

        ///<inheritdoc/>
        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Debug.WriteLine($"{nameof(ModuleStocksModule)} : {nameof(RegisterTypes)}");

            containerRegistry.RegisterForNavigation<CnbcStockQuote, CnbcStockQuoteViewModel>();
            containerRegistry.RegisterForNavigation<Watchlist, WatchlistViewModel>();
            containerRegistry.RegisterForNavigation<StockQuotes, StockQuotesViewModel>();
            containerRegistry.RegisterForNavigation<StockQuote, StockQuoteViewModel>();
            containerRegistry.RegisterSingleton<ICnbcDataService, CnbcDataService>();
            containerRegistry.RegisterSingleton<IWebullDataService, WebullDataService>();
        }

        public ModuleStocksModule(IRegionManager regionManager) : base(regionManager)
        {
            Debug.WriteLine($"ctor : {nameof(ModuleStocksModule)}");
        }
    }
}