using System.Text.Json;
using invest_analyst.Infra.ClientsHttp.Models;

namespace invest_analyst.Infra.ClientsHttp
{
    public class DyApi
    {
        private readonly HttpClient client;
        public DyApi()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("authority", "statusinvest.com.br");
            client.DefaultRequestHeaders.Add("accept", "application/json, text/javascript, */*; q=0.01");
            client.DefaultRequestHeaders.Add("accept-language", "pt-BR,pt;q=0.9,en-US;q=0.8,en;q=0.7");
            client.DefaultRequestHeaders.Add("cookie", "_gcl_au=1.1.1974132860.1659451935; _adasys=bab471b5-1a6c-4773-a8ba-043e4083687c; _ga=GA1.3.301246768.1659451936; hubspotutk=80493a7d2f06c4de3202f4820abd9503; __hssrc=1; G_ENABLED_IDPS=google; .StatusInvest=eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJBY2NvdW50SWQiOiI1NzY4MjAiLCJOYW1lIjoiTUFUSEVVUyBTSUxWQSBERSBTT1VaQSIsIkVtYWlsIjoiZXNjb3JwaW9uLmI4QGdtYWlsLmNvbSIsIkludGVyZmFjZVR5cGUiOiJXZWIiLCJJcCI6Ijo6ZmZmZjoxMC4xMDAuMTAuNDQiLCJuYmYiOjE2NTk0NTE5NTUsImV4cCI6MTY1OTUzODM1NSwiaWF0IjoxNjU5NDUxOTU1LCJpc3MiOiJTdGF0dXNJbnZlc3QiLCJhdWQiOiJTdGF0dXNJbnZlc3QifQ.kZ1vXy0B3SHGsXTGQLhLEG6ig9rFoyNnc5RbDfDmeTZGDyqA6sv-PWJH_H_QhZ7c2GAxMBk_Dk8KTQ4Zi72Y5A; _gid=GA1.3.1204370138.1662680763; __cf_bm=xMOmAwAPhbde710bPHBBHEfgfkUki.QxVBhFTn5zy08-1662766440-0-Ae8HADqIyNG5B/bhX05ex+6yCdHglWRZR+gxQJB98FKDTN/uOOFQKicLp17iIy/Za2vNauKJeMbfq5wp93KPYl/NIuyyFhJoAfo/92hZT41zjHHx+OJUOyITs1BlBaTsoA==; __hstc=176625274.80493a7d2f06c4de3202f4820abd9503.1659451951328.1662763555130.1662766455922.58; __hstc=176625274.80493a7d2f06c4de3202f4820abd9503.1659451951328.1662763555130.1662766455922.58; __hssc=176625274.2.1662766455922; _gat_UA-142136095-1=1; _adasys=bab471b5-1a6c-4773-a8ba-043e4083687c");
            client.DefaultRequestHeaders.Add("sec-ch-ua", "\"Google Chrome\";v=\"105\", \"Not)A;Brand\";v=\"8\", \"Chromium\";v=\"105\"");
            client.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
            client.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
            client.DefaultRequestHeaders.Add("sec-fetch-dest", "empty");
            client.DefaultRequestHeaders.Add("sec-fetch-mode", "cors");
            client.DefaultRequestHeaders.Add("sec-fetch-site", "same-origin");
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/105.0.0.0 Safari/537.36");
            client.DefaultRequestHeaders.Add("x-requested-with", "XMLHttpRequest");
        }

        public async Task<List<AssetEarningsYearlyModelDTO>> GetDY(string ticket, string ticketName)
        {
            var url = $"https://statusinvest.com.br/acao/companytickerprovents?companyName={ticketName}&ticker={ticket}&chartProventsType=2";
            var reqParams = new HttpRequestMessage(HttpMethod.Get, url);
            var res = await client.SendAsync(reqParams);
            var json = await res.Content.ReadAsStringAsync();
            var ticketDy = JsonSerializer.Deserialize<CompanytickerproventsDTO>(json);
            return ticketDy.AssetEarningsYearlyModels.OrderByDescending(e => e.Rank).ToList();
        }
    }
}