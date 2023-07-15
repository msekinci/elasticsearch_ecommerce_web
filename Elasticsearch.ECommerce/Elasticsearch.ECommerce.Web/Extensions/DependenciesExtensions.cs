using Elasticsearch.ECommerce.Web.Repositories;
using Elasticsearch.ECommerce.Web.Services;

namespace Elasticsearch.ECommerce.Web.Extensions;

public static class DependenciesExtensions
{
    public static void AddDependencies(this IServiceCollection service)
    {
        service.AddScoped<ECommerceRepository>();
        service.AddScoped<ECommerceService>();
    }
}