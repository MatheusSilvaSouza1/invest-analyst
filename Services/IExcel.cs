using invest_analyst.Domain;

namespace invest_analyst.Services
{
    public interface IExcel
    {
        void Write(List<Acoes> acoes);
        void Write(List<Acao> acoes);
    }
}