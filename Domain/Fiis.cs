using invest_analyst.Utils;

namespace invest_analyst.Domain
{
    public class Fiis
    {
        public Fiis(Dictionary<string, string> fiis)
        {
            Ticker = fiis.FirstOrDefault(e => e.Key == "TICKER").Value;
            Preco = Currency.CleanPriceValue(fiis.FirstOrDefault(e => e.Key == "PRECO").Value);
            UltimoDividendo = Currency.CleanPriceValue(fiis.FirstOrDefault(e => e.Key == "ULTIMO DIVIDENDO").Value);
            Dy = Currency.CleanPriceValue(fiis.FirstOrDefault(e => e.Key == "DY").Value);
            ValorPatrimonialCota = Currency.CleanPriceValue(fiis.FirstOrDefault(e => e.Key == "VALOR PATRIMONIAL COTA").Value);
            PVP = Currency.CleanPriceValue(fiis.FirstOrDefault(e => e.Key == "P/VP").Value);
            LiquidezMediaDiaria = Currency.CleanPriceValue(fiis.FirstOrDefault(e => e.Key == "LIQUIDEZ MEDIA DIARIA").Value);
            PercentualEmCaixa = Currency.CleanPriceValue(fiis.FirstOrDefault(e => e.Key == "PERCENTUAL EM CAIXA").Value);
            CARGDividendos3Anos = Currency.CleanPriceValue(fiis.FirstOrDefault(e => e.Key == "CAGR DIVIDENDOS 3 ANOS").Value);
            CARGValorCora3Anos = Currency.CleanPriceValue(fiis.FirstOrDefault(e => e.Key == " CAGR VALOR CORA 3 ANOS").Value);
            Patrimonio = Currency.CleanPriceValue(fiis.FirstOrDefault(e => e.Key == "PATRIMONIO").Value);
            NumCotistas = Currency.CleanPriceValue(fiis.FirstOrDefault(e => e.Key == "N COTISTAS").Value);
            Gestao = fiis.FirstOrDefault(e => e.Key == "GESTAO").Value?.Trim();
            NumCotas = Currency.CleanPriceValue(fiis.FirstOrDefault(e => e.Key == " N COTAS\r").Value);
        }

        public string Ticker { get; set; }
        public double Preco { get; set; }
        public double UltimoDividendo { get; set; }
        public double Dy { get; set; }
        public double ValorPatrimonialCota { get; set; }
        public double PVP { get; set; }
        public double LiquidezMediaDiaria { get; set; }
        public double PercentualEmCaixa { get; set; }
        public double CARGDividendos3Anos { get; set; }
        public double CARGValorCora3Anos { get; set; }
        public double Patrimonio { get; set; }
        public double NumCotistas { get; set; }
        public string Gestao { get; set; }
        public double NumCotas { get; set; }
    }
}