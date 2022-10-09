using invest_analyst.Utils;
using MongoDB.Bson.Serialization.Attributes;

namespace invest_analyst.Domain
{
    public class Acao
    {
        public Acao() { }

        public Acao(Dictionary<string, string> acao)
        {
            Ticket = acao.FirstOrDefault(e => e.Key == "TICKER").Value;
            Preco = Currency.CleanPriceValue(acao.FirstOrDefault(e => e.Key == "PRECO").Value);
            DY = Currency.CleanPriceValue(acao.FirstOrDefault(e => e.Key == "DY").Value);
            PL = Currency.CleanPriceValue(acao.FirstOrDefault(e => e.Key == "P/L").Value);
            PVP = Currency.CleanPriceValue(acao.FirstOrDefault(e => e.Key == "P/VP").Value);
            PAtivo = Currency.CleanPriceValue(acao.FirstOrDefault(e => e.Key == "P/ATIVOS").Value);
            MargemBruta = Currency.CleanPriceValue(acao.FirstOrDefault(e => e.Key == "MARGEM BRUTA").Value);
            MargemEbit = Currency.CleanPriceValue(acao.FirstOrDefault(e => e.Key == "MARGEM EBIT").Value);
            MargemLiq = Currency.CleanPriceValue(acao.FirstOrDefault(e => e.Key == "MARG. LIQUIDA").Value);
            PEbit = Currency.CleanPriceValue(acao.FirstOrDefault(e => e.Key == "P/EBIT").Value);
            EvEbit = Currency.CleanPriceValue(acao.FirstOrDefault(e => e.Key == "EV/EBIT").Value);
            DividaLiquidaEbitda = Currency.CleanPriceValue(acao.FirstOrDefault(e => e.Key == "DIVIDA LIQUIDA / EBIT").Value);
            DivLiqPatrim = Currency.CleanPriceValue(acao.FirstOrDefault(e => e.Key == "DIV. LIQ. / PATRI.").Value);
            PSR = Currency.CleanPriceValue(acao.FirstOrDefault(e => e.Key == "PSR").Value);
            PCapGiro = Currency.CleanPriceValue(acao.FirstOrDefault(e => e.Key == "P/CAP. GIRO").Value);
            PAtivCirLiq = Currency.CleanPriceValue(acao.FirstOrDefault(e => e.Key == "P. AT CIR. LIQ.").Value);
            LiqCorr = Currency.CleanPriceValue(acao.FirstOrDefault(e => e.Key == "LIQ. CORRENTE").Value);
            ROE = Currency.CleanPriceValue(acao.FirstOrDefault(e => e.Key == "ROE").Value);
            ROA = Currency.CleanPriceValue(acao.FirstOrDefault(e => e.Key == "ROA").Value);
            ROIC = Currency.CleanPriceValue(acao.FirstOrDefault(e => e.Key == "ROIC").Value);
            PatrimonioAtivos = Currency.CleanPriceValue(acao.FirstOrDefault(e => e.Key == "PATRIMONIO / ATIVOS").Value);
            PassivosAtivos = Currency.CleanPriceValue(acao.FirstOrDefault(e => e.Key == "PASSIVOS / ATIVOS").Value);
            GiroAtivo = Currency.CleanPriceValue(acao.FirstOrDefault(e => e.Key == "GIRO ATIVOS").Value);
            CAGRReceitas5Anos = Currency.CleanPriceValue(acao.FirstOrDefault(e => e.Key == "CAGR RECEITAS 5 ANOS").Value);
            CAGRLucros5Anos = Currency.CleanPriceValue(acao.FirstOrDefault(e => e.Key == "CAGR LUCROS 5 ANOS").Value);
            LiquidezMediaDiaria = Currency.CleanPriceValue(acao.FirstOrDefault(e => e.Key == " LIQUIDEZ MEDIA DIARIA").Value);
            VPA = Currency.CleanPriceValue(acao.FirstOrDefault(e => e.Key == " VPA").Value);
            LPA = Currency.CleanPriceValue(acao.FirstOrDefault(e => e.Key == " LPA").Value);
            PEGRatio = Currency.CleanPriceValue(acao.FirstOrDefault(e => e.Key == " PEG Ratio").Value);
            ValorDeMercado = Currency.CleanPriceValue(acao.FirstOrDefault(e => e.Key == " VALOR DE MERCADO").Value);
            CalcGraham();
            CalcPoints();
            Id = Guid.NewGuid();
        }

