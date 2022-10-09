using System.Globalization;

namespace invest_analyst.Utils
{
    public static class Currency
    {
        public static CultureInfo ptBR = new CultureInfo("pt-BR");
        public static NumberStyles style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
        public static double Round(double value)
            => Math.Round(value, 2);
        public static double CleanPriceValue(string value)
        {
            var valueClean = value?.Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "");
            double v;
            if (double.TryParse(valueClean, style, ptBR, out v))
            {
                return v;
            }
            return 0;
        }
    }
}