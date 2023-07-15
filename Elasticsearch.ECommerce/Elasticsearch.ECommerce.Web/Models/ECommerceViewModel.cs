using System.ComponentModel.DataAnnotations;

namespace Elasticsearch.ECommerce.Web.Models;

public class ECommerceViewModel
{
    public string Id { get; set; }
    [Display(Name ="customer_first_name")] public string CustomerFirstName { get; set; } = null!;

    [Display(Name ="customer_last_name")] public string CustomerLastName { get; set; } = null!;

    [Display(Name ="customer_full_name")] public string CustomerFullName { get; set; } = null!;
    [Display(Name ="customer_gender")] public string Gender { get; set; } = null!;
    [Display(Name ="category")] public string Category { get; set; } = null!;

    [Display(Name ="order_id")] public int OrderId { get; set; }

    [Display(Name ="order_date")] public string OrderDate { get; set; }

    [Display(Name ="taxful_total_price")] public double TaxfulTotalPrice { get; set; }
}