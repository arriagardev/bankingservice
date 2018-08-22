using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingService.Account
{
    public interface IAccount
    {
        string AccountNumber { get; }

        string Name { get; set; }

        decimal Balance { get; }
    }
}
