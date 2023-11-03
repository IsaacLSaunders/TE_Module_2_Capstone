using System.Collections.Generic;
using TEBucksServer.Models;

namespace TEBucksServer.DAO
{
    public interface IUserDao
    {
        User GetUserById(int id);
        User GetUserByPersonId(int personId);
        User GetUserByUsername(string username);
        User CreateUser(string username, string password);
        List<User> GetUsers();
    }
}
