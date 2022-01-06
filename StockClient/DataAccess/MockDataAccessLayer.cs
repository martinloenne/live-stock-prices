using System.Collections.Generic;
using System.Linq;
using StockClient.Model;

namespace StockClient.DataAccess
{
  /// <summary>
  /// Data Access Layer for testing mock data in form of messages
  /// </summary>
  public class MockDataAccessLayer : IDataAccessLayer<Stock>
  {
    readonly Queue<Stock> messages = new Queue<Stock>();

    public MockDataAccessLayer()
    {
      messages.Enqueue(new Stock("Xiaomi", 5, 5));
      messages.Enqueue(new Stock("Apple", 1, 1));
      messages.Enqueue(new Stock("IBM", 2, 2));
      messages.Enqueue(new Stock("Yahoo", 3, 2));
      messages.Enqueue(new Stock("IBM", 1, 3));
      messages.Enqueue(new Stock("IBM", 5, 2));
      messages.Enqueue(new Stock("Apple", 1, 0.5f));
      messages.Enqueue(new Stock("IBM", 1, 1));
    }

    /// <summary>
    /// Opens the connection to the server.
    /// </summary>
    public void OpenConnection()
    {
    }

    /// <summary>
    /// Closes the connection to the server.
    /// </summary>
    public void CloseConnection()
    {
    }

    /// <summary>
    /// Returns messages received from server.
    /// </summary>
    public List<Stock> GetReceivedMessages()
    {
      List<Stock> receivedStocks = new List<Stock>();
      Stock receivedStock;

      if (messages.Any())
      {
        receivedStock = messages.Dequeue();
        receivedStocks.Add(receivedStock);
        return receivedStocks;
      }
      else 
      { 
        return null; 
      }
    }


  }
}
