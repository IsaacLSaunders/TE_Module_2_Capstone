using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEBucksServer.DAO;
using TEBucksServer.Models;
using TEBucksServer.Security.Models;

namespace TeBucksServer.Tests.DAO
{
    [TestClass]
    public class UserSqlDaoTests : BaseDaoTests
    {

        private static readonly User USER_1 = new User() { UserId = 1010, FirstName = "joe", LastName = "shmoe", PasswordHash = "qXY8hY+tRMaJNlyHR7bul8Zs4gI=", Salt = "hNkZDaAlH/I=", Username = "username" };
        private static readonly User USER_2 = new User() { UserId = 1011, FirstName = "joe", LastName = "shmoe", PasswordHash = "B1cY6QY5wqkYrOPwqcliexMVo0U=", Salt = "X2zkWqvduXU=", Username = "username1" };
        private static readonly User USER_3 = new User() { UserId = 1012, FirstName = "bob", LastName = "bob", PasswordHash = "EpeAL9u572FDuvOwSe3fiWJczqc=", Salt = "ZL7trxpV8qs=", Username = "bob" };
        private static readonly User USER_4 = new User() { UserId = 1013, FirstName = "jim", LastName = "jim", PasswordHash = "RmszImAgkKUlxb54mCip57JUlVo=", Salt = "9kc0+u06DxA=", Username = "jim" };

        private IUserDao dao;

        private User testUser;

        [TestInitialize]
        public override void Setup()
        {
            dao = new UserSqlDao(ConnectionString);
            testUser = new User() { UserId = 1014, FirstName = "james", LastName = "james", PasswordHash = "+thi5mQNRmxZ6Jr6hwB4KfZwrIE=", Salt = "BG+0p0QaJ9A=", Username = "james" };
            base.Setup();
        }

        [TestMethod]
        public void GetUserByUserId_HappyPath()
        {
            User user = dao.GetUserById(1010);
            AssertUsersMatch(USER_1, user);

            user = dao.GetUserById(1011);
            AssertUsersMatch(USER_2, user);

            user = dao.GetUserById(1012);
            AssertUsersMatch(USER_3, user);

            user = dao.GetUserById(1013);
            AssertUsersMatch(USER_4, user);
        }

        [TestMethod]
        public void GetUserByUserName_HappyPath()
        {
            User user = dao.GetUserByUsername("username");
            AssertUsersMatch(USER_1, user);

            user = dao.GetUserByUsername("username1");
            AssertUsersMatch(USER_2, user);

            user = dao.GetUserByUsername("bob");
            AssertUsersMatch(USER_3, user);

            user = dao.GetUserByUsername("jim");
            AssertUsersMatch(USER_4, user);
        }

        [TestMethod]
        public void GetAllUsers_HappyPath()
        {
            List<User> expected = new List<User>() { USER_1, USER_2, USER_3, USER_4 };
            List<User> actual = dao.GetUsers();

            for (int i =0; i< actual.Count; i++)
            {
                AssertUsersMatch(expected[i], actual[i]);
            }
        }

        [TestMethod]
        public void CreateUser_HappyPath()
        {

            //NOT SURE IF THIS IS TESTABLE WITHOUT REMOVING THE HASH AND SALT FROM THE TEST
                //THE HASH AND SALT WILL ALWAYS CHANGE
            User actual = dao.CreateUser("james", "james", "james", "james");
            AssertUsersCreatedMatch(testUser, actual);
        }

        public void AssertUsersMatch(User expected , User actual)
        {
            Assert.AreEqual(expected.UserId, actual.UserId);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.PasswordHash, actual.PasswordHash);
            Assert.AreEqual(expected.Salt, actual.Salt);
            Assert.AreEqual(expected.Username, actual.Username);
        }

        public void AssertUsersCreatedMatch(User expected, User actual)
        {
            Assert.AreEqual(expected.UserId, actual.UserId);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.Username, actual.Username);
        }
    }
}
