using System.Text.Json.Serialization;

namespace invest_analyst.Infra.ClientsHttp.Models
{
    public class PricesDTO
    {
        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("date"),]
        public string Date { get; set; }
    }
}