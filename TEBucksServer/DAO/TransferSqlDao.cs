﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using TEBucksServer.DTO;
using TEBucksServer.Exceptions;
using TEBucksServer.Models;

namespace TEBucksServer.DAO
{

    public class TransferSqlDao : ITransferDao
    {
        private readonly string ConnectionString;
        private readonly IUserDao UserDao;



        public TransferSqlDao(string connectionString)
        {
            ConnectionString = connectionString;
            UserDao = new UserSqlDao(connectionString);
        }

        public TransferSqlDao() { }

        public Transfer CreateTransfer(NewTransferDto incoming)
        {
            Transfer output = null;
            string sql = "INSERT INTO transfers (transferType, transferStatus, userFrom, userTo, amount) " +
                "OUTPUT INSERTED.transferId " +
                "VALUES (@type, @status, @from, @to, @amount);";
            int newId = 0;

            string status = incoming.transferType == "Send" ? "Approved" : "Pending";

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@type", incoming.transferType);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@from", incoming.userFrom);
                    cmd.Parameters.AddWithValue("@to", incoming.userTo);
                    cmd.Parameters.AddWithValue("@amount", incoming.amount);

                    newId = Convert.ToInt32(cmd.ExecuteScalar());

                    output = GetTransferByTransferId(newId);
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
            string sql = "UPDATE transfers SET transferStatus = 'Rejected' " +
                "OUTPUT INSERTED.transferId " +
                "WHERE transferId = @id;";
            int idActual = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    idActual = Convert.ToInt32(cmd.ExecuteScalar());

                    output = GetTransferByTransferId(idActual);
                }
            }
            catch (SqlException ex)
            {

                throw new DaoException("Sql exception ocurred", ex);
            }
            return output;
        }

        public List<Transfer> GetAllTransfersByStatus(string transferStatus)
        {
            List<Transfer> output = new List<Transfer>();
            string sql = "SELECT transferId, transferType, transferStatus, userFrom, userTo, amount FROM transfers WHERE transferStatus = @transferStatus;";

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@transferStatus", transferStatus);

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

        public List<Transfer> GetAllTransfersByType(string transferType)
        {
            List<Transfer> output = new List<Transfer>();
            string sql = "SELECT transferId, transferType, transferStatus, userFrom, userTo, amount FROM transfers WHERE transferType = @transferType;";

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@transferType", transferType);

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

        public List<Transfer> GetTransferByUserFromId(int userId)
        {
            List<Transfer> output = new List<Transfer>();

            string sql = "SELECT transferId, transferType, transferStatus, userFrom, userTo, amount FROM transfers WHERE userFrom = @userId;";

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
                throw new DaoException("Sql exception occurred", ex);
            }

            return output;
        }
        
        public List<Transfer> GetTransferByUserToId(int userId)
        {
            List<Transfer> output = new List<Transfer>();

            string sql = "SELECT transferId, transferType, transferStatus, userFrom, userTo, amount FROM transfers WHERE userTo = @userId;";

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
                throw new DaoException("Sql exception occurred", ex);
            }

            return output;
        }

        public Transfer GetTransferByTransferId (int transferId)
        {
            Transfer output = null;

            string sql = "SELECT transferId, fromId.user_id, fromId.firstname, fromId.lastname, fromId.username, fromId.password_hash, fromId.salt, " +
                "toID.user_id, toID.firstname, toID.lastname, toID.username, toID.password_hash, toID.salt, transferType, transferStatus, amount " +
                "FROM transfers " +
                "JOIN users AS fromId ON transfers.userFrom = fromId.user_id " +
                "JOIN users AS toID ON transfers.userTo = toID.user_id " +
                "WHERE transferId = @transferId;";

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@transferId", transferId);

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



        public List<Transfer> GetTransfersByUserName(string userName)
        {
            //IUserDao UserDao = new UserSqlDao(ConnectionString);
            List<Transfer> output = new List<Transfer>();

            try
            {
                User user = UserDao.GetUserByUsername(userName);

                List<Transfer> from = GetTransferByUserFromId(user.UserId);
                List<Transfer> to = GetTransferByUserToId(user.UserId);

                output.AddRange(from);
                output.AddRange(to);
            }
            catch (SqlException ex)
            {

                throw new DaoException("Sql exception ocurred", ex);
            }

            return output;
        }

        public Transfer MapRowToTransfer(SqlDataReader reader)
        {
            Transfer output = new Transfer();

            output.TransferId = Convert.ToInt32(reader["transferId"]);
            output.UserFrom = UserDao.MapRowToUser(reader);
            output.UserTo = UserDao.MapRowToUser(reader);
            output.TransferType = Convert.ToString(reader["transferType"]);
            output.TransferStatus = Convert.ToString(reader["transferStatus"]);
            output.Amount = Convert.ToDecimal(reader["amount"]);

            return output;
        }
    }
}
