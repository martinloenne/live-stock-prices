using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockClient.ViewModel;


namespace StockClient.ViewModel.Tests
{
  [TestClass()]
  public class StockViewModelTests
  {


    [TestMethod()]
    public void UpdateStocks_ValidUpdate_ChangesPrice()
    {
      // arrange
      StockViewModel viewModel = new StockViewModel();
      viewModel.UpdateStocks();

      double currentBalance = 10.0;
      double withdrawal = 1.0;
      double expected = 9.0;
      var account = new CheckingAccount("JohnDoe", currentBalance);

      // act
      account.Withdraw(withdrawal);

      // assert
      Assert.AreEqual(expected, account.Balance);
    }
  }
}