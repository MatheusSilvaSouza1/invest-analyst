using System.Text.Json.Serialization;

namespace invest_analyst.Infra.ClientsHttp.Models
{
    public partial class AssetEarningsYearlyModelDTO
    {
        [JsonPropertyName("rank")]
        public long Rank { get; set; }

        [JsonPropertyName("value")]
        public decimal Value { get; set; }
    }
}