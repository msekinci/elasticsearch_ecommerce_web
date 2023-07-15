using System.Collections.Immutable;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elasticsearch.ECommerce.Web.Extensions;
using Elasticsearch.ECommerce.Web.Models;

namespace Elasticsearch.ECommerce.Web.Repositories;

public class ECommerceRepository
{
    private readonly ElasticsearchClient _client;
    private const string indexName = "kibana_sample_data_ecommerce";

    public ECommerceRepository(ElasticsearchClient client)
    {
        _client = client;
    }

    public async Task<(ImmutableList<Models.ECommerce>, int count)> SearchAsync(ECommerceSearchViewModel searchViewModel, int page, int pageSize)
    {
        List<Action<QueryDescriptor<Models.ECommerce>>> listQueries = new();

        if (searchViewModel is null)
        {
            listQueries.Add(q => q.MatchAll());

            return await RetrieveData(listQueries, page, pageSize);
        }

        if (!string.IsNullOrWhiteSpace(searchViewModel.Category))
        {
            listQueries.Add((q) => q.Match(m => m
                .Field(f => f.Category)
                .Query(searchViewModel.Category)));
        }
        
        if (!string.IsNullOrWhiteSpace(searchViewModel.CustomerFullName))
        {
            listQueries.Add((q) => q.Match(m => m
                .Field(f => f.CustomerFullName)
                .Query(searchViewModel.CustomerFullName)));
        }
        
        if (!string.IsNullOrWhiteSpace(searchViewModel.Gender))
        {
            listQueries.Add((q) => q.Term(m => m
                .Field(f => f.Gender)
                .Value(searchViewModel.Gender)
                .CaseInsensitive()));
        }
        
        if (searchViewModel.OrderDateStart.HasValue)
        {
            listQueries.Add((q) => q.Range(r => r
                .DateRange(dr => dr
                    .Field(f => f.OrderDate)
                    .Gte(searchViewModel.OrderDateStart))));
        }
        
        if (searchViewModel.OrderDateEnd.HasValue)
        {
            listQueries.Add((q) => q.Range(r => r
                .DateRange(dr => dr
                    .Field(f => f.OrderDate)
                    .Lte(searchViewModel.OrderDateEnd))));
        }


        if (!listQueries.Any())
        {
            listQueries.Add(q => q.MatchAll());
        }

        return await RetrieveData(listQueries, page, pageSize);

    }

    private async Task<(ImmutableList<Models.ECommerce>, int count)> RetrieveData(List<Action<QueryDescriptor<Models.ECommerce>>> listQueries,int page, int pageSize)
    {
        var pageFrom = (page - 1) * pageSize;

        var result = _client.Search<Models.ECommerce>(s => s
            .Index(indexName)
            .Size(pageSize)
            .From(pageFrom)
            .Query(q => q
                .Bool(b => b
                    .Must(listQueries.ToArray()))));

        return ((ImmutableList<Models.ECommerce>, int count))(result.ConvertImmutableListWithId(), result.Total);
    }
}