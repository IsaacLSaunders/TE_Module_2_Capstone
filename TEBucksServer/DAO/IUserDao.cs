using System.Collections.Generic;
using System.Data.SqlClient;
using TEBucksServer.Models;

namespace TEBucksServer.DAO
{
    public interface IUserDao
    {
        User GetUserById(int id);
        User GetUserByUsername(string username);
        User CreateUser(string username, string password, string firstName, string lastName);
        List<User> GetUsers();
        User MapRowToUser(SqlDataReader reader);

    }
}
