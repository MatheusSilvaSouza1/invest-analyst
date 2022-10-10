using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using invest_analyst.Domain;
using MongoDB.Driver;

namespace invest_analyst.Infra.Database
{
    public class ContextDb : IContext
    {
        private readonly IMongoCollection<Acao> _context;
        public ContextDb()
        {
            var client = InitializeMongoDb.Init();
            var mongoDatabase = client.GetDatabase("Analyst");

            _context = mongoDatabase.GetCollection<Acao>("Acoes");
        }

        public async Task<List<Acao>> FindAcoes(IEnumerable<string> tickets)
        {
            var filter = Builders<Acao>.Filter;
            var result = await _context.FindAsync(e => tickets.Contains(e.Ticket) && e.DtAnalise >= DateTime.Now.Date && e.DtAnalise <= DateTime.Now);
            return await result.ToListAsync();
        }

        public async Task Save(List<Acao> acoes)
        {
            await _context.InsertManyAsync(acoes);
        }
    }
}