using Implementation.Repository;
using Implementation.Service;
using Implementation.UnitTest.Builders;
using Implementation.UnitTest.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Implementation.UnitTest
{
    [TestClass]
    public class ShoppingCartTest
    {
        //private Mock<IUserRepository> _userRepository;
        //private Mock<IBankingService> _bankingService;

        //private ShoppingCart _cart;

        private Product Xbox = new Product("Xbox 360", 199.99);
        private Product Playstation = new Product("PlayStation3", 250);

        [TestInitialize]
        public void TestInitialize()
        {
            //_userRepository = new Mock<IUserRepository>();
            //_bankingService = new Mock<IBankingService>();
        }

        [TestMethod]
        public void Cart_Should_Contain_Product_After_Product_Is_Added()
        {
            var fixture = new ShoppingCartFixture();

            fixture.Setup();

            fixture.AddProductToCart(Xbox, 2);

            fixture.AssertProductIsInCart(Xbox, 2);
        }

        [TestMethod]
        public void Cart_Should_Update_Amount_When_Same_Product_Is_Added_Twice()
        {
            var fixture = new ShoppingCartFixture();

            fixture.Setup();

            fixture.AddProductToCart(Xbox, 2);
            fixture.AddProductToCart(Xbox, 3);

            fixture.AssertProductIsInCart(Xbox, 5);
        }

        [TestMethod]
        public void Cart_should_Contain_Different_Products_After_Products_Are_Added()
        {
            var fixture = new ShoppingCartFixture();

            fixture.Setup();

            fixture.AddProductToCart(Xbox, 1);
            fixture.AddProductToCart(Playstation, 2);

            fixture.AssertProductIsInCart(Xbox, 1);
            fixture.AssertProductIsInCart(Playstation, 2);
        }

        [TestMethod]
        public void Empty_Cart_Total_Should_Be_Zero()
        {
            var fixture = new ShoppingCartFixture();

            fixture.Setup();

            //Assert.AreEqual(0, _cart.Total);
            fixture.AssertCartTotalIsCorrect(0);
        }

        [TestMethod]
        public void Carts_Total_Should_Be_Sum_Of_Products_Price_Times_Amount()
        {
            var fixture = new ShoppingCartFixture();

            fixture.Setup();

            fixture.AddProductToCart(Playstation, 2); //500
            fixture.AddProductToCart(Xbox, 1); //199.99

            //Assert.AreEqual(699.99, _cart.Total);
            fixture.AssertCartTotalIsCorrect(699.99);
        }

        [TestMethod]
        public void User_Should_Be_Fetched_From_Repository_On_Checkout()
        {
            var fixture = new ShoppingCartFixture();
            fixture.UserConfiguration
                .WithName("Frank")
                .WithDateOfBirth(new DateTime(1978, 9, 27))
                .WithAccountNumber("1234-84");
            fixture.ShoppingCartConfiguration
                .WithUserName("Frank");

            fixture.Setup();

            fixture.AddProductToCart(Xbox, 1);
            fixture.CheckoutCart();

            fixture.VerifyUserFetched();
        }

        [TestMethod]
        public void Balance_Should_Be_Fetched_From_BankingService_On_Checkout()
        {
            var fixture = new ShoppingCartFixture();
            fixture.UserConfiguration
                .WithName("Frank")
                .WithDateOfBirth(new DateTime(1978, 9, 27))
                .WithAccountNumber("1234-84");
            fixture.ShoppingCartConfiguration
                .WithUserName("Frank");

            fixture.Setup();

            fixture.AddProductToCart(Xbox, 1);
            fixture.CheckoutCart();

            fixture.VerifyBalanceFetched();
        }

        //private void AssertProductIsInCart(Product expectedItem, int expectedAmount)
        //{
        //    Assert.IsTrue(_cart.Orders.ContainsKey(expectedItem));
        //    Assert.AreEqual(expectedAmount, _cart.Orders[expectedItem]);
        //}
    }
}
