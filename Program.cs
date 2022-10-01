using invest_analyst.Infra.ClientsHttp;
using invest_analyst.Domain;
using invest_analyst.Services;
using AutoMapper;

namespace invest_analyst;

class Program
{
    private static readonly ICsv _readCsv = new Csv();
    private static readonly PricesApi _pricesApi = new PricesApi();
    private static readonly DyApi _dyApi = new DyApi();
    private static readonly IExcel _excel = new Excel();
    private static readonly ICrawler _crawler = new Crawler();
    private static readonly IMapper _mapper = new Mapper(InitializeAutoMapper.Initialize());

    static async Task Main(string[] args)
    {
        Console.WriteLine("Bem vindo!");
        Console.WriteLine("Digite o numero da opção desejada");
        Console.WriteLine("1 - Para gerar o relatorio com base no idiv");
        Console.WriteLine("2 - Para gerar o relatorio com base nas ações passadas por vc");
        int option = int.Parse(Console.ReadLine());
        if (option == 1)
        {

            Console.WriteLine("Copie o caminho ate o relatorio idiv");
            Console.WriteLine(@"Ex: C:\Users\yourUser\Downloads\IDIVDia_19-09-22.csv");
            string path = Console.ReadLine();
            await GenerateReportByIdiv(path);
        }
    }

    public static async Task GenerateReportByIdiv(string path)
    {
        path = path ?? @"C:\Users\math\Downloads\IDIVDia_19-09-22.csv";
        var idivAcoes = _readCsv.Read(path);

        idivAcoes = idivAcoes.OrderByDescending(e => e.Part).ToList();
        var acoes = new List<Acoes>();

        var count = 1;
        foreach (var idiv in idivAcoes)
        {
            var price = await _pricesApi.GetPrices(idiv.Codigo);
            var dy = await _dyApi.GetDY(idiv.Codigo, idiv.Nome);
            var acao = await _crawler.GetInfosAsync(idiv.Codigo);
            acao.CalculeDy(_mapper.Map<Prices>(price), _mapper.Map<List<Dy>>(dy));
            acoes.Add(acao);
            Thread.Sleep(count % 2 == 0 ? 250 : 500);
            count++;
        }

        _excel.Write(acoes);
    }
}


