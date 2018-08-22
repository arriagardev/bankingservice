using BankingService.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using ThirdPartyLibraries;

namespace BankingService
{
   public class BankingService
   {      
        static AccountRepository Repository;

        public BankingService()
        {
            Repository = new AccountRepository();
        }

      public void Deposit(decimal amount, string toAccountNumber)
      {         

         ITransactAccount toAccount = Repository.GetAccount(toAccountNumber);

            if (toAccount == null)
            {
                throw new KeyNotFoundException($"The Account number {toAccountNumber} does not exist.");
            }            

            toAccount.MoveCashIn(amount);
            Repository.SaveAccount(toAccount);
         Console.WriteLine($"Successful deposit of {amount:C} to {toAccount.Name} ({toAccount.AccountNumber})");
      }

      public void Withdraw(decimal amount, string fromAccountNumber)
      {

         ITransactAccount account = Repository.GetAccount(fromAccountNumber);

            if (account == null)
            {
                throw new KeyNotFoundException($"Account number {account} does not exist.");
            }


            if (!(account is ITransactCashOutAccount))                
            {
                throw new InvalidOperationException("Invalid from account type for withdraw.");
            }

            ITransactCashOutAccount fromAccount = (ITransactCashOutAccount)account;
            fromAccount.MoveCashOut(amount);
            Repository.SaveAccount(fromAccount);
         Console.WriteLine($"Successful withdrawal of {amount:C} from {fromAccount.Name} ({fromAccount.AccountNumber})");
      }

      public void Transfer(decimal amount, string fromAccountNumber, string toAccountNumber)
      {
         
            ITransactAccount account = Repository.GetAccount(fromAccountNumber);
            if (account == null)
            {
                throw new KeyNotFoundException($"Account number {account} does not exist.");
            }
            ITransactAccount toAccount = Repository.GetAccount(toAccountNumber);
            if (toAccount == null)
            {
                throw new KeyNotFoundException($"Account number {toAccount} does not exist.");
            }
            if (!(account is ITransactCashOutAccount))
            {
                throw new InvalidOperationException("Invalid from account type for Transfer.");
            }

            ITransactCashOutAccount fromAccount = (ITransactCashOutAccount)account;

            fromAccount.MoveCashOut(amount);
            toAccount.MoveCashIn(amount);
            Repository.SaveAccount(fromAccount);
            Repository.SaveAccount(toAccount);
         Console.WriteLine($"Successful transfer of {amount:C} from {fromAccount.Name} ({fromAccount.AccountNumber}) to {toAccount.Name} ({toAccount.AccountNumber})");
      }

      public virtual void PayBill(decimal amount, string fromAccountNumber, string payeeId, string billingAccountId)
      {
         //if (amount <= 0)
         //{
         //   throw new ArgumentException("Amount must be greater than 0.", nameof(amount));
         //}

         ITransactAccount account = Repository.GetAccount(fromAccountNumber);

            //if (fromAccount == null)
            //{
            //   throw new KeyNotFoundException($"Account number {fromAccountNumber} does not exist.");
            //}

            //if (fromAccount is SavingsAccount)
            //{
            //   // Asset account balances decrease on withdraw.
            //   fromAccount.Balance -= amount;
            //}
            //else if (fromAccount is CreditCardAccount)
            //{
            //   // Liability account balances increase on withdraw.
            //   fromAccount.Balance += amount;
            //}
            //else
            //{
            //   // Mortgage accounts do not support withdraw.
            //   throw new InvalidOperationException("Invalid from account type for bill payment.");
            //}

            //try
            //{
            //   BillPaymentService.ProcessBillPayment(amount, payeeId, billingAccountId);
            //}
            //catch (Exception ex)
            //{
            //   throw new ApplicationException($"Bill payment failed with error: {ex.Message}", ex);
            //}

            if (!(account is ITransactCashOutAccount))
            {
                throw new InvalidOperationException("Invalid from account type for bill payment.");
            }

            ITransactCashOutAccount fromAccount = (ITransactCashOutAccount)account;

            fromAccount.MoveCashOut(amount);
            Repository.SaveAccount(fromAccount);
         Console.WriteLine($"Successful bill payment of {amount:C} from {fromAccount.Name} ({fromAccount.AccountNumber})");
      }
   }
}
