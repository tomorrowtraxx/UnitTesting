﻿using Implementation.Repository;
using Implementation.Service;
using Implementation.UnitTest.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Implementation.UnitTest
{
    [TestClass]
    public class ShoppingCartTest
    {
        private Mock<IUserRepository> _userRepository;
        private Mock<IBankingService> _bankingService;

        private ShoppingCart _cart;

        private Product Xbox = new Product("Xbox 360", 199.99);
        private Product Playstation = new Product("PlayStation3", 250);

        [TestInitialize]
        public void TestInitialize()
        {
            _userRepository = new Mock<IUserRepository>();
            _bankingService = new Mock<IBankingService>();
        }

        [TestMethod]
        public void Cart_Should_Contain_Product_After_Product_Is_Added()
        {
            _cart = new ShoppingCartBuilder()
                .WithUserRepository(_userRepository.Object)
                .WithBankingService(_bankingService.Object)
                .Build();

            _cart.Add(Xbox, 2);
            AssertProductIsInCart(Xbox, 2);
        }

        [TestMethod]
        public void Cart_Should_Update_Amount_When_Same_Product_Is_Added_Twice()
        {
            _cart = new ShoppingCartBuilder()
                .WithUserRepository(_userRepository.Object)
                .WithBankingService(_bankingService.Object)
                .Build();

            _cart.Add(Xbox, 2);
            _cart.Add(Xbox, 3);

            AssertProductIsInCart(Xbox, 5);
        }

        [TestMethod]
        public void Cart_should_Contain_Different_Products_After_Products_Are_Added()
        {
            _cart = new ShoppingCartBuilder()
                .WithUserRepository(_userRepository.Object)
                .WithBankingService(_bankingService.Object)
                .Build();

            _cart.Add(Xbox, 1);
            _cart.Add(Playstation, 2);

            AssertProductIsInCart(Xbox, 1);
            AssertProductIsInCart(Playstation, 2);
        }

        [TestMethod]
        public void Empty_Cart_Total_Should_Be_Zero()
        {
            _cart = new ShoppingCartBuilder()
                .WithUserRepository(_userRepository.Object)
                .WithBankingService(_bankingService.Object)
                .Build();

            Assert.AreEqual(0, _cart.Total);
        }

        [TestMethod]
        public void Carts_Total_Should_Be_Sum_Of_Products_Price_Times_Amount()
        {
            _cart = new ShoppingCartBuilder()
                .WithUserRepository(_userRepository.Object)
                .WithBankingService(_bankingService.Object)
                .Build();

            _cart.Add(Playstation, 2); //500
            _cart.Add(Xbox, 1); //199.99

            Assert.AreEqual(699.99, _cart.Total);
        }

        [TestMethod]
        public void User_Should_Be_Fetched_From_Repository_On_Checkout()
        {
            User frank = new UserBuilder()
                .WithName("Frank")
                .WithDateOfBirth(new DateTime(1978, 9, 27))
                .WithAccountNumber("1234-84")
                .Build();

            _cart = new ShoppingCartBuilder()
                .WithUserRepository(_userRepository.Object)
                .WithBankingService(_bankingService.Object)
                .WithUserName("Frank")
                .Build();

            _userRepository.Setup(x => x.GetUser(It.IsAny<string>())).Returns(frank);

            _cart.Add(Xbox, 1);
            _cart.CheckOut();

            _userRepository.Verify(x => x.GetUser(_cart.Owner));
        }

        [TestMethod]
        public void Balance_Should_Be_Fetched_From_BankingService_On_Checkout()
        {
            User frank = new UserBuilder()
                .WithName("Frank")
                .WithDateOfBirth(new DateTime(1978, 9, 27))
                .WithAccountNumber("1234-84")
                .Build();

            _cart = new ShoppingCartBuilder()
                .WithUserRepository(_userRepository.Object)
                .WithBankingService(_bankingService.Object)
                .WithUserName("Frank")
                .Build();

            _userRepository.Setup(x => x.GetUser(It.IsAny<string>())).Returns(frank);

            _cart.Add(Xbox, 1);
            _cart.CheckOut();

            _bankingService.Verify(x => x.GetBalance(frank.AccountNumber));
        }

        private void AssertProductIsInCart(Product expectedItem, int expectedAmount)
        {
            Assert.IsTrue(_cart.Orders.ContainsKey(expectedItem));
            Assert.AreEqual(expectedAmount, _cart.Orders[expectedItem]);
        }
    }
}
