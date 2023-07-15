using Elasticsearch.ECommerce.Web.Models;
using Elasticsearch.ECommerce.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Elasticsearch.ECommerce.Web.Controllers;

public class ECommerceController : Controller
{
    private readonly ECommerceService _eCommerceService;

    public ECommerceController(ECommerceService eCommerceService)
    {
        _eCommerceService = eCommerceService;
    }

    // GET
    public async Task<IActionResult> Search([FromQuery] SearchPageViewModel searchPageViewModel)
    {
        var result = await _eCommerceService.SearchAsync(
            searchPageViewModel.SearchViewModel, 
            searchPageViewModel.Page,
            searchPageViewModel.PageSize);

        searchPageViewModel.List = result.data;
        searchPageViewModel.PageLinkCount = result.pageLinkCount;
        searchPageViewModel.TotalCount = result.totalCount;
        
        return View(searchPageViewModel);
    }
}