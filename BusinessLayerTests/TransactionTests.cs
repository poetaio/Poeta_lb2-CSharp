using System;
using Xunit;
using Wallets.BusinessLayer;
using System.Collections.Generic;
using System.Drawing;

namespace Wallets.BusinessLayerTests
{
    public class TransactionTests
    {
        [Fact]
        public void ValidationTest()
        {

            // Arrange
            Transaction validTransaction = new Transaction(DateTime.Now, 0.0m, "Valid", "USD",
                new Category(Color.Black));
            Transaction invalidDescrTransaction = new Transaction(DateTime.Now, 0.0m, "", "USD",
                new Category(Color.Black));
            Transaction invalidCurrencyTransaction = new Transaction(DateTime.Now, 0.0m, "Invalid", "UsD",
                new Category(Color.Black));

            // Act
            bool isDescrValid = invalidDescrTransaction.Validate();
            bool isCurrencyValid = invalidCurrencyTransaction.Validate();
            bool isValidOneValid = validTransaction.Validate();

            // Assert
            Assert.False(isDescrValid);
            Assert.False(isCurrencyValid);
            Assert.True(isValidOneValid);
        }

        [Fact]
        public void CompareToTest()
        {
            // Arrange
            Transaction transaction1 = new Transaction(new DateTime(2020, 1, 1), 0.0m);

            Transaction transaction2 = new Transaction(new DateTime(2020, 1, 1), 0.0m);

            Transaction transaction3 = new Transaction(new DateTime(2020, 1, 2), 0.0m);

            // Act
            bool compare12 = transaction1.CompareTo(transaction2) == 0;
            bool compare23 = transaction2.CompareTo(transaction3) == -1;
            bool compare32 = transaction3.CompareTo(transaction2) == 1;

            // Assert
            Assert.True(compare12);
            Assert.True(compare23);
            Assert.True(compare32);
        }
    }
}
