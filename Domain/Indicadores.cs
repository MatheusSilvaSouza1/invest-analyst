using invest_analyst.Utils;

namespace invest_analyst.Domain
{
    public class Indicadores
    {
        public Indicadores() { }
        public Indicadores(string tipo, string valor)
        {
            Tipo = tipo;
            Valor = valor;
        }

        public string Tipo { get; private set; }
        public string Valor { get; private set; }

        public double ValorLimpo
        {
            get => Currency.CleanPriceValue(Valor);
        }
    }
}