using ClosedXML.Excel;
using invest_analyst.Domain;

namespace invest_analyst.Services
{
    public class Excel : IExcel
    {
        public void Write(List<Acao> acoes)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Analises");
                var props = typeof(Acao).GetProperties();
                var names = props.Where(e => e.Name != nameof(Acao.Id)).Select(e => e.Name).ToList();
                for (int i = 0; i < names.Count; i++)
                {
                    worksheet.Cell(1, i + 1).Value = names[i];
                }

                for (int row = 0; row < acoes.Count; row++)
                {
                    for (int column = 0; column < names.Count; column++)
                    {
                        worksheet.Cell(row + 2, column + 1).Value = acoes[row].GetType().GetProperty(names[column]).GetValue(acoes[row]);
                    }
                }
                workbook.SaveAs($".\\{DateTime.UtcNow.AddHours(-3).Date.ToString("yyyy-MM-dd")} Analise.xlsx");
            }
        }
    }
}