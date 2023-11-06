using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEBucksServer.DAO;
using TEBucksServer.DTO;
using TEBucksServer.Models;

namespace TeBucksServer.Tests.DAO
{
    [TestClass]
    public class TransferSqlDaoTests : BaseDaoTests
    {
        private static readonly User USER_1 = new User() { UserId = 1010, FirstName = "joe", LastName = "shmoe", PasswordHash = "qXY8hY+tRMaJNlyHR7bul8Zs4gI=", Salt = "hNkZDaAlH/I=", Username = "username" };
        private static readonly User USER_2 = new User() { UserId = 1011, FirstName = "joe", LastName = "shmoe", PasswordHash = "B1cY6QY5wqkYrOPwqcliexMVo0U=", Salt = "X2zkWqvduXU=", Username = "username1" };
        private static readonly User USER_3 = new User() { UserId = 1012, FirstName = "bob", LastName = "bob", PasswordHash = "EpeAL9u572FDuvOwSe3fiWJczqc=", Salt = "ZL7trxpV8qs=", Username = "bob" };
        private static readonly User USER_4 = new User() { UserId = 1013, FirstName = "jim", LastName = "jim", PasswordHash = "RmszImAgkKUlxb54mCip57JUlVo=", Salt = "9kc0+u06DxA=", Username = "jim" };

        private static readonly Transfer TRANSFER_1 = new Transfer() { TransferId = 1, TransferType = "Send", TransferStatus = "Approved", UserFrom = USER_1, UserTo = USER_2 , Amount = 150.00M};
        private static readonly Transfer TRANSFER_2 = new Transfer() { TransferId = 2, TransferType = "Send", TransferStatus = "Approved", UserFrom = USER_1, UserTo = USER_3, Amount = 150.00M };
        private static readonly Transfer TRANSFER_3 = new Transfer() { TransferId = 3, TransferType = "Send", TransferStatus = "Approved", UserFrom = USER_1, UserTo = USER_4, Amount = 150.00M };
        private static readonly Transfer TRANSFER_4 = new Transfer() { TransferId = 4, TransferType = "Request", TransferStatus = "Pending", UserFrom = USER_2, UserTo = USER_1, Amount = 100.00M };
        private static readonly Transfer TRANSFER_5 = new Transfer() { TransferId = 5, TransferType = "Request", TransferStatus = "Pending", UserFrom = USER_2, UserTo = USER_3, Amount = 100.00M };
        private static readonly Transfer TRANSFER_6 = new Transfer() { TransferId = 6, TransferType = "Request", TransferStatus = "Pending", UserFrom = USER_2, UserTo = USER_4, Amount = 100.00M };
        private static readonly Transfer TRANSFER_7 = new Transfer() { TransferId = 7, TransferType = "Request", TransferStatus = "Approved", UserFrom = USER_3, UserTo = USER_1, Amount = 200.00M };
        private static readonly Transfer TRANSFER_8 = new Transfer() { TransferId = 8, TransferType = "Request", TransferStatus = "Approved", UserFrom = USER_3, UserTo = USER_2, Amount = 200.00M };
        private static readonly Transfer TRANSFER_9 = new Transfer() { TransferId = 9, TransferType = "Request", TransferStatus = "Approved", UserFrom = USER_3, UserTo = USER_4, Amount = 200.00M };
        private static readonly Transfer TRANSFER_10 = new Transfer() { TransferId = 10, TransferType = "Request", TransferStatus = "Rejected", UserFrom = USER_4, UserTo = USER_1, Amount = 200.00M };
        private static readonly Transfer TRANSFER_11 = new Transfer() { TransferId = 11, TransferType = "Request", TransferStatus = "Rejected", UserFrom = USER_4, UserTo = USER_2, Amount = 200.00M };
        private static readonly Transfer TRANSFER_12 = new Transfer() { TransferId = 12, TransferType = "Request", TransferStatus = "Rejected", UserFrom = USER_4, UserTo = USER_3, Amount = 200.00M };

        private ITransferDao dao;

        private NewTransferDto testTransferDto;
        private Transfer testTransfer;

        [TestInitialize]
        public override void Setup()
        {
            dao = new TransferSqlDao(ConnectionString);
            testTransferDto = new NewTransferDto() { transferType = "Send", userFrom = USER_3.UserId, userTo = USER_4.UserId, amount = 5.00M };
            testTransfer = new Transfer() { TransferId = 13, TransferType = "Send", TransferStatus = "Approved", UserFrom = USER_3, UserTo = USER_4, Amount = 5.00M };
            base.Setup();
        }

        [TestMethod]
        public void CreateTransfer_HappyPath()
        {
            //THIS TEST DOES NOT WORK BECAUSE OF DISTRIBUTED TRANSACTIONS, WHATEVER THAT MEANS...
            Transfer actual = dao.CreateTransfer(testTransferDto);
            AssertTransfersMatch(testTransfer, actual);

        }

        [TestMethod]
        public void EditTransferStatus_HappyPath()
        {

        }

        [TestMethod]
        public void GetTransferByUserFromId_HappyPath()
        {

        }

        [TestMethod]
        public void GetTransferByUserToId_HappyPath()
        {

        }

        [TestMethod]
        public void GetTransferByTransferId_HappyPath()
        {

        }

        [TestMethod]
        public void GetTransfersByUserName_HappyPath()
        {

        }

        public void AssertTransfersMatch(Transfer expected, Transfer actual)
        {
            Assert.AreEqual(expected.TransferId, actual.TransferId);
            Assert.AreEqual(expected.TransferStatus, actual.TransferStatus);
            Assert.AreEqual(expected.TransferType, actual.TransferType);
            AssertUsersMatch(expected.UserFrom, actual.UserFrom);
            AssertUsersMatch(expected.UserTo, actual.UserTo);
            Assert.AreEqual(expected.Amount, actual.Amount);
        }

        public void AssertUsersMatch(User expected, User actual)
        {
            Assert.AreEqual(expected.UserId, actual.UserId);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.PasswordHash, actual.PasswordHash);
            Assert.AreEqual(expected.Salt, actual.Salt);
            Assert.AreEqual(expected.Username, actual.Username);
        }
    }
}