        [BsonId]
        public Guid Id { get; set; }
        public string Ticket { get; set; }
        public double Preco { get; set; }
        public double PrecoTeto { get; set; }
        public double Graham { get; set; }
        public double DY { get; set; }
        public DateTime DtAnalise { get; set; } = DateTime.UtcNow.AddHours(-3);
        public double PL { get; set; }
        public double PVP { get; set; }
        public double PAtivo { get; set; }
        public double MargemBruta { get; set; }
        public double MargemEbit { get; set; }
        public double MargemLiq { get; set; }
        public double PEbit { get; set; }
        public double EvEbit { get; set; }
        public double DividaLiquidaEbitda { get; set; }
        public double DivLiqPatrim { get; set; }
        public double PSR { get; set; }
        public double PCapGiro { get; set; }
        public double PAtivCirLiq { get; set; }
        public double LiqCorr { get; set; }
        public double ROE { get; set; }
        public double ROA { get; set; }
        public double ROIC { get; set; }
        public double PatrimonioAtivos { get; set; }
        public double PassivosAtivos { get; set; }
        public double GiroAtivo { get; set; }
        public double CAGRReceitas5Anos { get; set; }
        public double CAGRLucros5Anos { get; set; }
        public double LiquidezMediaDiaria { get; set; }
        public double VPA { get; set; }
        public double LPA { get; set; }
        public double PEGRatio { get; set; }
        public double ValorDeMercado { get; set; }
        public int TotalDividendosPag { get; set; }
        public double TotalDyLast5Yeas { get; set; }
        public double MediaTotalDividendosPago { get; set; }

        public int TotalPoints { get; set; }

        public void CalcPoints()
        {
            if (DY > 7.5)
                TotalPoints++;

            if (PL <= 10)
                TotalPoints++;

            if (PVP > 0.80 && PVP < 1.20)
                TotalPoints++;

            if (PEbit <= 10)
                TotalPoints++;

            if (EvEbit <= 10)
                TotalPoints++;

            if (MargemLiq >= 10)
                TotalPoints++;

            if (ROIC > 15)
                TotalPoints++;

            if (DivLiqPatrim < 3)
                TotalPoints++;

            if (Graham > (Preco + (Preco * 0.1)))
                TotalPoints++;

            if (CAGRLucros5Anos > 2)
                TotalPoints++;

            if (CAGRReceitas5Anos > 2)
                TotalPoints++;
        }

        public void CalcGraham()
        {
            var g = Math.Round((double)(22.5 * VPA * LPA), 2);
            Graham = Currency.Round((double)Math.Sqrt(g < 0 ? 0 : g));
        }

        public static List<Acao> FilterBests(List<Acao> acoes)
            => acoes.Where(e => e.DY > 7 &&
                                e.TotalPoints > 8 &&
                                e.CAGRReceitas5Anos > 0 &&
                                e.LiquidezMediaDiaria >= 100000.00 &&
                                e.Graham > e.Preco)
                    .ToList();

        public void CalculeDy(List<Dy> dys)
        {
            TotalDividendosPag = dys.Count;
            TotalDyLast5Yeas = Currency.Round(dys.Skip(1).Take(5).Sum(e => e.Value));
            MediaTotalDividendosPago = Currency.Round(dys.Sum(e => e.Value));
            PrecoTeto = Currency.Round((TotalDyLast5Yeas / 5) / 0.06);
        }
    }
}