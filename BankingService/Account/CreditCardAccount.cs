using System;

namespace BankingService.Account
{
   public class CreditCardAccount : BaseTransactAccount, ITransactCashOutAccount
   {
      public decimal CreditLimit { get; set; }

      public decimal AnnualFee { get; set; }

      public CreditCardAccount(string accountNumber, string name, decimal balance)
         : base(accountNumber, name, balance)
      {
      }

      //public override void Open()
      //{
      //   throw new NotImplementedException();
      //}

      //public override void Close()
      //{
      //   throw new NotImplementedException();
      //}

      //public override void Freeze()
      //{
      //   throw new NotImplementedException();
      //}

      //public override void UnFreeze()
      //{
      //   throw new NotImplementedException();
      //}

      //public override void ChangeOwner(string newOwnerId)
      //{
      //   throw new NotImplementedException();
      //}

        public override void MoveCashIn(decimal amount)
        {
            if (ValidateAmount(amount))
            {
                this.Balance -= amount;
            }
        }

        public void MoveCashOut(decimal amount)
        {
            if (ValidateAmount(amount))
            {
                this.Balance += amount;
            }
        }
    }
}
