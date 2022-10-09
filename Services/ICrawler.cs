using invest_analyst.Domain;

namespace invest_analyst.Services
{
    public interface ICrawler
    {
        Task<Acoes> GetInfosAsync(string ticket);
        Task<List<Acao>> GetAcoes();
    }
}