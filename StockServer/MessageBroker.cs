using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using StockServer.Producers;
using StockServer.Model;
using System.Text.Json;


namespace StockServer
{
  /// <summary>
  /// The broker handles exchanging messages from the producer to the clients/consumers.
  /// </summary>
  /// TODO: Handle multiple producers
  public class MessageBroker<T> : IMessageBroker
  {
    private Socket serverSocket, _consumer;
    private byte[] _buffer;
    private IProducer<T> _producer;

    public MessageBroker(IProducer<T> producer)
    {
      _producer = producer;
    }


    /// <summary>
    /// Starts the message broker and producer
    /// </summary>
    /// <param name="port">The port which the server listens for incoming consumers</param>
    /// TODO: Multiple clients with their own queue
    public void StartBroker(int port)
    {
      // Create server socket and listen on any local interface.
      serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
      serverSocket.Listen(0);
      Console.WriteLine("Listening for connections");
      
      // A consumer has connected
      _consumer = serverSocket.Accept();
      Console.WriteLine("Consumer Connected");

      // Message queue for the client
      Queue<T> messages = new Queue<T>();

      // While the connection to the consumer is open
      while (_consumer.Connected)
      {
        // Get the messages from the producer
        T producerMessage = _producer.ProducerDequeue();
        if (producerMessage != null)
        {
          messages.Enqueue(producerMessage);
        }

        _buffer = new byte[_consumer.ReceiveBufferSize];

        // Receive response from consumer ready to receive message
        if (messages.Any()) {
          Console.WriteLine("Waiting for response");
          ReceiveData();
        }
        else
        {
          Console.WriteLine("No messages");
        }
      
        // Check if producer has received any messages
        if (messages.Any())
        {
          SendData(messages.Dequeue());
        }
      }
    }

    private void ReceiveData()
    {
        _consumer.Receive(_buffer);
        string receivedMsg = Encoding.ASCII.GetString(_buffer);
        // Reset buffer
        Array.Resize(ref _buffer, _consumer.ReceiveBufferSize);
        Console.WriteLine("Synchronize received: " + receivedMsg);
    }

    private void SendData(T message)
    {
      byte[] jsonMessage = JsonSerializer.SerializeToUtf8Bytes(message);
      // Send the buffer over the socket to the consumer
      _consumer.Send(jsonMessage);
      Console.WriteLine("Message sent to consumer.\n");
    }
  }
}
