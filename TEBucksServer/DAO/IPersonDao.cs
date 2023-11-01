using System.Data.SqlClient;
using TEBucksServer.Models;

namespace TEBucksServer.DAO
{
    public interface IPersonDao
    {

        Person GetPersonById(int id);
        Person CreatePerson(string firstName, string lastName, int userId);
        Person MapRowToPerson(SqlDataReader reader);
    }
}
