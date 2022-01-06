using System;
using StockServer.Model;
using StockServer.Producers;

namespace StockServer
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Message Broker started");
      MessageBroker<Stock> broker = new MessageBroker<Stock>(new MockLiveStockProducer());
      broker.StartBroker(3000);
    }
  }
}
