using System;
using System.Collections.Generic;
using BankingService.Account;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankingServiceUnitTest
{
    [TestClass]
    public class BankingServiceTest
    {
        static AccountRepository repository;
        static BankingService.BankingService service;

        [TestInitialize]
        public void Initialize()
        {
            repository = new AccountRepository();
            service = new BankingService.BankingService();
        }

        // **Deposit Testing**

        [TestMethod]
        public void DepositSuccess()
        {
            var account = repository.GetAccount("225893");
            var balance = account.Balance;
            service.Deposit(100, "225893");
            account = repository.GetAccount("225893");
            Assert.IsTrue(balance + 100 == account.Balance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DepositInvalidAmount()
        {            
            service.Deposit(0, "225893");      
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void DepositInvalidAccount()
        {
            service.Deposit(100, "xxxxxx");
        }

        // **Withdraw Testing**

        [TestMethod]
        public void WithdrawSuccess()
        {
            var account = repository.GetAccount("285901");
            var balance = account.Balance;
            service.Withdraw(100, "285901");
            account = repository.GetAccount("285901");
            Assert.IsTrue(balance - 100 == account.Balance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WithdrawInvalidAmount()
        {            
            service.Withdraw(0, "285901");            
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void WithdrawInvalidAccount()
        {
            service.Withdraw(100, "xxxxxx");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WithdrawInvalidMortgage()
        {
            service.Withdraw(0, "312590");
        }

        // **Transfer Testing**

        [TestMethod]
        public void TransferSuccess()
        {
            var savingsaccount = repository.GetAccount("225893");
            var creditaccount = repository.GetAccount("599051");
            var savingsbalance = savingsaccount.Balance;
            var creditbalance = creditaccount.Balance;
            service.Transfer(100, "225893", "599051");
            savingsaccount = repository.GetAccount("225893");
            creditaccount = repository.GetAccount("599051");
            Assert.IsTrue(savingsaccount.Balance == savingsbalance - 100 && creditaccount.Balance == creditbalance - 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TransferInvalidAmount()
        {
            service.Transfer(0, "225893", "599051");
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TransferInvalidFromAccount()
        {
            service.Transfer(100, "xxxxxx", "599051");
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TransferInvalidToAccount()
        {
            service.Transfer(100, "225893", "xxxxxx");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TransferInvalidFromMortgage()
        {
            service.Transfer(0, "312590", "599051");
        }
    }
}
