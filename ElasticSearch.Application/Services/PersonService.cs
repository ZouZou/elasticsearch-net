using System.Collections.Generic;
using System.Threading.Tasks;
using ElasticSearch.Application.Models;
using Microsoft.Extensions.Logging;
using Nest;

namespace ElasticSearch.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IElasticClient _elasticClient;
        private readonly ILogger<PersonService> _logger;

        public PersonService(IElasticClient elasticClient, ILogger<PersonService> logger)
        {
            _elasticClient = elasticClient;
            _logger = logger;
        }

        public async Task<IEnumerable<Person>> GetPersons(int count, int skip = 0)
        {
            var persons = await _elasticClient.SearchAsync<Person>(
                s => s
                .From(skip)
                .Take(count)
            );

            return persons.Documents;
        }

        public async Task<Person> GetPersonById(int id)
        {
            var person = await _elasticClient.GetAsync<Person>(id);

            return person.Source;
        }

        public async Task<IEnumerable<Person>> GetPersonByName(string name)
        {
            var persons = await _elasticClient.SearchAsync<Person>(
                s => s
                .From(0)
                .Take(10)
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.FirstName)
                        .Query(name)
                    )
                )
            );

            return persons.Documents;
        }

        public async Task<IEnumerable<Person>> GetPersonByAddress(string address)
        {
            var persons = await _elasticClient.SearchAsync<Person>(
                s => s
                .From(0)
                .Take(10)
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.Address)
                        .Query(address)
                    )
                )
            );

            return persons.Documents;
        }

        public async Task DeleteAsync(Person person)
        {
            await _elasticClient.DeleteAsync<Person>(person);
        }

        public async Task SaveSingleAsync(Person person)
        {
            await _elasticClient.IndexDocumentAsync<Person>(person);
        }

        public async Task SaveManyAsync(Person[] persons)
        {
            var result = await _elasticClient.IndexManyAsync(persons);
            if (result.Errors)
            {
                // the response can be inspected for errors
                foreach (var itemWithError in result.ItemsWithErrors)
                {
                    _logger.LogError("Failed to index document {0}: {1}",
                        itemWithError.Id, itemWithError.Error);
                }
            }
        }

        public async Task SaveBulkAsync(Person[] persons)
        {
            var result = await _elasticClient.BulkAsync(b => b.Index("persons").IndexMany(persons));
            if (result.Errors)
            {
                // the response can be inspected for errors
                foreach (var itemWithError in result.ItemsWithErrors)
                {
                    _logger.LogError("Failed to index document {0}: {1}",
                        itemWithError.Id, itemWithError.Error);
                }
            }
        }
    }
}