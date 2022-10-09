using invest_analyst.Domain;

namespace invest_analyst.Infra.DynamoDb
{
    public interface IContext
    {
        Task Save(List<Acao> acoes);
    }
}