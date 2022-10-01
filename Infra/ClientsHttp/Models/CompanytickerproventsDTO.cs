using System.Text.Json.Serialization;

namespace invest_analyst.Infra.ClientsHttp.Models
{
    public partial class CompanytickerproventsDTO
    {
        [JsonPropertyName("assetEarningsYearlyModels")]
        public List<AssetEarningsYearlyModelDTO> AssetEarningsYearlyModels { get; set; }
    }
}