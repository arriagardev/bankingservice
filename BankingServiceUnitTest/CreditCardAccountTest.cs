using System;
using BankingService.Account;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankingServiceUnitTest
{
    [TestClass]
    public class CreditCardAccountTest
    {
        static AccountRepository repository;
        static BankingService.BankingService service;

        [TestInitialize]
        public void Initialize()
        {
            repository = new AccountRepository();
            service = new BankingService.BankingService();
        }

        [TestMethod]
        public void MoveCashInSuccess()
        {
            var account = repository.GetAccount("599051");
            var balance = account.Balance;
            account.MoveCashIn(100);            
            Assert.IsTrue(account.Balance == balance - 100);
        }

        [TestMethod]
        public void MoveCashOutSuccess()
        {
            var account = repository.GetAccount("599051");
            var balance = account.Balance;
            ITransactCashOutAccount fromAccount = (ITransactCashOutAccount)account;
            fromAccount.MoveCashOut(100);            
            Assert.IsTrue(account.Balance == balance + 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MoveCashInInvalidAmount()
        {
            var account = repository.GetAccount("599051");            
            account.MoveCashIn(0);            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MoveCashOutInvalidAmount()
        {
            var account = repository.GetAccount("599051");
            ITransactCashOutAccount fromAccount = (ITransactCashOutAccount)account;
            fromAccount.MoveCashOut(0);
        }
    }
}
