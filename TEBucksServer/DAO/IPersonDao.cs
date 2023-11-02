using System.Collections.Generic;
using System.Data.SqlClient;
using TEBucksServer.Models;

namespace TEBucksServer.DAO
{
    public interface IPersonDao
    {
        List<Person> GetAllPeople();
        Person GetPersonById(int id);
        Person CreatePerson(string firstName, string lastName, int userId);
    }
}
