using System;
using System.Collections.Generic;
using LewisFam;
using LewisFam.CmdLine;
using LewisFam.Stocks.ThirdParty.Webull.Models;

namespace ModuleStocks
{
    public abstract class StocksModuleConsole : BaseLewisFamConsole
    {
        private IEnumerable<Stock> _stocks;

        protected virtual void PrintStockSymbols()
        {
            if (_stocks == null)
            {
                PrintLine("Stocks == null");
                return;
            }

            foreach (var stock in _stocks)
            {
                PrintLine(stock.Symbol);
            }
        }
    }
}