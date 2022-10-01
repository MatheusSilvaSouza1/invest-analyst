using System.Globalization;

namespace invest_analyst.Utils
{
    public static class Currency
    {
        public static decimal Round(decimal value) 
            => Math.Round(value, 2);
        public static string CleanPriceValue(string value)
            => value.Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "");
    }
}