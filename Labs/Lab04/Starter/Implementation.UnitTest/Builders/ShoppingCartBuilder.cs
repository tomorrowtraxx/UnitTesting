using Implementation.Repository;
using Implementation.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.UnitTest.Builders
{
    public class ShoppingCartBuilder
    {
        private string _userName;
        private IUserRepository _userRepository;
        private IBankingService _bankingService;

        public ShoppingCartBuilder()
        {
            _userName = "Jasper";
            _userRepository = null;
            _bankingService = null;
        }

        public ShoppingCartBuilder WithUserName(string userName)
        {
            _userName = userName;
            return this;
        }

        public ShoppingCartBuilder WithUserRepository(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            return this;
        }

        public ShoppingCartBuilder WithBankingService(IBankingService bankingService)
        {
            _bankingService = bankingService;
            return this;
        }

        public ShoppingCart Build()
        {
            return new ShoppingCart(_userName, _userRepository, _bankingService);
        }
    }
}
