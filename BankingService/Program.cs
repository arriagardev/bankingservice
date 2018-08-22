using BankingService.Account;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingService
{
   class Program
   {
      static void Main(string[] args)
      {
            AccountRepository repository = new AccountRepository();
            BankingService service = new BankingService();

         List<ITransactAccount> accounts = repository.GetAccounts();

         string[,] openingBalances = new string[accounts.Count + 1, 3];
         openingBalances[0, 0] = "Acct#";
         openingBalances[0, 1] = "Acct Name";
         openingBalances[0, 2] = "Balance";

         for (int index = 0; index < accounts.Count; index++)
         {
            ITransactAccount account = accounts[index];
            openingBalances[index + 1, 0] = account.AccountNumber;
            openingBalances[index + 1, 1] = account.Name;
            openingBalances[index + 1, 2] = $"{account.Balance:C}";
         }

         Console.WriteLine("OPENING BALANCES:");
         PrintTable(openingBalances, 3, new int[] { 2 });

         Console.WriteLine();
         Console.WriteLine("TRANSACTIONS:");

         try
         {
            // Deposit $500 to Vacation account
            service.Deposit(500, ("225893"));
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }

         try
         {
            // Withdraw $250 from Car account
            service.Withdraw(250, "285901");
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }

         try
         {
            // Transfer $1000 from Visa account to Mortgage account
            service.Transfer(1000, "599051", "312590");
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }

         try
         {
            // Pay bill of $375.12 from Visa account
            service.PayBill(375.12m, "599051", "05213", "0029591839");
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }

         try
         {
            // Attempt to deposit a negative amount to the Vacation account
            service.Deposit(-100, "225893");
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }

         try
         {
            // Attempt to withdraw from the Mortgage account
            service.Withdraw(100, "312590");
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }

         accounts = repository.GetAccounts();

         string[,] closingBalances = new string[accounts.Count + 1, 3];
         closingBalances[0, 0] = "Acct#";
         closingBalances[0, 1] = "Acct Name";
         closingBalances[0, 2] = "Balance";

         for (int index = 0; index < accounts.Count; index++)
         {
            ITransactAccount account = accounts[index];
            closingBalances[index + 1, 0] = account.AccountNumber;
            closingBalances[index + 1, 1] = account.Name;
            closingBalances[index + 1, 2] = $"{account.Balance:C}";
         }

         Console.WriteLine();
         Console.WriteLine("CLOSING BALANCES:");
         PrintTable(closingBalances, 3, new int[] { 2 });

         Console.ReadKey();
      }

      private static void PrintTable(string[,] table, int columnSpacing, int[] rightAlignCols)
      {
         int[] maxWidth = new int[table.GetLength(1)];

         for (int row = 0; row < table.GetLength(0); row++)
         {
            for (int col = 0; col < table.GetLength(1); col++)
            {
               int length = table[row, col].Length;
               if (length > maxWidth[col])
               {
                  maxWidth[col] = length;
               }
            }
         }

         for (int row = 0; row < table.GetLength(0); row++)
         {
            for (int col = 0; col < table.GetLength(1); col++)
            {
               if (rightAlignCols.Contains(col))
               {
                  Console.Write(table[row, col].PadLeft(maxWidth[col]) + new string(' ', columnSpacing));
               }
               else
               {
                  Console.Write(table[row, col].PadRight(maxWidth[col] + columnSpacing));
               }
            }
            Console.WriteLine();
         }
      }
   }
}
