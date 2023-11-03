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
            string sql = "insert into Accounts(PersonId,Balance) output inserted.PersonId values(@personid,@balance)";
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

                    newAccount = GetAccountByPersonId(newId);

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
            string sql = "select AccountId,PersonId,Balance from accounts " +
                "JOIN Persons ON Persons.Id = Accounts.PersonId " +
                "JOIN users ON Persons.LoginId = users.user_id " +
                "where users.user_id = @userId;";

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

        public Account GetAccountByPersonId(int newId)
        {
            Account newAccount = null;
            string sql = "SELECT AccountId, PersonId, Balance FROM accounts " +
                "JOIN Persons ON Persons.Id = Accounts.PersonId " +
                "JOIN users ON Persons.LoginId = users.user_id " +
                "WHERE users.user_id = @userId";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userId", newId);

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

        /// <summary>
        /// send money to an account
        /// </summary>
        /// <param name="incoming"></param>
        /// <returns></returns>
        public bool IncrementBalance(Transfer incoming)
        {
            bool success = false;
            string sql = "BEGIN TRANSACTION " +
                "UPDATE Accounts SET Balance = Balance - @amount " +
                "WHERE " +
                "(SELECT TOP 1 Accounts.PersonId " +
                "FROM Accounts " +
                "JOIN Persons ON Persons.Id = Accounts.PersonId " +
                "JOIN Transfers ON Transfers.UserFromId = Persons.Id " +
                "WHERE Transfers.UserFromId = @fromId) = Accounts.PersonId " +
                "UPDATE Accounts SET Balance = Balance + @amount " +
                "WHERE " +
                "(SELECT TOP 1 Accounts.PersonId " +
                "FROM Accounts " +
                "JOIN Persons ON Persons.Id = Accounts.PersonId " +
                "JOIN Transfers ON Transfers.UserToId = Persons.Id " +
                "WHERE Transfers.UserToId = @toId) = Accounts.PersonId " +
                "COMMIT";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@fromId",incoming.UserFrom);
                    cmd.Parameters.AddWithValue("@toId",incoming.UserTo);
                    cmd.Parameters.AddWithValue("@amount",incoming.Amount);

                    int affected = cmd.ExecuteNonQuery();
                    success = affected > 0 ? true : false;
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
            string sql = "BEGIN TRANSACTION " +
                "UPDATE Accounts SET Balance = Balance + @amount " +
                "WHERE " +
                "(SELECT TOP 1 Accounts.PersonId " +
                "FROM Accounts " +
                "JOIN Persons ON Persons.Id = Accounts.PersonId " +
                "JOIN Transfers ON Transfers.UserFromId = Persons.Id " +
                "WHERE Transfers.UserFromId = @fromId) = Accounts.PersonId " +
                "UPDATE Accounts SET Balance = Balance - @amount " +
                "WHERE " +
                "(SELECT TOP 1 Accounts.PersonId " +
                "FROM Accounts " +
                "JOIN Persons ON Persons.Id = Accounts.PersonId " +
                "JOIN Transfers ON Transfers.UserToId = Persons.Id " +
                "WHERE Transfers.UserToId = @toId) = Accounts.PersonId " +
                "COMMIT";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@fromId", incoming.UserFrom);
                    cmd.Parameters.AddWithValue("@toId", incoming.UserTo);
                    cmd.Parameters.AddWithValue("@amount", incoming.Amount);

                    int affected = cmd.ExecuteNonQuery();
                    success = affected > 0 ? true : false;
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
