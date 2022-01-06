using System;
using System.Linq;
using System.Threading.Tasks;
using StockClient.Model;
using System.Collections.ObjectModel;
using StockClient.DataAccess;
using System.Collections.Generic;

namespace StockClient.ViewModel
{
  public class StockViewModel
  {
    public ObservableCollection<Stock> Stocks { get; set; }
    public Stock SelectedStock { get; set; }
    readonly SocketDataAcessLayer dataAccess = new SocketDataAcessLayer();


    public StockViewModel()
    {
      // Instansiates the main collection for stocks
      Stocks = new ObservableCollection<Stock>();
      // Start receiving stocks
      Task checkUpdatesTask = CheckForUpdates();
    }

    /// <summary>
    /// Creates or updates received stocks. 
    /// </summary>
    /// <param name="receivedStocks">Stocks that needs to be updated or inserted into the collection.</param>
    /// TODO: Unit test
    public void UpdateStocks(List<Stock> receivedStocks)
    {
      foreach (Stock receivedStock in receivedStocks)
      {
        // Search through the stocks already in the program
        /// TODO: Implement a new data structure for the ObservableCollection that supports keys for faster lookup
        Stock stock = Stocks.FirstOrDefault(i => i.symbol == receivedStock.symbol);
        // No stock detected then create the stock
        if (stock == null)
        {
            Stocks.Add(receivedStock);
        }
        else  // Stock detected then update the stock with new values
        {
          stock.bid = receivedStock.bid;
          stock.ask = receivedStock.ask;
        }
      }
    }

    /// <summary>
    /// Task for collecting stocks from the data access layer.
    /// </summary>
    private async Task CheckForUpdates()
    {
      while (true)
      {
        List<Stock> receivedStocks = new();

        await Task.Run(() => receivedStocks = dataAccess.GetReceivedMessages());

        if (receivedStocks != null)
        {
          UpdateStocks(receivedStocks);
        }
      }
    }

  }
}
