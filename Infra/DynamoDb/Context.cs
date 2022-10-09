using Amazon.DynamoDBv2.DataModel;
using invest_analyst.Domain;

namespace invest_analyst.Infra.DynamoDb
{
    public class Context : IContext
    {
        private readonly DynamoDBContext _context;
        public Context()
        {
            _context = InitializeDynamoDb.CreateConnection();
        }

        public async Task Save(List<Acao> acoes)
        {
            var acoesBatch = _context.CreateBatchWrite<Acao>();
            foreach (var chunk in acoes.Chunk(25))
            {
                acoesBatch.AddPutItems(chunk);
                await acoesBatch.ExecuteAsync();
            }
        }
    }
}