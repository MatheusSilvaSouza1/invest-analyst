using System.Text;
using invest_analyst.Domain;

namespace invest_analyst.Services
{
    public class Csv : ICsv
    {
        public List<Idiv> Read(string path)
        {
            var acoes = new List<Idiv>();
            using (var reader = new StreamReader(path!, Encoding.UTF8))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line?.Split(';');
                    if (values?.Count() == 6)
                    {
                        acoes.Add(new Idiv()
                        {
                            Codigo = values[0],
                            Nome = values[1],
                            Tipo = values[2],
                            QuantidadeTeorica = decimal.Parse(values[3]),
                            Part = decimal.Parse(values[4]),
                        });
                    }
                }
            }

            return acoes;
        }
    }
}