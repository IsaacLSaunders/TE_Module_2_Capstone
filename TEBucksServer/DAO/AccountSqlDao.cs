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
            Account newAccount = new Account();
            string sql = "";
            int newId = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql,conn);


                }
            }
            catch (SqlException e)
            {

                throw new DaoException(e.Message);
            }
            return newAccount;
        }

        public Account GetAccountByUserId(int newId)
        {
            Account newAccount = null;
            string sql = "";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql,conn);


                }       
            }
            catch (SqlException e)
            {

                throw new DaoException(e.Message);
            }
            return newAccount;
        }

        public Account GetAccountByPersonId(int newId)
        {
            Account newAccount = null;
            string sql = "";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);


                }
            }
            catch (SqlException e)
            {

                throw new DaoException(e.Message);
            }
            return newAccount;
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
            catch (SqlException e)
            {

                throw new DaoException(e.Message);
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
            catch (SqlException e)
            {

                throw new DaoException(e.Message);
            }
            return success;
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
