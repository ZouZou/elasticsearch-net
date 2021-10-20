using System.Collections.Generic;
using System.Threading.Tasks;
using ElasticSearch.Application.Models;

namespace ElasticSearch.Application.Services
{
    public interface IQuoteService
    {
        Task<IEnumerable<Quote>> GetQuotes(string query, int count, int skip = 0);
        Task<Quote> GetQuoteById(string id);
        Task SaveSingleAsync(Quote quote);
    }
}