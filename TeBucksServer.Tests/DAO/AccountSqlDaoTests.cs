using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEBucksServer.DAO;
using TEBucksServer.Models;

namespace TeBucksServer.Tests.DAO
{
    [TestClass]
    public class AccountSqlDaoTests : BaseDaoTests
    {
        private static readonly Account Account_1 = new Account() { AccountId = 10, UserId = 1010, Balance = 1000};
        private static readonly Account Account_2 = new Account() { AccountId = 11, UserId = 1011, Balance = 1000};
        private static readonly Account Account_3 = new Account() { AccountId = 12, UserId = 1012, Balance = 1000};
        private static readonly Account Account_4 = new Account() { AccountId = 13, UserId = 1013, Balance = 1000};

        private IAccountDao dao;

        private Account testAccount;

        [TestInitialize]
        public override void Setup()
        {
            dao = new AccountSqlDao(ConnectionString);
            testAccount = new Account() { AccountId = 13, UserId = 1013, Balance = 1000 };
            base.Setup();
        }

        [TestMethod]
        public void GetAccountByAccountId_HappyPath()
        {
            Account account = dao.GetAccountByAccountId(10);
            AssertAccountsMatch(Account_1, account);

            account = dao.GetAccountByAccountId(11);
            AssertAccountsMatch(Account_2, account);

            account = dao.GetAccountByAccountId(12);
            AssertAccountsMatch(Account_3, account);

            account = dao.GetAccountByAccountId(13);
            AssertAccountsMatch(Account_4, account);

        }

        [TestMethod]
        public void GetAccountByUserId_HappyPath()
        {
            Account account = dao.GetAccountByUserId(1010);
            AssertAccountsMatch(Account_1, account);

            account = dao.GetAccountByUserId(1011);
            AssertAccountsMatch(Account_2, account);

            account = dao.GetAccountByUserId(1012);
            AssertAccountsMatch(Account_3, account);

            account = dao.GetAccountByUserId(1013);
            AssertAccountsMatch(Account_4, account);

        }

        //[TestMethod]
        //public void IncrementBalance_HappyPath(Transfer incoming)
        //{
        //    Assert.IsTrue(dao.IncrementBalance(incoming));

        //}

        //[TestMethod]
        //public void DecrementBalance_HappyPath(Transfer incoming)
        //{
        //    Assert.IsTrue(dao.DecrementBalance(incoming));

        //}


        public void AssertAccountsMatch(Account expected, Account actual)
        {
            Assert.AreEqual(expected.AccountId, actual.AccountId);
            Assert.AreEqual(expected.UserId, actual.UserId);
            Assert.AreEqual(expected.Balance, actual.Balance);
        }
    }
}
