using System;
using System.Threading.Tasks;
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
            // var response = await _elasticClient.SearchAsync<Person>(
            //      s => s.Query(q => q.QueryString(d => d.Query('*' + query + '*')))
            //          .From((page - 1) * pageSize)
            //          .Size(pageSize));
            // if (!response.IsValid)
            // {
            //     // We could handle errors here by checking response.OriginalException 
            //     //or response.ServerError properties
            //     _logger.LogError("Failed to search documents");
            //     return Ok(new Person[] { });
            // }
            // return Ok(response.Documents);
            
            var response = await _personService.GetPersons(query, ((page - 1) * pageSize), pageSize);

            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPersonById(int id)
        {
            var existing = await _personService.GetPersonById(id);

            if (existing != null)
            {
                return Ok((Person)existing);
            }
            else
            {
                 return StatusCode(404, $"Person with Id '{id}' does not exists.");
            }
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> GetPersonByName(string name)
        {
            var existing = await _personService.GetPersonByName(name);

            if (existing != null)
            {
                return Ok((Person)existing);
            }
            else
            {
                 return StatusCode(404, $"Person '{name}' does not exists.");
            }
        }

        [HttpGet]
        [Route("{address}")]
        public async Task<IActionResult> GetPersonByAddress(string address)
        {
            var existing = await _personService.GetPersonByAddress(address);

            if (existing != null)
            {
                return Ok((Person)existing);
            }
            else
            {
                 return StatusCode(404, $"Person with address '{address}' does not exists.");
            }
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
        public async Task<IActionResult> DeletePerson(int id)
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
            var tweet = new Tweet
            {
                Id = 1,
                User = "kimchy",
                PostDate = new DateTime(2009, 11, 15),
                Message = "Trying out NEST, so far so good?"
            };

            Console.WriteLine(_elasticClient.Index(tweet, idx => idx.Index("mytweetindex")));
            return ""; 
        }

        [HttpPost]
        [Route("CreatePersonIndex")]
        public async Task<string> CreatePersonIndex()
        {
            var person = new Person
            {
                Id = 3,
                FirstName = "Martijn",
                LastName = "Laarman"
            };

            Console.WriteLine(await _elasticClient.IndexAsync(person, idx => idx.Index("mypersonindex")));

            return ""; 
        }
    }
}