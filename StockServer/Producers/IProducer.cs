using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockServer.Model;

namespace StockServer.Producers
{

  public interface IProducer<T>
  {
    /// <summary>
    /// Returns a message from producer.
    /// </summary>
    public T ProducerDequeue();
  }
}
