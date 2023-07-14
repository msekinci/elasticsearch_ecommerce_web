using System.Collections.Immutable;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Elasticsearch.ECommerce.Web.Models;

namespace Elasticsearch.ECommerce.Web.Extensions;

public static class ElasticsearchExtensions
{
    public static void AddElastic(this IServiceCollection services, IConfiguration configuration)
    {
        var url = configuration.GetSection("Elasticsearch")["Url"]!;
        var username = configuration.GetSection("Elasticsearch")["Username"]!;
        var password = configuration.GetSection("Elasticsearch")["Password"]!;

        var settings = new ElasticsearchClientSettings(new Uri(url))
            .Authentication(new BasicAuthentication(username!, password!));
        var client = new ElasticsearchClient(settings);
        services.AddSingleton(client);
    }

    public static ImmutableList<T> ConvertImmutableListWithId<T>(this SearchResponse<T> result) where T : IndexBase
    {
        foreach (var item in result.Hits)
        {
            item.Source!.Id = item.Id;
        }

        return result.Documents.ToImmutableList();
    }
}