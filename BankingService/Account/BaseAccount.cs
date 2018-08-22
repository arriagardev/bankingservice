using System;
using System.Collections.Generic;

namespace BankingService.Account
{
   public abstract class BaseAccount : IAccount
   {
      public string AccountNumber { get; private set; }

      public string Name { get; set; }

      public decimal Balance { get; protected set; }                        

      //public DateTime OpeningDate { get; }

      //public string OwnerId { get; }

      //public decimal MonthlyFee { get; set; }

        public static AccountRepository Repository;

      public BaseAccount(string accountNumber, string name, decimal balance)
      {
         this.AccountNumber = accountNumber;
         this.Name = name;
         this.Balance = balance;
            Repository = new AccountRepository();
      }

      public ITransactAccount ShallowCopy()
      {
         return (ITransactAccount)this.MemberwiseClone();
      }

      //public abstract void Open();

      //public abstract void Close();

      //public abstract void Freeze();

      //public abstract void UnFreeze();

      //public abstract void ChangeOwner(string newOwnerId);
      
    }
}
