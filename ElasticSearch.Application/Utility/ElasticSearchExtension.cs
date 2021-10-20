using System;
using Elasticsearch.Net;
using ElasticSearch.Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace ElasticSearch.Application.Utility
{
    public static class ElasticSearchExtensions
    {
        public static void AddElasticsearch(
            this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["elasticsearch:url"];
            var defaultIndex = configuration["elasticsearch:index"];

            var nodes = new Uri[]
            {
                new Uri(url)
            };

            var pool = new StaticConnectionPool(nodes);
            var settings = new ConnectionSettings(pool)
                .DefaultIndex(defaultIndex)
                .EnableDebugMode();

            AddDefaultMappings(settings);

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);

            CreateIndex(client, defaultIndex);
            CreateQuoteIndex(client, "quotes");
        }

        private static void AddDefaultMappings(ConnectionSettings settings)
        {
            settings
                .DefaultMappingFor<Person>(m => m
                    .IndexName("persons")
                    .IdProperty(p => p.Id)
                );
            settings
                .DefaultMappingFor<Quote>(m => m
                    .IndexName("quote")
                    .IdProperty(p => p.RequestReferenceNo)
                );
            // settings
            //     .DefaultMappingFor<Tweet>(m => m);
        }

        private static void CreateIndex(IElasticClient client, string indexName)
        {  
            var createIndexResponse = client.Indices.Create(indexName,
                index => index.Map<Person>(x => x.AutoMap())
            );
        }
        private static void CreateQuoteIndex(IElasticClient client, string indexName)
        {  
            var createIndexResponse = client.Indices.Create(indexName,
                index => index.Map<Quote>(x => x.AutoMap())
            );
        }
    }
}