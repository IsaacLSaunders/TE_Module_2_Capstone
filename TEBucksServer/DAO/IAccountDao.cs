using System.Data.SqlClient;
using TEBucksServer.Models;

namespace TEBucksServer.DAO
{
    public interface IAccountDao
    {
        public Account CreateAccount(int id);
        public Account GetAccountByUserId(int newId);
        public Account GetAccountByPersonId(int newId);
        public bool IncrementBalance(Transfer incoming);
        public bool DecrementBalance(Transfer incoming);


    }
}
