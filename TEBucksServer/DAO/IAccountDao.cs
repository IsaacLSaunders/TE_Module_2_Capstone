using System.Data.SqlClient;
using TEBucksServer.Models;

namespace TEBucksServer.DAO
{
    public interface IAccountDao
    {
        public Account CreateAccount(int id);
        public Account GetAccountById(int newId);
    }
}
