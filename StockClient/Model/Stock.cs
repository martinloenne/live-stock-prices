using System.ComponentModel;

namespace StockClient.Model
{
  public class Stock : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;
    private string Symbol;
    private float Bid { get; set; }
    private float Ask { get; set; }
    private bool Increase { get; set; }

    public Stock(string initSymbol, float initBid, float initAsk)
    {
      Symbol = initSymbol;
      Bid = initBid;
      Ask = initAsk;
      Increase = false;
    }

    public string symbol
    {
      get { return Symbol; }
      set
      {
        Symbol = value;
        OnPropertyChanged("Name");
      }
    }

    public float bid
    {
      get { return Bid; }
      set
      {
        Bid = value;
        OnPropertyChanged("Bid");
      }
    }

    public float ask
    {
      get { return Ask; }
      set
      {
        Ask = value;
        OnPropertyChanged("Ask");
      }
    }

    public bool increase
    {
      get { return Increase; }
      set
      {
        Increase = value;
        OnPropertyChanged("Increase");
      }
    }

    private void OnPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

  }
}
