using System;
using System.Drawing;
using Xunit;
using Wallets.BusinessLayer;
using System.Collections.Generic;

namespace Wallets.BusinessLayerTests
{
    public class WalletTests
    {

        [Fact]
        public void ValidationTest()
        {
            // Arrange
            Wallet validWallet = new Wallet("valid Wallet", "This is a valid wallet", "USD", 0m);
            Wallet invalidNameWallet = new Wallet("", "This is a valid wallet", "USD", 0m);
            Wallet invalidDescrWallet = new Wallet("valid Wallet", "", "USD", 0m);
            Wallet invalidCurrencyWallet = new Wallet("valid Wallet", "This is a valid wallet", "UsD", 0m);

            // Act
            bool isNameValid = invalidNameWallet.Validate();
            bool isDescrValid = invalidDescrWallet.Validate();
            bool isCurrencyValid = invalidCurrencyWallet.Validate();
            bool isValidOneValid = validWallet.Validate();

            // Assert
            Assert.False(isNameValid);
            Assert.False(isDescrValid);
            Assert.False(isCurrencyValid);
            Assert.True(isValidOneValid);
        }

        [Fact]
        public void BalanceTest()
        {
            // Arrange
            Wallet wallet = new Wallet("Ilia's Poeta", "This is my wallet", "UAH", 10000);
            wallet.AddCategory(new Category(Color.Black));

            wallet.AddTransaction(new Transaction(DateTime.Now, 14.5m));
            wallet.AddTransaction(new Transaction(DateTime.Now.AddDays(-10), 150m));
            wallet.AddTransaction(new Transaction(DateTime.Now.AddDays(-50), 1100m));
            wallet.AddTransaction(new Transaction(DateTime.Now, -1000m));

            decimal expectedBalance = 10264.5m;

            // Act
            decimal balance = wallet.Balance;

            // Assert
            Assert.Equal(expectedBalance, balance);
        }

        [Fact]
        public void LastMonthTransactionsTest()
        {
            // Arrange
            Wallet wallet = new Wallet("Ilia's Poeta", "This is my wallet", "UAH", 0m);
            wallet.AddCategory(new Category(Color.Black));

            wallet.AddTransaction(new Transaction(DateTime.Now, 14.5m));
            wallet.AddTransaction(new Transaction(DateTime.Now.AddDays(-14), 150m));
            wallet.AddTransaction(new Transaction(DateTime.Now.AddDays(-1), -15.5m));
            wallet.AddTransaction(new Transaction(DateTime.Now.AddDays(-10), 30m));
            wallet.AddTransaction(new Transaction(DateTime.Now.AddDays(-20), -14.5m));
            // not included
            wallet.AddTransaction(new Transaction(DateTime.Now.AddDays(-32), -100m));
            wallet.AddTransaction(new Transaction(DateTime.Now.AddDays(-50), 500m));

            decimal expectedIncome = 194.5m;
            decimal expectedExpenses = 30m;

            // Act
            decimal income = wallet.GetLastMonthIncome();
            decimal expenses = wallet.GetLastMonthExpenses();

            // Assert
            Assert.Equal(expectedIncome, income);
            Assert.Equal(expectedExpenses, expenses);
        }

        [Fact]
        public void ChangeTransactionTest()
        {
            // Arrange
            Wallet wallet = new Wallet("Ilia's Poeta", "This is my wallet", "UAH", 0m);

            wallet.AddCategory(new Category(Color.White, "Old", "OLD", null));
            wallet.AddCategory(new Category(Color.White, "New", "New", null));

            wallet.AddTransaction(new Transaction(DateTime.Now, 14.5m, "OLD", "UAH",
                new Category(Color.White, "Old", "OLD", null), new List<String>()));
            decimal expectedSum = 0;
            string expectedName = "new name";
            string expectedDescription = "new description";
            string expectedCurrency = "NEW";
            Category expectedCategory = new Category(Color.White, "New", "New", null);
            DateTime expectedDate = DateTime.Now.AddDays(10);

            // Act
            wallet.SetTransactionSum(0, expectedSum);
            wallet.SetTransactionDescription(0, expectedDescription);
            wallet.SetTransactionCurrency(0, expectedCurrency);
            wallet.SetTransactionCategory(0, expectedCategory);
            wallet.SetTransactionDate(0, expectedDate);

            // Assert
            Assert.Equal(expectedSum, wallet[0].Sum);
            Assert.Equal(expectedDescription, wallet[0].Description);
            Assert.Equal(expectedCurrency, wallet[0].Currency);
            Assert.Equal(expectedCategory, wallet[0].Category);
            Assert.Equal(expectedDate, wallet[0].Date);

        }

        [Fact]
        public void RemoveTransactionTest()
        {
            // Arrange
            Wallet wallet = new Wallet("Ilia's Poeta", "This is my wallet", "UAH", 0m);
            wallet.AddCategory(new Category(Color.White, "Old", "OLD", null));

            wallet.AddTransaction(new Transaction(DateTime.Now, 14.5m, "old description", "UAH",
                new Category(Color.White, "Old", "OLD", null), new List<String>()));

            // Act
            wallet.RemoveTransaction(0);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => wallet[0]);
        }

        [Fact]
        public void SortBySumTest()
        {

        }

        [Fact]
        public void SortByCurrencyTest()
        {

        }

        [Fact]
        public void SortByCategoryTest()
        {

        }

        [Fact]
        public void SortByDateTest()
        {

        }

        [Fact]
        public void SortByUserTest()
        {

        }
    }

}
