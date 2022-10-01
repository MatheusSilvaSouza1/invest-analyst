using System.Text.Json.Serialization;

namespace invest_analyst.Infra.ClientsHttp.Models
{
    public class TicketPricesDTO
    {
        [JsonPropertyName("currencyType")]
        public int CurrencyType { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("prices")]
        public List<PricesDTO> Prices { get; set; }
    }
}