using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingService.Account
{
    public interface ITransactCashOutAccount : ITransactAccount
    {
        void MoveCashOut(decimal amount);        
    }
}
