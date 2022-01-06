using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockClient.Model;
using StockClient.ViewModel;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text.Json;

namespace StockClient.DataAccess
{
  /// <summary>
  /// Data Access Layer for socket connection to the message broker
  /// </summary>
  public class SocketDataAcessLayer : IDataAccessLayer<Stock>
  {
    private readonly Socket server = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    public SocketDataAcessLayer()
    {
      OpenConnection();
    }

    /// <summary>
    /// Opens the connection to the server.
    /// </summary>
    public void OpenConnection()
    {
      byte[] bytes = new byte[512];
      // Connect to the socket
      try
      {
        IPEndPoint serverConnection = new(IPAddress.Loopback, 3000);
        server.Connect(serverConnection);  
        // Encode the data string into a byte array.  
        byte[] msg = Encoding.ASCII.GetBytes("SYN");
        // Send the data through the socket.  
        int bytesSent = server.Send(msg);
      }
      catch (SocketException socketError)
      {
        Console.WriteLine("Error in socket connection : {0}", socketError.ToString());
      }
    }

    /// <summary>
    /// Closes the connection to the server.
    /// </summary>
    public void CloseConnection()
    {
      // Close the connection by releasing the socket
      server.Shutdown(SocketShutdown.Both);
      server.Close();
    }

    /// <summary>
    /// Returns messages received from server.
    /// </summary>
    public List<Stock> GetReceivedMessages()
    {
      try {
        byte[] bytes = new byte[512];
        // Receive the response from the remote device.  
        int bytesRec = server.Receive(bytes);
        String encodedMessage = Encoding.ASCII.GetString(bytes, 0, bytesRec);
        byte[] messageBytes = Encoding.UTF8.GetBytes(encodedMessage);
        StockDTO message = JsonSerializer.Deserialize<StockDTO>(messageBytes);

        // Reply that message is received 
        byte[] replyMsg = Encoding.ASCII.GetBytes("SYN");
        int bytesSent = server.Send(replyMsg);

        // Return Recieved stocks to view model
        List<Stock> receivedStocks = new List<Stock>();

        receivedStocks.Add(new Stock(message.Symbol, message.Bid, message.Ask));
        return receivedStocks;
      }
      catch (Exception socketException)
      {
        // The connection betweent the broker and client was disconnected
        Console.WriteLine("The connection disconnected: {0}", socketException);
        return null;
      }
    }
  }

}
