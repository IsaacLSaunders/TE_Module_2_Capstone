using System;
using System.Data.SqlClient;
using TEBucksServer.Exceptions;
using TEBucksServer.Models;

namespace TEBucksServer.DAO
{
    public class PersonSqlDao : IPersonDao
    {
        private readonly string connectionString;

        public PersonSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Person GetPersonById(int id)
        {

            Person retrieved = null;

            string sql = "SELECT Id, LoginId, FirstName, LastName FROM Persons WHERE Id = @id;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        retrieved = MapRowToPerson(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("Sql exception occurred", ex);
            }

            return retrieved;

        }

        public Person CreatePerson(string firstName, string lastName, int userId)
        {
            Person newPerson = null;

            string sql = "INSERT INTO Persons(LoginId, FirstName, LastName) OUTPUT INSERTED.Id VALUES(@userId, @firstName, @lastName);";
            int personId = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@lastName", lastName);

                    personId = Convert.ToInt32(cmd.ExecuteScalar());

                    newPerson = GetPersonById(personId);
                    
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("Sql exception occurred", ex);
            }

            return newPerson;
        }

        public Person MapRowToPerson (SqlDataReader reader)
        {
            Person output = new Person();

            output.Id = Convert.ToInt32(reader["id"]);
            output.LoginId = Convert.ToInt32(reader["loginid"]);
            output.FirstName = Convert.ToString(reader["firstname"]);
            output.LastName = Convert.ToString(reader["lastname"]);

            return output;
        }
    }
}
