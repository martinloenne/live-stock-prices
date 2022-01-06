using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockServer.Model
{
  [Serializable]
  public class Stock
  {
    public string Symbol { get; }
    public float Bid { get; }
    public float Ask { get; }

    public Stock(string symbol, float bid, float ask)
    {
      Symbol = symbol;
      Bid = bid;
      Ask = ask;
    }

  }
}
