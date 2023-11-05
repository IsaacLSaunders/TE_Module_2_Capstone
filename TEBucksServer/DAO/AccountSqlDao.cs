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
        const decimal StartingBalance = 1000M;


        public AccountSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Account CreateAccount(int id)
        {
            Account newAccount = null;
            string sql = "INSERT INTO accounts (userId, balance) " +
                "OUTPUT INSERTED.account_id " +
                "VALUES (@userId, @defBal);";
            int newId = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql,conn);
                    cmd.Parameters.AddWithValue("@userId", id);
                    cmd.Parameters.AddWithValue("@defBal", StartingBalance);

                    newId = Convert.ToInt32(cmd.ExecuteScalar());

                    newAccount = GetAccountByAccountId(newId);
                }
            }
            catch (SqlException ex)
            {

                throw new DaoException("Sql exception ocurred", ex);
            }
            return newAccount;
        }

        public Account GetAccountByAccountId(int newId)
        {
            Account account = null;
            string sql = "SELECT account_id, userId, balance FROM accounts WHERE account_id = @accountId;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@accountId", newId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        account = MapRowToAccount(reader);
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new DaoException("Sql exception ocurred", ex);
            }
            return account;
        }

        public Account GetAccountByUserId(int newId)
        {
            Account account = null;
            string sql = "SELECT account_id, userId, balance FROM accounts WHERE userId = @userId;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql,conn);
                    cmd.Parameters.AddWithValue("@userId", newId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        account = MapRowToAccount(reader);
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new DaoException("Sql exception ocurred", ex);
            }
            return account;
        }

        /// <summary>
        /// send money to an account
        /// </summary>
        /// <param name="incoming"></param>
        /// <returns></returns>
        public bool IncrementBalance(Transfer incoming)
        {
            bool success = false;
            string sql = "";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);


                }
            }
            catch (SqlException ex)
            {

                throw new DaoException("Sql exception ocurred", ex);
            }
            return success;
        }

        /// <summary>
        /// request money from an account
        /// </summary>
        /// <param name="incoming"></param>
        /// <returns></returns>
        public bool DecrementBalance(Transfer incoming)
        {
            bool success = false;
            string sql = "";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);

                }
            }
            catch (SqlException ex)
            {

                throw new DaoException("Sql exception ocurred", ex);
            }
            return success;
        }

        private Account MapRowToAccount(SqlDataReader reader)
        {
            Account output = new Account();
            output.AccountId = Convert.ToInt32(reader["account_id"]);
            output.UserId = Convert.ToInt32(reader["userId"]);
            output.Balance = Convert.ToDecimal(reader["balance"]);

            return output;
        }
    }
}
