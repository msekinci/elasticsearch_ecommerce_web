using System.Text.Json.Serialization;

namespace Elasticsearch.ECommerce.Web.Models;

public class IndexBase
{
    [JsonPropertyName("_id")]
    public string Id { get; set; } = null!;
}