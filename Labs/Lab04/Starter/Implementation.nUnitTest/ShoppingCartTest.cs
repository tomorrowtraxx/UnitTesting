using Implementation.Repository;
using Implementation.Service;
using Moq;
using System;
using Xunit;

namespace Implementation.xUnitTest
{
    public class ShoppingCartTest
    {
        private Mock<IUserRepository> _userRepository;
        private Mock<IBankingService> _bankingService;

        private ShoppingCart _cart;

        private Product Xbox = new Product("Xbox 360", 199.99);
        private Product Playstation = new Product("PlayStation3", 250);

        private User Frank = new User("Frank", new DateTime(1978, 9, 27), "1234-84");

        public ShoppingCartTest()
        {
            _userRepository = new Mock<IUserRepository>();
            _bankingService = new Mock<IBankingService>();
            _cart = new ShoppingCart("Frank", _userRepository.Object, _bankingService.Object);

            _userRepository.Setup(x => x.GetUser(It.IsAny<string>())).Returns(Frank);
        }

        [Fact]
        public void Cart_Should_Contain_Product_After_Product_Is_Added()
        {
            _cart.Add(Xbox, 2);
            AssertProductIsInCart(Xbox, 2);
        }

        [Fact]
        public void Cart_Should_Update_Amount_When_Same_Product_Is_Added_Twice()
        {
            _cart.Add(Xbox, 2);
            _cart.Add(Xbox, 3);
            AssertProductIsInCart(Xbox, 5);
        }

        [Fact]
        public void Cart_should_Contain_Different_Products_After_Products_Are_Added()
        {
            _cart.Add(Xbox, 1);
            _cart.Add(Playstation, 2);
            AssertProductIsInCart(Xbox, 1);
            AssertProductIsInCart(Playstation, 2);
        }

        [Fact]
        public void Empty_Cart_Total_Should_Be_Zero()
        {
            Assert.Equal(0, _cart.Total);
        }

        [Fact]
        public void Carts_Total_Should_Be_Sum_Of_Products_Price_Times_Amount()
        {
            _cart.Add(Playstation, 2); //500
            _cart.Add(Xbox, 1); //199.99
            Assert.Equal(699.99, _cart.Total);
        }

        [Fact]
        public void User_Should_Be_Fetched_From_Repository_On_Checkout()
        {
            _cart.Add(Xbox, 1);
            _cart.CheckOut();

            _userRepository.Verify(x => x.GetUser(_cart.Owner));
        }

        [Fact]
        public void Balance_Should_Be_Fetched_From_BankingService_On_Checkout()
        {
            _cart.Add(Xbox, 1);
            _cart.CheckOut();

            _bankingService.Verify(x => x.GetBalance(Frank.AccountNumber));
        }

        private void AssertProductIsInCart(Product expectedItem, int expectedAmount)
        {
            Assert.True(_cart.Orders.ContainsKey(expectedItem));
            Assert.Equal(expectedAmount, _cart.Orders[expectedItem]);
        }
    }
}
