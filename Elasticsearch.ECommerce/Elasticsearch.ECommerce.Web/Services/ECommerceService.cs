using Elasticsearch.ECommerce.Web.Models;
using Elasticsearch.ECommerce.Web.Repositories;

namespace Elasticsearch.ECommerce.Web.Services;

public class ECommerceService
{
    private readonly ECommerceRepository _commerceRepository;

    public ECommerceService(ECommerceRepository commerceRepository)
    {
        _commerceRepository = commerceRepository;
    }

    public async Task<(List<ECommerceViewModel> data, int totalCount, int pageLinkCount)> SearchAsync(ECommerceSearchViewModel searchViewModel, int page, int pageSize)
    {
        var (list, totalCount) = await _commerceRepository.SearchAsync(searchViewModel, page, pageSize);
        var pageLinkCount = totalCount % pageSize;

        pageLinkCount = pageLinkCount == 0 ? (totalCount / pageSize) : (totalCount / pageSize) + 1;

        var eCommerceList = list.Select(x => new ECommerceViewModel()
        {
            Category = string.Join(", ", x.Category),
            CustomerFullName = x.CustomerFullName,
            Gender = x.Gender,
            CustomerFirstName = x.CustomerFirstName,
            CustomerLastName = x.CustomerLastName,
            TaxfulTotalPrice = x.TaxfulTotalPrice,
            OrderDate = x.OrderDate.ToString("dd.MM.yyyy"),
            OrderId = x.OrderId,
            Id = x.Id
        }).ToList();

        return (eCommerceList, totalCount, pageLinkCount);
    }
}