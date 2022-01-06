using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockClient.Model
{
  /// <summary>
  /// Data Transfer Object for Stock
  /// </summary>
  [Serializable]
  class StockDTO
  {
    public string Symbol { get; }
    public float Bid { get; }
    public float Ask { get; }

    public StockDTO(string symbol, float bid, float ask)
    {
      Symbol = symbol;
      Bid = bid;
      Ask = ask;
    }

  }
}

