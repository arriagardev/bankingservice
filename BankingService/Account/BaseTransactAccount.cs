using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingService.Account
{
    public abstract class BaseTransactAccount : BaseAccount, ITransactAccount
    {
        public BaseTransactAccount(string accountNumber, string name, decimal balance) : base(accountNumber, name, balance)
        {
            
        }

        public abstract void MoveCashIn(decimal amount);

        public virtual bool ValidateAmount(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than 0.", nameof(amount));
            }

            ITransactAccount toAccount = Repository.GetAccount(AccountNumber);

            if (toAccount == null)
            {
                throw new KeyNotFoundException($"Account number {AccountNumber} does not exist.");
            }

            return true;
        }
    }
}
