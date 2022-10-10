using invest_analyst.Infra.ClientsHttp;
using invest_analyst.Domain;
using invest_analyst.Services;
using AutoMapper;
using invest_analyst.Infra.Database;

namespace invest_analyst;

class Program
{
    private static readonly ICsv _readCsv = new Csv();
    private static readonly StatusInvestApi _pricesApi = new StatusInvestApi();
    private static readonly IExcel _excel = new Excel();
    private static readonly ICrawler _crawler = new Crawler();
    private static readonly IMapper _mapper = new Mapper(InitializeAutoMapper.Initialize());
    private static readonly IContext _context = new ContextDb();

    static async Task Main(string[] args)
    {
        Console.WriteLine("Bem vindo!");
        Console.WriteLine("Digite o numero da opção desejada");
        Console.WriteLine("1 - Para gerar o relatorio com base no idiv");
        Console.WriteLine("2 - Para gerar o relatorio com base em analise fundamentalista");
        int option = int.Parse(Console.ReadLine());
        if (option == 1)
        {
            Console.WriteLine("Copie o caminho ate o relatorio idiv");
            Console.WriteLine(@"Ex: C:\Users\yourUser\Downloads\IDIVDia_19-09-22.csv");
            string path = Console.ReadLine();
            await GenerateReportByIdiv(path);
        }
        else if (option == 2)
        {
            await GenerateReportByBestsAcoes();
            Console.WriteLine(@"Relatorio gerado!");
        }
    }

    public static async Task GenerateReportByBestsAcoes()
    {
        var acoes = await _crawler.GetAcoes();
        await _context.Save(acoes);
        var acoesFilters = Acao.FilterBests(acoes);
        acoesFilters.ForEach(async acao =>
        {
            var dys = await GetDy(acao.Ticket, acao.Ticket);
            acao.CalculeDy(dys);
        });

        var bestAcoes = acoesFilters.Where(e => e.PrecoTeto > e.Preco).ToList();
        _excel.Write(bestAcoes);
    }

    public static async Task GenerateReportByIdiv(string path)
    {
        path = path ?? @"C:\Users\math\Downloads\IDIVDia_19-09-22.csv";
        var idivAcoes = _readCsv.Read(path);

        idivAcoes = idivAcoes.OrderByDescending(e => e.Part).ToList();

        var acoes = await _context.FindAcoes(idivAcoes.Select(e => e.Codigo));
        if (acoes.Count == 0)
        {
            acoes = await _crawler.GetAcoes();
            await _context.Save(acoes);
            acoes = acoes.Where(e => idivAcoes.Select(e => e.Codigo).Contains(e.Ticket)).ToList();
        }
        acoes = Acao.FilterBests(acoes);
        foreach (var acao in acoes)
        {
            var dy = await GetDy(acao.Ticket, acao.Ticket);
            acao.CalculeDy(dy);
        }
        var bestAcoes = acoes.Where(e => e.PrecoTeto > e.Preco).ToList();
        _excel.Write(bestAcoes);
    }

    public static async Task<List<Dy>> GetDy(string codigo, string name)
    {
        Thread.Sleep(DateTime.Now.Second % 2 == 0 ? 128 : 256);
        var dy = await _pricesApi.GetDY(codigo, name);
        return _mapper.Map<List<Dy>>(dy);
    }
}


