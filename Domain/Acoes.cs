using invest_analyst.Utils;

namespace invest_analyst.Domain
{
    public class Acoes
    {
        public Acoes() { }

        public Acoes(string ticket, string dySomaTotal, string valor, List<Indicadores> indicadores)
        {
            Ticket = ticket;
            DySomaTotal = Currency.CleanPriceValue(dySomaTotal);
            Valor = Currency.CleanPriceValue(valor);
            Indicadores = indicadores;
            Calcule();
            DataAnalise = DateTime.UtcNow.AddHours(-3);
        }


        public string Ticket { get; private set; }
        public double Valor { get; private set; }
        public double DySomaTotal { get; private set; }
        public List<Indicadores> Indicadores { get; private set; }
        public double Graham { get; private set; }
        public double Bazin { get; private set; }
        public DateTime DataAnalise { get; private set; }

        public int TotalDividendosPag { get; set; }
        public double Dy5Anos { get; set; }
        public double Dy10Anos { get; set; }
        public double Price { get; set; }
        public double? PL { get; set; }
        public double? PVP { get; set; }
        public double? VPA { get; set; }
        public double? LPA { get; set; }

        public bool CValorJusto { get; set; }
        public bool CBazin { get; set; }
        public bool CTeto { get; set; }

        public void CalculeDy(Prices price, List<Dy> dys)
        {
            TotalDividendosPag = dys.Count;
            Dy5Anos = Currency.Round((dys.Take(5).Sum(e => e.Value) / 5) / 0.06);
            Dy10Anos = Currency.Round((dys.Take(10).Sum(e => e.Value) / 10) / 0.06);
            Price = price.Price;

            CValorJusto = Graham > Valor + (Valor * 0.1) ? true : false;
            CBazin = Bazin > Valor + (Valor * 0.1) ? true : false;
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

            Graham = CalcGraham(VPA, LPA);
            Bazin = CalcBazin();
        }

        public double CalcGraham(double? VPA, double? LPA)
            => Currency.Round((double)Math.Sqrt(Math.Round((double)(22.5 * VPA * LPA), 2)));

        public double CalcBazin()
            => Currency.Round((DySomaTotal * 100) / 6.5);
    }
}