using invest_analyst.Domain;

namespace invest_analyst.Services
{
    public interface ICrawler
    {
        Task<List<Acao>> GetAcoes();
        Task<List<Fiis>> GetFiis();
    }
}