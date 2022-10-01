using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using invest_analyst.Domain;

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
    }
}