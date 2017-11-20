using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Repository
{
    public interface IUserRepository
    {
        User GetUser(string username);

        void AddPaymentHistory(string username, double amount);
    }
}
