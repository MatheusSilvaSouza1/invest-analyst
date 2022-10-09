using System.Text.RegularExpressions;
using HtmlAgilityPack;
using invest_analyst.Domain;
using invest_analyst.Infra.ClientsHttp;

namespace invest_analyst.Services
{
    public class Crawler : ICrawler
    {
        public readonly HttpClient httpClient;

        public Crawler()
        {
            httpClient = new HttpClient();
        }
        public async Task<Acoes> GetInfosAsync(string ticket)
        {
            var html = await httpClient.GetStringAsync($"https://statusinvest.com.br/acoes/{ticket}");

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var valorAtual = htmlDocument.DocumentNode.Descendants()
                                   .First(node => node.GetAttributeValue("title", "").Equals("Valor atual do ativo"))
                                   .Descendants()
                                   .First(node => node.GetAttributeValue("class", "").Equals("value")).InnerHtml;

            var dy = htmlDocument.DocumentNode.Descendants()
                                   .First(node => node.GetAttributeValue("title", "").Equals("Soma total de proventos distribuídos nos últimos 12 meses"))
                                   .Descendants()
                                   .First(node => node.GetAttributeValue("class", "").Equals("sub-value")).InnerHtml
                                   .Replace("R$ ", "");


            var indicadores = htmlDocument.DocumentNode.Descendants()
                                   .Where(node => node.GetAttributeValue("class", "").Equals("indicator-today-container"))
                                   .SelectMany(div => div.Descendants()
                                                     .Where(node => node.GetAttributeValue("class", "")
                                                                        .Equals("w-50 w-sm-33 w-md-25 w-lg-16_6 mb-2 mt-2 item ")))
                                    .Where(section => section.Descendants("strong").FirstOrDefault()?.InnerText != "-")
                                    .Select(section => new Indicadores(tipo: section.Descendants("h3").FirstOrDefault()?.InnerText,
                                                                       valor: section.Descendants("strong").FirstOrDefault()?.InnerText))
                                    .ToList();

            return new Acoes(ticket, dy, valorAtual, indicadores);
        }

        public async Task<List<Acao>> GetAcoes()
        {
            var lines = await new StatusInvestApi().GetAcoesCSV();
            var acoes = (from value in lines.Skip(1) select value)
                        .Select(e =>
                        {
                            var columns = lines[0].Split(";");
                            var dic = new Dictionary<string, string>();
                            var values = e.Split(";");
                            for (int i = 0; i < values.Count(); i++)
                            {
                                dic.Add(columns[i], values[i]);
                            }
                            return new Acao(dic);
                        })
                        .ToList();
            return acoes;
        }
    }
}