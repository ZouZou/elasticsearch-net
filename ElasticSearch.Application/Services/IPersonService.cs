using System.Collections.Generic;
using System.Threading.Tasks;
using ElasticSearch.Application.Models;

namespace ElasticSearch.Application.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetPersons(string query, int count, int skip = 0);
        Task<Person> GetPersonById(int id);
        Task<IEnumerable<Person>> GetPersonByName(string name);
        Task<IEnumerable<Person>> GetPersonByAddress(string address);
        Task DeleteAsync(Person person);
        Task SaveSingleAsync(Person person);
        Task SaveManyAsync(Person[] persons);
        Task SaveBulkAsync(Person[] persons);
    }
}
