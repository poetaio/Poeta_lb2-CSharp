using System;
using System.Collections.Generic;
using Xunit;
using Wallets.BusinessLayer;
using System.Drawing;
using Wallets.BusinessLayer.Users;

namespace Wallets.BusinessLayerTests
{
    public class UserTests
    {
        [Fact]
        public void ValidationTest()
        {
            // Arrange
            User validUser = new User(Guid.NewGuid(), "Ilia", "Poeta", "em@c.com");
            User invalidNameUser = new User(new Guid(), "", "Poeta", "em@c.com");
            User invalidSurnameUser = new User(new Guid(), "Ilia", "", "em@c.com");

            // Act
            bool isValidValid = validUser.Validate();
            bool isInvalidNameValid = invalidNameUser.Validate();
            bool isInvalidSurnameValid = invalidSurnameUser.Validate();

            // Assert
            Assert.True(isValidValid);
            Assert.False(isInvalidNameValid);
            Assert.False(isInvalidSurnameValid);
        }

        [Fact]
        public void FullNameTest()
        {
            // Arrange
            User validUser = new User(new Guid(), "Ilia", "Poeta", "em@c.com");
            User invalidNameUser = new User(new Guid(), "", "Poeta", "em@c.com");
            User invalidSurnameUser = new User(new Guid(), "Ilia", "", "em@c.com");
            string expectedValidFullName = "Ilia Poeta";
            string expectedInvalidName = "Poeta";
            string expectedInvalidSurname = "Ilia";

            // Act
            string validFullName = validUser.FullName;
            string invalidNameUserFullName = invalidNameUser.FullName;
            string invalidSurnameUserFullName = invalidSurnameUser.FullName;

            // Assert
            Assert.Equal(expectedValidFullName, validFullName);
            Assert.Equal(expectedInvalidName, invalidNameUserFullName);
            Assert.Equal(expectedInvalidSurname, invalidSurnameUserFullName);
        }

        [Fact]
        public void SharedWalletTest()
        {
            // Arrange
            User firstUser = new User(new Guid(), "U1", "U1", "em@c.com");
            User secondUser = new User(new Guid(), "U2", "U2", "em@c.com");

            Wallet wallet = new Wallet("Wallet", "Descr", "USD", 50);
            wallet.AddCategory(new Category(Color.Black));

            wallet.AddTransaction(new Transaction(DateTime.Now, 0m, "Description",
                "USD", null, new List<string>()));
            firstUser.Wallets.Add(wallet);
            secondUser.Wallets.Add(wallet);

            firstUser.Wallets.Find(x => x.Name == wallet.Name).AddTransaction(new Transaction(DateTime.Now, 10m, "Description",
                "USD", null, new List<string>()));
            secondUser.Wallets.Find(x => x.Name == wallet.Name).AddTransaction(new Transaction(DateTime.Now, -100m, "Description",
                "USD", null, new List<string>()));

            decimal expectedBalance = -40;

            // Act
            decimal balanceFirstUser = firstUser.Wallets.Find(x => x.Name == wallet.Name).Balance;
            decimal balanceSecondUser = secondUser.Wallets.Find(x => x.Name == wallet.Name).Balance;

            // Assert
            Assert.Equal(expectedBalance, balanceFirstUser);
            Assert.Equal(expectedBalance, balanceSecondUser);
        }

        [Fact]
        public void UserTransactionsTest()
        {

        }

        [Fact]
        public void OtherUsersTransactionsTest()
        {

        }
    }
}
