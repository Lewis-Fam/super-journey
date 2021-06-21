using System.Collections.Generic;
using System.Collections.ObjectModel;
using LewisFam.Stocks.Models;
using LewisFam.Stocks.ThirdParty.Cnbc.Models;
using LewisFam.Stocks.ThirdParty.Webull.Models;

namespace ModuleStocks.Models
{
    public class RealTimeStockQuote : WebullStockQuote
    {
    }

    public class WebullStockQuoteCollection : ObservableCollection<IRealTimeStockQuote>
    {
    }

    /// <summary>
    /// The stock collection.
    /// </summary>
    public class StockCollection : ObservableCollection<IStock>
    {
    }

    /// <summary>
    /// The cnbc stock quote collection.
    /// </summary>
    public class CnbcStockQuoteCollection : ObservableCollection<ICnbcRealTimeStockQuote>
    {
    }

    /// <summary>
    /// The watchlist collection.
    /// </summary>
    public class WatchlistCollection : ObservableCollection<Watchlist>
    {
    }
}
