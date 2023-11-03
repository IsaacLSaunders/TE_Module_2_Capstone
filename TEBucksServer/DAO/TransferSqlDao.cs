using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TEBucksServer.Exceptions;
using TEBucksServer.Models;

namespace TEBucksServer.DAO
{

    public class TransferSqlDao : ITransferDao
    {
        private readonly string ConnectionString;

        public TransferSqlDao(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public TransferSqlDao() { }

        public TransferDto CreateTransfer(Transfer incoming)
        {
            TransferDto output = null;
            string sql = "Insert Into Transfers (UserFromId, UserToId, TransferType, TransferStatus, Amount) " +
                "OUTPUT Inserted.TransferId Values ((Select Persons.Id FROM Persons WHERE Persons.LoginId = @userFrom)," +
                " (Select Persons.Id FROM Persons WHERE Persons.LoginId = @userTo), @type, @status, @amount)";
            int newId = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userFrom", incoming.UserFrom);
                    cmd.Parameters.AddWithValue("@userTo", incoming.UserTo);
                    cmd.Parameters.AddWithValue("@type", incoming.TransferType);
                    cmd.Parameters.AddWithValue("@status", incoming.TransferStatus);
                    cmd.Parameters.AddWithValue("@amount", incoming.Amount);

                    newId = Convert.ToInt32(cmd.ExecuteScalar());

                    output = GetTransferDtoById(newId);
                }
            }
            catch (SqlException ex)
            {

                throw new DaoException("Sql exception occurred", ex);
            }

            return output;
        }

