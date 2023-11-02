using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public Transfer CreateTransfer(Transfer incoming)
        {
            Transfer output = null;
            string sql = "Insert Into Transfers (UserFromId, UserToId, TransferType, TransferStatus, Amount) " +
                "OUTPUT Inserted.TansferId " +
                "Values (@userFrom, @userTo, @type, @status, @amount)";
            int newId = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userFrom", incoming.UserFromId);
                    cmd.Parameters.AddWithValue("@userTo", incoming.UserToId);
                    cmd.Parameters.AddWithValue("@type", incoming.TransferType);
                    cmd.Parameters.AddWithValue("@status", incoming.TransferStatus);
                    cmd.Parameters.AddWithValue("@amount", incoming.Amount);

                    newId = Convert.ToInt32(cmd.ExecuteScalar());

                    output = GetTransferById(newId);
                }
            }
            catch (SqlException ex)
            {

                throw new DaoException("Sql exception occurred", ex);
            }

            return output;
        }

        public Transfer EditTransferStatus(Transfer incoming)
        {
            throw new System.NotImplementedException();
        }

        public List<Transfer> GetAllTransfers()
        {
            throw new System.NotImplementedException();
        }

        public List<Transfer> GetAllTransfersByStatusAndPersonId(int personId, string tranferStatus)
        {
            throw new System.NotImplementedException();
        }

        public List<Transfer> GetAllTransfersByTypeAndPersonId(int personId, string transferType)
        {
            throw new System.NotImplementedException();
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

        public List<Transfer> GetTransfersByPersonId(int personId)
        {
            throw new System.NotImplementedException();
        }

        public Transfer MapRowToTransfer(SqlDataReader reader)
        {
            Transfer output = new Transfer();

            output.TransferId = Convert.ToInt32(reader["TransferId"]);
            output.UserFromId = Convert.ToInt32(reader["UserFromId"]);
            output.UserToId = Convert.ToInt32(reader["UserToId"]);
            output.TransferType = Convert.ToString(reader["TransferType"]);
            output.TransferStatus = Convert.ToString(reader["TransferStatus"]);
            output.Amount = Convert.ToDecimal(reader["Amount"]);

            return output;
        }
    }
}
