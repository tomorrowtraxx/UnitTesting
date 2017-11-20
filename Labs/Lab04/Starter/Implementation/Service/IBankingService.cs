using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Service
{
    public interface IBankingService
    {
        double GetBalance(string accountNumber);

        void MakePayment(string accountNumber, double amount);
    }
}
