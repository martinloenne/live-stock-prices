using NUnit.Framework;
using StockClient.ViewModel;
using StockClient.Model;
using System.Collections.Generic;
using FluentAssertions;

namespace StockClient.UnitTest
{
  public class Tests
  {
    StockViewModel viewModel = new StockViewModel();

    [Test]
    public void UpdateStocks_ValidInsert_InsertNewStock()
    {
      // Arrange
      List<Stock> receivedStocks = new();
      Stock UpdatedStock = new("AMZN", 5, 5);
      receivedStocks.Add(UpdatedStock);

      // Act
      viewModel.UpdateStocks(receivedStocks);

      // Assert
      Assert.AreEqual(viewModel.Stocks[0], UpdatedStock);
    }

    [Test]
    public void UpdateStocks_ValidUpdate_UpdateNewStock()
    {
      // Arrange
      List<Stock> receivedStocksFirst = new();
      Stock InitialStock = new("AMZN", 5, 5);
      receivedStocksFirst.Add(InitialStock);
      viewModel.UpdateStocks(receivedStocksFirst);
      float ExpectedBid = 2;
      float ExpectedAsk = 3;
      string ExpectedSymbol = "AMZN";

      List<Stock> receivedStocksSecond = new();
      Stock FirstUpdatedStock = new("AMZN", 1, 1);
      Stock SecondUpdatedStock = new(ExpectedSymbol, ExpectedBid, ExpectedAsk);
      receivedStocksSecond.Add(FirstUpdatedStock);
      receivedStocksSecond.Add(SecondUpdatedStock);

      // Act
      viewModel.UpdateStocks(receivedStocksSecond);

      // Assert
      viewModel.Stocks[0].Should().BeEquivalentTo(SecondUpdatedStock);
    }
  }
}