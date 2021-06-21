
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;


namespace allTdL.Services.Interfaces
{
    public interface IMessageService
    {
        string GetMessage();
        DriveInfo[] GetAllDriveInfo();
        DirectoryInfo GetDirInfo(string path);
    }

    //public interface IWebullDataService : IDisposable
    //{
    //    IEnumerable<IWebullOptionQuote> AllOptions { get; }

    //    Task<IEnumerable<IChartData>> GetStockChartDataAsync(long tickerId);

    //    Task<Stock> FindStockAsync(string symbol);

    //    Task<long?> FindStockIdAsync(string symbol);

    //    Task<IEnumerable<IWebullOptionQuote>> GetAllOptionsAsync(long tickerId);

    //    Task<IWebullOptionQuote> GetOptionAsync(long tickerId);

    //    Task<IWebullOptionQuote> GetOptionAsync(string symbol);

    //    Task<IEnumerable<IStockQuoteFull>> GetRealTimeMarketQuotes(IEnumerable<long> tickerIds, int batchSize = 50);

    //    Task<IEnumerable<Stock>> SearchSymbolAsync(string symbol);
    //}
}
