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

            // // var settings = new ConnectionSettings(new Uri(url))
            // //     .DefaultIndex(defaultIndex)
            // //     .EnableDebugMode();

            AddDefaultMappings(settings);

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);

            CreateIndex(client, defaultIndex);
        }

        private static void AddDefaultMappings(ConnectionSettings settings)
        {
            // settings
            //     .DefaultMappingFor<Product>(m => m
            //         .Ignore(p => p.Price)
            //         .Ignore(p => p.Quantity)
            //         .Ignore(p => p.Rating)
            //     );
            settings
                .DefaultMappingFor<Person>(m => m.IndexName("persons"));
            // settings
            //     .DefaultMappingFor<Tweet>(m => m);
        }

private static void test()
{
     var nodes = new Uri[]
            {
                new Uri("http://192.168.12.45:9400")
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
}
        private static void CreateIndex(IElasticClient client, string indexName)
        {  
            // var createIndexResponse = client.Indices.Create(indexName,
            //     index => index.Map<Product>(x => x.AutoMap())
            // );

            var createIndexResponse = client.Indices.Create(indexName,
                index => index.Map<Person>(x => x.AutoMap())
            );
            // var createTweetIndexResponse = client.Indices.Create(indexName,
            //     index => index.Map<Tweet>(x => x.AutoMap())
            // );
        }
    }
}