        public Transfer EditTransferStatus(TransferStatusUpdateDto status, int id)
        {
            Transfer output = null;
            string sql = "Update Transfers Set TransferStatus = @status Where TransferId = @transferId;";

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@status",status.TransferStatus);
                    cmd.Parameters.AddWithValue("@transferId", id);

                    int newId = Convert.ToInt32(cmd.ExecuteScalar());

                    output = GetTransferById(newId);
                }
            }
            catch (SqlException ex)
            {

                throw new DaoException("Sql exception ocurred", ex);
            }
            return output;
        }

        public List<Transfer> GetAllTransfersByStatusAndPersonId(int personId, string tranferStatus)
        {
            List<Transfer> output = new List<Transfer>();
            string sql = "Select TransferId, UserFromId, UserToId, TransferType, TransferStatus, Amount " +
                "From Transfers Join Persons As FromId On Transfers.UserFromId = FromId.Id Join Persons " +
                "As ToId On Transfers.UserToId = ToId.Id Where TransferStatus = @status And (FromId.Id = @id Or ToId.Id = @id);";

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", personId);
                    cmd.Parameters.AddWithValue("@status", tranferStatus);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        output.Add(MapRowToTransfer(reader));
                    }

                }
            }
            catch (SqlException ex)
            {

                throw new DaoException("Sql exception ocurred", ex);
            }
            return output;
        }

        public List<Transfer> GetAllTransfersByTypeAndPersonId(int personId, string transferType)
        {
            List<Transfer> output = new List<Transfer>();
            string sql = "Select TransferId, UserFromId, UserToId, TransferType, TransferStatus, Amount " +
                "From Transfers Join Persons As FromId On Transfers.UserFromId = FromId.Id Join Persons " +
                "As ToId On Transfers.UserToId = ToId.Id Where TransferType = @type And (FromId.Id = @id Or ToId.Id = @id);";

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", personId);
                    cmd.Parameters.AddWithValue("@type", transferType);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        output.Add(MapRowToTransfer(reader));
                    }

                }
            }
            catch (SqlException ex)
            {

                throw new DaoException("Sql exception ocurred", ex);
            }
            return output;
        }

        public Transfer GetTransferById(int transferId)
        {

            Transfer output = null;
            string sql = "SELECT TransferId, UserFromId, UserToId, TransferType, TransferStatus, Amount " +
                "FROM Transfers WHERE TransferId = @transferId;";

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("transferId", transferId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        output = MapRowToTransfer(reader);
                    }


                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("Sql exception occurred", ex);
            }

            return output;

        }

        public TransferDto GetTransferDtoById (int transferId)
        {
            IUserDao UserDao = new UserSqlDao(ConnectionString);

            Transfer output = null;
            TransferDto actualOutput = new TransferDto();

            string sql = "SELECT TransferId, UserFromId, UserToId, TransferType, TransferStatus, Amount " +
                "FROM Transfers WHERE TransferId = @transferId;";

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("transferId", transferId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        output = MapRowToTransfer(reader);
                    }

                    User userFrom = UserDao.GetUserByPersonId(output.UserFrom);
                    User userTo = UserDao.GetUserByPersonId(output.UserTo);

                    actualOutput.TransferId = output.TransferId;
                    actualOutput.userFrom = UserDao.GetUserByPersonId(output.UserFrom);
                    actualOutput.userTo = UserDao.GetUserByPersonId(output.UserTo);
                    actualOutput.TransferType = output.TransferType;
                    actualOutput.TransferStatus = output.TransferStatus;
                    actualOutput.Amount = output.Amount;

                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("Sql exception occurred", ex);
            }

            return actualOutput;

        }



        public List<TransferDto> GetTransfersByPersonId(int userId)
        {
            IUserDao UserDao = new UserSqlDao(ConnectionString);

            List<Transfer> output = new List<Transfer>();
            List<TransferDto> actual = new List<TransferDto>();
            string sql = "Select TransferId, UserFromId, UserToId, TransferType, TransferStatus, Amount From Transfers " +
                "WHERE UserFromId = " +
                "(Select persons.Id FROM Persons JOIN users ON persons.LoginId = users.user_id WHERE user_id = @userId) " +
                "OR UserToId = " +
                "(Select persons.Id FROM Persons JOIN users ON persons.LoginId = users.user_id WHERE user_id = @userId);";

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        output.Add(MapRowToTransfer(reader));
                    }

                }
            }
            catch (SqlException ex)
            {

                throw new DaoException("Sql exception ocurred", ex);
            }

            foreach(Transfer transfer in output)
            {
                TransferDto actualOutput = new TransferDto();


                User userFrom = UserDao.GetUserByPersonId(transfer.UserFrom);
                User userTo = UserDao.GetUserByPersonId(transfer.UserTo);

                actualOutput.TransferId = transfer.TransferId;
                actualOutput.userFrom = UserDao.GetUserByPersonId(transfer.UserFrom);
                actualOutput.userTo = UserDao.GetUserByPersonId(transfer.UserTo);
                actualOutput.TransferType = transfer.TransferType;
                actualOutput.TransferStatus = transfer.TransferStatus;
                actualOutput.Amount = transfer.Amount;
            }

            return actual;
        }

        public Transfer MapRowToTransfer(SqlDataReader reader)
        {
            Transfer output = new Transfer();

            output.TransferId = Convert.ToInt32(reader["TransferId"]);
            output.UserFrom = Convert.ToInt32(reader["UserFromId"]);
            output.UserTo = Convert.ToInt32(reader["UserToId"]);
            //output.UserFromObj = MapRowToUser(reader);
            //output.UserToObj = MapRowToUser(reader);
            output.TransferType = Convert.ToString(reader["TransferType"]);
            output.TransferStatus = Convert.ToString(reader["TransferStatus"]);
            output.Amount = Convert.ToDecimal(reader["Amount"]);

            return output;
        }

        //public User MapRowToUser(SqlDataReader reader)
        //{
        //    User user = new User();
        //    user.UserId = Convert.ToInt32(reader["user_id"]);
        //    user.Username = Convert.ToString(reader["username"]);
        //    user.PasswordHash = Convert.ToString(reader["password_hash"]);
        //    user.Salt = Convert.ToString(reader["salt"]);
        //    return user;
        //}
    }
}
