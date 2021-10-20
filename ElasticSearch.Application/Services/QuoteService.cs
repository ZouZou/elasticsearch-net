using System.Collections.Generic;
using System.Threading.Tasks;
using ElasticSearch.Application.Models;
using Microsoft.Extensions.Logging;
using Nest;

namespace ElasticSearch.Application.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly IElasticClient _elasticClient;
        private readonly ILogger<QuoteService> _logger;

        public QuoteService(IElasticClient elasticClient, ILogger<QuoteService> logger)
        {
            _elasticClient = elasticClient;
            _logger = logger;
        }

        public async Task<Quote> GetQuoteById(string id)
        {
            var quote = await _elasticClient.GetAsync<Quote>(id);

            return quote.Source;
        }

        public async Task<IEnumerable<Quote>> GetQuotes(string query, int count, int skip = 0)
        {
            var quotes = await _elasticClient.SearchAsync<Quote>(
                s => s
                .Query(q => q.QueryString(d => d.Query('*' + query + '*')))
                .From(skip)
                .Take(count)
            );

            if (!quotes.IsValid)
            {
                // We could handle errors here by checking response.OriginalException 
                //or response.ServerError properties
                _logger.LogError("Failed to search documents");
                return (new Quote[] { });
            }

            return quotes.Documents;
        }

        public async Task SaveSingleAsync(Quote quote)
        {
            await _elasticClient.IndexDocumentAsync<Quote>(quote);
        }
    }
}