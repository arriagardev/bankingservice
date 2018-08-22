using System;

namespace BankingService.Account
{
   public interface ITransactAccount : IAccount
   {      
      ITransactAccount ShallowCopy();

      void MoveCashIn(decimal amount);      
    }
}
