using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockServer.Model;

namespace StockServer.Producers
{
  /// <summary>
  /// The producer handles the flow of information going into the Server
  /// </summary>
  /// TODO: Handle multiple consumers having their own queues
  public class MockLiveStockProducer : IProducer<Stock>
  {
    private readonly Queue<Stock> mockStocks = new Queue<Stock>();
    
    public MockLiveStockProducer()
    {
      ReceiveData();
    }

    private async Task ReceiveData()
    {
      // Generate random mock data
      while (true)
      {
        string[] symbolNames = { "Apple", "IBM", "Yahoo", "Xiaomi", "TSLA", "AMZN", "FB", "NVDA" };
        var rand = new Random();
        int randomIndex = rand.Next(0, 7);
        await Task.Run(() => mockStocks.Enqueue(new Stock(symbolNames[randomIndex], rand.Next(0, 5), rand.Next(0, 5))));
        await Task.Delay(100);
      }
    }

    /// <summary>
    /// Returns the messages from the producer
    /// </summary>
    /// <returns>A model class of type <see cref="Stock"/></returns>
    public Stock ProducerDequeue()
    {
      if (mockStocks.Any())
      {
        return mockStocks.Dequeue();
      }
      else { return null; }
    }

  }
}
