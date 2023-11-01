using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Security.Cryptography.Xml;
using TEBucksServer.Exceptions;
using TEBucksServer.Models;

namespace TEBucksServer.DAO
{
    public class AccountSqlDao : IAccountDao
    {
        private readonly string connectionString;

        public AccountSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Account CreateAccount(int id)
        {
            Account newAccount = new Account();
            string sql = "insert into Accounts(PersonId,Balance) output inserted.AccountId values(@personid,@balance)";
            int newId = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql,conn);
                    cmd.Parameters.AddWithValue("@personid",id);
                    cmd.Parameters.AddWithValue("@balance", newAccount.Balance);

                    newId = Convert.ToInt32(cmd.ExecuteScalar());

                    newAccount = GetAccountById(newId);

                }
            }
            catch (SqlException e)
            {

                throw new DaoException(e.Message);
            }
            return newAccount;
        }

        private Account GetAccountById(int newId)
        {
            Account newAccount = null;
            string sql = "select AccountId,PersonId,Balance from accounts where AccountId = @accountId";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql,conn);
                    cmd.Parameters.AddWithValue("@accountId", newId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        newAccount = MapRowToAccount(reader);
                    }


                }       
            }
            catch (SqlException e)
            {

                throw new DaoException(e.Message);
            }
            return newAccount;
        }

        private Account MapRowToAccount(SqlDataReader reader)
        {
            Account output = new Account();
            output.AccountId = Convert.ToInt32(reader["AccountId"]);
            output.UserId = Convert.ToInt32(reader["PersonId"]);
            output.Balance = Convert.ToDecimal(reader["Balance"]);

            return output;
        }
    }
}
