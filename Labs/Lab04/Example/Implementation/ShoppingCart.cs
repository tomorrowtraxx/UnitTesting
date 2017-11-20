using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Implementation.Repository;
using Implementation.Service;

namespace Implementation
{
    public class ShoppingCart
    {
        /// <summary>
        /// List to keep track of the items in the cart.
        /// </summary>
        public Dictionary<Product, int> Orders {get; private set;}

        private IUserRepository _userRepository;
        private IBankingService _bankingService;

        public ShoppingCart(string username, IUserRepository userRepository, IBankingService bankingService)
        {
            Orders = new Dictionary<Product, int>();
            _userRepository = userRepository;
            _bankingService = bankingService;
            Owner = username;
        }

        /// <summary>
        /// The owner of this shoping cart.
        /// </summary>
        public string Owner { get; private set; }

        /// <summary>
        /// Add a new item to this cart.
        /// </summary>
        /// <param name="item">The item to be added.</param>
        /// <param name="amount">The amount for this item to be added.</param>
        /// <remarks>
        /// When the item is already in the list, only the amount should be increased and no new item added.
        /// </remarks>
        public void Add(Product item, int amount)
        {
            if (Orders.ContainsKey(item))
            {
                Orders[item] += amount;
            }
            else
            {
                Orders.Add(item, amount);
            }
        }

        public double Total
        {
            get
            {
                double total = 0;
                foreach (var product in Orders.Keys)
                {
                    total += product.Price * Orders[product];
                }
                return total;
            }
        }

        public void CheckOut()
        {
            var user = _userRepository.GetUser(Owner);
            _bankingService.GetBalance(user.AccountNumber);
        }
    }
}
