using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingService.Account
{
    public class AccountRepository
    {
        private static List<ITransactAccount> accounts;

        static AccountRepository()
        {
            accounts = new List<ITransactAccount>();
            accounts.Add(new SavingsAccount("225893", "Vacation", 15928.47m));
            accounts.Add(new SavingsAccount("285901", "Car", 8059.23m));
            accounts.Add(new CreditCardAccount("599051", "Visa", 9105.39m));
            accounts.Add(new MortgageAccount("312590", "Mortgage", 159015.10m));
        }

        public List<ITransactAccount> GetAccounts()
        {
            return accounts.OrderBy(a => a.AccountNumber).Select(a => a.ShallowCopy()).ToList();
        }

        public ITransactAccount GetAccount(string accountNumber)
        {
            return accounts.Find(a => a.AccountNumber == accountNumber)?.ShallowCopy();
        }

        public void SaveAccount(ITransactAccount account)
        {
            ITransactAccount oldAccount = accounts.Find(a => a.AccountNumber == account.AccountNumber);

            if (oldAccount != null)
            {
                accounts.Remove(oldAccount);
            }

            accounts.Add(account.ShallowCopy());
        }
    }
}
