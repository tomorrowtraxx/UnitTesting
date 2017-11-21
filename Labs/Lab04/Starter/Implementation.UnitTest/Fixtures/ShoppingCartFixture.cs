using Implementation.Repository;
using Implementation.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Implementation.UnitTest.Fixtures
{
    public class ShoppingCartFixture
    {
        private ShoppingCart _cart;
        private User _user;

        //private Mock<IUserRepository> _userRepository;
        //private Mock<IBankingService> _bankingService;

        public ShoppingCartFixture()
        {
            //_userRepository = new Mock<IUserRepository>();
            //_bankingService = new Mock<IBankingService>();

            _shoppingCartConfiguration = new ShoppingCartBuilder();
            _userConfiguration = new UserBuilder();
        }

        private ShoppingCartBuilder _shoppingCartConfiguration;
        public ShoppingCartBuilder ShoppingCartConfiguration
        {
            get
            {
                return _shoppingCartConfiguration;
            }
        }

        private UserBuilder _userConfiguration;
        public UserBuilder UserConfiguration
        {
            get
            {
                return _userConfiguration;
            }
        }

        public void Setup()
        {
            _cart = ShoppingCartConfiguration.Build();
            _user = UserConfiguration.Build();
            _shoppingCartConfiguration.UserRepository.Setup(x => x.GetUser(It.IsAny<string>())).Returns(_user);
        }

        public void CheckoutCart()
        {
            _cart.CheckOut();
        }

        public void AddProductToCart(Product product, int amount)
        {
            _cart.Add(product, amount);
        }

        public void AssertProductIsInCart(Product expectedItem, int expectedAmount)
        {
            Assert.IsTrue(_cart.Orders.ContainsKey(expectedItem));
            Assert.AreEqual(expectedAmount, _cart.Orders[expectedItem]);
        }

        public void VerifyBalanceFetched()
        {
            _shoppingCartConfiguration.BankingService.Verify(x => x.GetBalance(_user.AccountNumber));
        }

        public void VerifyUserFetched()
        {
            _shoppingCartConfiguration.UserRepository.Verify(x => x.GetUser(_cart.Owner));
        }

        public void AssertCartTotalIsCorrect(double amount)
        {
            Assert.AreEqual(amount, _cart.Total);
        }

        public class ShoppingCartBuilder
        {
            private string _userName;
            private Mock<IUserRepository> _userRepository;
            private Mock<IBankingService> _bankingService;

            public Mock<IUserRepository> UserRepository { get => _userRepository; }
            public Mock<IBankingService> BankingService { get => _bankingService; }

            public ShoppingCartBuilder()
            {
                _userName = "Jasper";
                _userRepository = new Mock<IUserRepository>();
                _bankingService = new Mock<IBankingService>();
            }

            public ShoppingCartBuilder WithUserName(string userName)
            {
                _userName = userName;
                return this;
            }

            //public ShoppingCartBuilder WithUserRepository(IUserRepository userRepository)
            //{
            //    _userRepository = userRepository;
            //    return this;
            //}

            //public ShoppingCartBuilder WithBankingService(IBankingService bankingService)
            //{
            //    _bankingService = bankingService;
            //    return this;
            //}

            public ShoppingCart Build()
            {
                return new ShoppingCart(_userName, _userRepository.Object, _bankingService.Object);
            }
        }

        public class UserBuilder
        {
            private string _name;
            private string _accountNumber;
            private DateTime _dateOfBirth;

            public UserBuilder()
            {
                _name = "Jasper";
                _accountNumber = "0000000000000";
                _dateOfBirth = new DateTime(1900, 1, 1);
            }

            public UserBuilder WithAccountNumber(string accountNumber)
            {
                _accountNumber = accountNumber;
                return this;
            }

            public UserBuilder WithDateOfBirth(DateTime dateOfBirth)
            {
                _dateOfBirth = dateOfBirth;
                return this;
            }

            public UserBuilder WithName(string name)
            {
                _name = name;
                return this;
            }

            public User Build()
            {
                return new User(_name, _dateOfBirth, _accountNumber);
            }
        }
    }
}
