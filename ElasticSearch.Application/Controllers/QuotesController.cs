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
    public class QuotesController : ControllerBase
    {
        private readonly IQuoteService _quoteService;
        private readonly IElasticClient _elasticClient;
        private readonly ILogger _logger;
        
        public QuotesController(IQuoteService quoteService, 
                                 IElasticClient elasticClient,
                                 ILogger<QuotesController> logger)
        {
            _quoteService = quoteService;
            _elasticClient = elasticClient;
            _logger = logger;
        }

        [HttpGet("find")]
        public async Task<IActionResult> Find(string query, int page = 1, int pageSize = 5)
        {
            var response = await _quoteService.GetQuotes(query, ((page - 1) * pageSize), pageSize);

            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetQuoteById(string id)
        {
            var existing = await _quoteService.GetQuoteById(id);

            if (existing != null)
            {
                return Ok((Quote)existing);
            }
            else
            {
                 return StatusCode(404, $"Quote with Id '{id}' does not exists.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuote(Quote quote)
        {
            var existing = await _quoteService.GetQuoteById(quote.RequestReferenceNo);

            if (existing != null)
            {
                return StatusCode(409, $"Quote '{quote.RequestReferenceNo}' already exists.");
            }
            else
            {
                await _quoteService.SaveSingleAsync(quote);
                return Ok();
            }
        }
    }
}