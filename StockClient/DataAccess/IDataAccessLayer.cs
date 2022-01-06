using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockClient.Model;


namespace StockClient.DataAccess
{
  /// <summary>
  /// Defines the implementation of a data access layer.
  /// </summary>
  interface IDataAccessLayer<T>
  {
    /// <summary>
    /// Opens a connection to be used to receive stocks.
    /// </summary>
    public void OpenConnection();

    /// <summary>
    /// Closes the connection and stops the transactions of messages.
    /// </summary>
    public void CloseConnection();

    /// <summary>
    /// Returns a list of type <see cref="Stock"/> of received messages
    /// </summary>
    public List<T> GetReceivedMessages();

  }
}
