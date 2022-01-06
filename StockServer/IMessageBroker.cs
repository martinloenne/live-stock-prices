using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockServer
{
  interface IMessageBroker
  {
    /// <summary>
    /// Starts the message broker and producer
    /// </summary>
    /// <param name="port">The port which the server listens for incoming consumers</param>
    public void StartBroker(int port);
  }
}
