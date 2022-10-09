using ClosedXML.Excel;
using invest_analyst.Domain;

namespace invest_analyst.Services
{
    public class Excel : IExcel
    {
        public void Write(List<Acoes> acoes)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Analises");
                worksheet.Cell("A1").Value = "Ticket";
                worksheet.Cell("B1").Value = "Dt. analise";
                worksheet.Cell("C1").Value = "Cotação";
                worksheet.Cell("D1").Value = "DY (12m)";
                worksheet.Cell("E1").Value = "VPA";
                worksheet.Cell("F1").Value = "LPA";
                worksheet.Cell("G1").Value = "V. justo Graham";
                worksheet.Cell("H1").Value = "V. justo Bazin";
                worksheet.Cell("I1").Value = "V. teto (6% a.a)";
                worksheet.Cell("J1").Value = "C. justo?";
                worksheet.Cell("K1").Value = "C. justo Bazin?";
                worksheet.Cell("L1").Value = "C. teto?";

                var row = 2;
                foreach (var acao in acoes)
                {
                    worksheet.Cell(row, 1).Value = acao.Ticket;
                    worksheet.Cell(row, 2).Value = acao.DataAnalise.ToString("yyyy-MM-dd");
                    worksheet.Cell(row, 3).Value = acao.Valor;
                    worksheet.Cell(row, 4).Value = acao.DySomaTotal;
                    worksheet.Cell(row, 5).Value = acao.VPA;
                    worksheet.Cell(row, 6).Value = acao.LPA;
                    worksheet.Cell(row, 7).Value = acao.Graham;
                    worksheet.Cell(row, 8).Value = acao.Bazin;
                    worksheet.Cell(row, 9).Value = acao.Dy5Anos;
                    worksheet.Cell(row, 10).Value = acao.CValorJusto;
                    worksheet.Cell(row, 10).Style.Fill.BackgroundColor = acao.CValorJusto ? XLColor.Green : XLColor.Red;
                    worksheet.Cell(row, 11).Value = acao.CBazin;
                    worksheet.Cell(row, 11).Style.Fill.BackgroundColor = acao.CBazin ? XLColor.Green : XLColor.Red;
                    worksheet.Cell(row, 12).Value = acao.CTeto;
                    worksheet.Cell(row, 12).Style.Fill.BackgroundColor = acao.CTeto ? XLColor.Green : XLColor.Red;

                    row++;
                }

                workbook.SaveAs($".\\{DateTime.UtcNow.AddHours(-3).Date.ToString("yyyy-MM-dd")} Analise.xlsx");
            }
        }
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