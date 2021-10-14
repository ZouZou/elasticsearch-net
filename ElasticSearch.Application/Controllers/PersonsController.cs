using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Elasticsearch.Net;
using ElasticSearch.Application.Models;
using ElasticSearch.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nest;

namespace ElasticSearch.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IElasticClient _elasticClient;
        private readonly ILogger _logger;
        
        public PersonsController(IPersonService personService, 
                                 IElasticClient elasticClient,
                                 ILogger<PersonsController> logger)
        {
            _personService = personService;
            _elasticClient = elasticClient;
            _logger = logger;
        }

        [HttpGet("find")]
        public async Task<IActionResult> Find(string query, int page = 1, int pageSize = 5)
        {
            var response = await _elasticClient.SearchAsync<Person>(
                 s => s.Query(q => q.QueryString(d => d.Query('*' + query + '*')))
                     .From((page - 1) * pageSize)
                     .Size(pageSize));

            if (!response.IsValid)
            {
                // We could handle errors here by checking response.OriginalException 
                //or response.ServerError properties
                _logger.LogError("Failed to search documents");
                return Ok(new Person[] { });
            }

            return Ok(response.Documents);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson(Person person)
        {
            var existing = await _personService.GetPersonById(person.Id);

            if (existing != null)
            {
                return StatusCode(409, $"Person '{person.FirstName}' already exists.");
            }
            else
            {
                await _personService.SaveSingleAsync(person);
                return Ok();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(int id, Person person)
        {
            var existing = await _personService.GetPersonById(id);

            if (existing != null)
            {
                await _personService.SaveSingleAsync(existing);
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var existing = await _personService.GetPersonById(id);

            if (existing != null)
            {
                await _personService.DeleteAsync(existing);
                return Ok();
            }

            return NotFound();
        }

        [HttpPost]
        [Route("CreateTweetIndex")]
        public ActionResult<string> CreateTweetIndex()
        {
            var nodes = new Uri[]
            {
                new Uri("http://192.168.12.45:9200")
            };

            var pool = new StaticConnectionPool(nodes);
            var settings = new ConnectionSettings(pool);
            var client = new ElasticClient(settings);

            var tweet = new Tweet
            {
                Id = 1,
                User = "kimchy",
                PostDate = new DateTime(2009, 11, 15),
                Message = "Trying out NEST, so far so good?"
            };

            //or specify index via settings.DefaultIndex("mytweetindex");
            Console.WriteLine(client.Index(tweet, idx => idx.Index("mytweetindex")));
            return ""; 
        }

        [HttpPost]
        [Route("CreatePersonIndex")]
        public async Task<string> CreatePersonIndex()
        {
            var nodes = new Uri[]
            {
                new Uri("http://192.168.12.45:9200")
            };

            var pool = new StaticConnectionPool(nodes);
            var settings = new ConnectionSettings(pool);
            var client = new ElasticClient(settings);

            var person = new Person
            {
                Id = 3,
                FirstName = "Martijn",
                LastName = "Laarman"
            };

            Console.WriteLine(await client.IndexAsync(person, idx => idx.Index("mypersonindex")));

            return ""; 
        }

        [HttpPost]
        [Route("CreateClientsIndex")]
        public async Task<string> CreateClientsIndex()
        {
            var nodes = new Uri[]
            {
                new Uri("http://192.168.12.45:9200")
            };

            var pool = new StaticConnectionPool(nodes);
            var settings = new ConnectionSettings(pool);
            var client = new ElasticClient(settings);

            var clients = new clients
            {
                Id = 3,
                Name = "Martijn",
                Address = "Laarman"
            };

            Console.WriteLine(await client.IndexAsync(clients, idx => idx.Index("myclientindex")));

            return ""; 
        }

        [HttpPost]
        [Route("CreateIndex")]
        public async Task<string> CreateIndex()
        {
            var nodes = new Uri[]
            {
                new Uri("http://192.168.12.45:9200")
            };

            var pool = new StaticConnectionPool(nodes);
            var settings = new ConnectionSettings(pool);
            var client = new ElasticClient(settings);

            var clients = new clients
            {
                Id = 3,
                Name = "Joseph",
                Address = "Lebanon"
            };

            Console.WriteLine(await client.IndexAsync(clients, idx => idx.Index("myclientsindex")));

            return ""; 
        }

        [HttpGet]
        [Route("GetPerson")]
        public async Task<Person> GetPersonByIndex(int id)
        {
            var nodes = new Uri[]
            {
                new Uri("http://192.168.12.45:9200")
            };

            var pool = new StaticConnectionPool(nodes);
            var settings = new ConnectionSettings(pool);
            var client = new ElasticClient(settings);
            var response = await client.GetAsync<Person>(id, idx => idx.Index("mypersonindex")); 
            return response.Source;
        }
    }

    public class DynamicRequester
    {
        public object input { get; set; }
        public string indexName { get; set; }
    }
    public class clients 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
    public class SingleClient 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}