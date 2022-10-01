using invest_analyst.Utils;

namespace invest_analyst.Domain
{
    public class Acoes
    {
        public Acoes() { }

        public Acoes(string ticket, string dySomaTotal, string valor, List<Indicadores> indicadores)
        {
            Ticket = ticket;
            DySomaTotal = decimal.Parse(Currency.CleanPriceValue(dySomaTotal));
            Valor = decimal.Parse(Currency.CleanPriceValue(valor));
            Indicadores = indicadores;
            Calcule();
            DataAnalise = DateTime.UtcNow.AddHours(-3);
        }

        public string Ticket { get; private set; }
        public decimal Valor { get; private set; }
        public decimal DySomaTotal { get; private set; }
        public List<Indicadores> Indicadores { get; private set; }
        public decimal Graham { get; private set; }
        public decimal Bazin { get; private set; }
        public DateTime DataAnalise { get; private set; }

        public int TotalDividendosPag { get; set; }
        public decimal Dy5Anos { get; set; }
        public decimal Dy10Anos { get; set; }
        public decimal Price { get; set; }
        public decimal? PL { get; set; }
        public decimal? PVP { get; set; }
        public decimal? VPA { get; set; }
        public decimal? LPA { get; set; }

        public bool CValorJusto { get; set; }
        public bool CBazin { get; set; }
        public bool CTeto { get; set; }

        public void CalculeDy(Prices price, List<Dy> dys)
        {
            TotalDividendosPag = dys.Count;
            Dy5Anos = Currency.Round((dys.Take(5).Sum(e => e.Value) / 5) / 0.06m);
            Dy10Anos = Currency.Round((dys.Take(10).Sum(e => e.Value) / 10) / 0.06m);
            Price = price.Price;

            CValorJusto = Graham > Valor + (Valor * 0.1m) ? true : false;
            CBazin = Bazin > Valor + (Valor * 0.1m) ? true : false;
            CTeto = Dy5Anos > Valor ? true : false;

        }

        private void Calcule()
        {
            PL = Indicadores.FirstOrDefault(i => i.Tipo == "P/L")?.ValorLimpo;
            PVP = Indicadores.FirstOrDefault(i => i.Tipo == "P/VP")?.ValorLimpo;
            VPA = Indicadores.FirstOrDefault(i => i.Tipo == "VPA")?.ValorLimpo;
            LPA = Indicadores.FirstOrDefault(i => i.Tipo == "LPA")?.ValorLimpo;

            if (VPA < 0 || LPA < 0)
            {
                return;
            }

            Graham = Currency.Round((decimal)Math.Sqrt(Math.Round((double)(22.5m * VPA * LPA), 2)));
            Bazin = Currency.Round((DySomaTotal * 100) / 6.5m);
        }
    }
}