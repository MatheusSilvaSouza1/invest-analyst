using invest_analyst.Domain;

namespace invest_analyst.Infra.Database
{
    public interface IContext
    {
        Task Save(List<Acao> acoes);
        Task<List<Acao>> FindAcoes(IEnumerable<string> tickets);
    }
}