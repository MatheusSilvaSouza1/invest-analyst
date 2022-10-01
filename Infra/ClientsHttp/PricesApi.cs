using System.Text.Json;
using invest_analyst.Infra.ClientsHttp.Models;

namespace invest_analyst.Infra.ClientsHttp
{
    public class PricesApi
    {
        private readonly HttpClient client;
        public PricesApi()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("authority", "statusinvest.com.br");
            client.DefaultRequestHeaders.Add("accept", "*/*");
            client.DefaultRequestHeaders.Add("accept-language", "pt-BR,pt;q=0.9,en-US;q=0.8,en;q=0.7");
            client.DefaultRequestHeaders.TryAddWithoutValidation("content-type", "application/x-www-form-urlencoded; charset=UTF-8");
            client.DefaultRequestHeaders.Add("cookie", "_gcl_au=1.1.1974132860.1659451935; _adasys=bab471b5-1a6c-4773-a8ba-043e4083687c; _ga=GA1.3.301246768.1659451936; hubspotutk=80493a7d2f06c4de3202f4820abd9503; __hssrc=1; G_ENABLED_IDPS=google; _fbp=fb.2.1662850930168.1671521439; pg_beacon=1; pg_mm2_cookie_a=8db858f0-0230-4ec2-84d1-04dd7ef5aaa6; pg_ua=Mozilla/5.0 (Windows NT 10.0 Win64 x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/105.0.0.0 Safari/537.36; pg_geo={\"country\":\"BR\",\"region\":\"PE\",\"ip\":\"179.251.187.62\"}; pg_custom_timeout=; pg_ip=179.251.187.62; pg_bot_percent=0; pg_bot_reason=lnb; pg_analytics=disabled; pg_height=1080; pg_width=1920; pg_aheight=1032; pg_awidth=1920; _gid=GA1.3.263836511.1663452142; pg_tc=sample; pg_pl=12; pg_quick_check=true; pg_preconnecting=unset; pg_bot_model=1; __gads=ID=cbe5549d1571e1ff:T=1662918022:S=ALNI_MaSREXw2Jo_kcpBw2Tex0xtW-Knfg; pg_keypress=0; pg_touch_start=0; pg_touch_move=0; __cf_bm=H1nsoFFt1yY6HtUQ8dGb5ZidNDT0o.1sDQgd7FF7950-1663535111-0-AUR2xX3tsk1SzfFkGhfcrThaBUojgi/9vRiRV2OLhc6NK5htnG8G9d5D4ZcyVs6OQcYbKNGilmKL5+XNRb2fqZDjsCNGpW/VWK/UU8ITC3M8ySEMfGOgtV3z2149TodHKg==; __hstc=176625274.80493a7d2f06c4de3202f4820abd9503.1659451951328.1663509513435.1663535135734.69; __hstc=176625274.80493a7d2f06c4de3202f4820abd9503.1659451951328.1663509513435.1663535135734.69; pg_session_id=26004469-bf99-4aff-ac79-442d11d774a7; pg_canonical_session=statusinvest.com.br/acoes/sapr4; __gpi=UID=000005758269fc31:T=1662918022:RT=1663535143:S=ALNI_MYu2s-dgzriSYmZ-5OlnhUO0qiArg; qcSxc=1663535471564; __qca=P0-276460964-1663535471558; pg_tc_cc=1; pg_pv_time_3=338476; pg_scroll=722; __hssc=176625274.4.1663535135734; pg_session_depth=4; pg_latency_before_tc=231; pg_after_init_response_time=702; pg_tc_response_time=645; FCNEC=%5B%5B%22AKsRol8gbaLlWYAMD19BE2i-nLkl1a_cPqqQrE8BOEjvIaRq9qveMOmgFBfE9ntVxXX5Jyau9yfFsoHXvDQjlkzaRBLmdDEtpPPu3d03I0AJUOv_AWlis5-qP-07R521QywggXNEMCy0UmMZTdtRj1tN0UfERH5ayQ%3D%3D%22%5D%2Cnull%2C%5B%5D%5D; pg_last_unload=341974; pg_pv_time_4=341974; pg_mouse_move=4249; pg_click=11; _gat_UA-142136095-1=1; _adasys=bab471b5-1a6c-4773-a8ba-043e4083687c");
            client.DefaultRequestHeaders.Add("origin", "https://statusinvest.com.br");
            client.DefaultRequestHeaders.Add("referer", "https://statusinvest.com.br/acoes/sapr4");
            client.DefaultRequestHeaders.Add("sec-ch-ua", "\"Google Chrome\";v=\"105\", \"Not)A;Brand\";v=\"8\", \"Chromium\";v=\"105\"");
            client.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
            client.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
            client.DefaultRequestHeaders.Add("sec-fetch-dest", "empty");
            client.DefaultRequestHeaders.Add("sec-fetch-mode", "cors");
            client.DefaultRequestHeaders.Add("sec-fetch-site", "same-origin");
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/105.0.0.0 Safari/537.36");
            client.DefaultRequestHeaders.Add("x-requested-with", "XMLHttpRequest");
        }

        public async Task<PricesDTO?> GetPrices(string ticket)
        {
            var url = "https://statusinvest.com.br/acao/tickerprice";

            var dict = new Dictionary<string, string>();
            dict.Add("ticker", ticket);
            dict.Add("type", "0");
            dict.Add("currences[]", "1");
            var reqParams = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(dict) };
            var res = await client.SendAsync(reqParams);
            var json = await res.Content.ReadAsStringAsync();
            var ticketPrices = JsonSerializer.Deserialize<List<TicketPricesDTO>>(json);
            return ticketPrices.SelectMany(e => e.Prices.OrderByDescending(price => price.Date)).FirstOrDefault();
        }
    }
}
