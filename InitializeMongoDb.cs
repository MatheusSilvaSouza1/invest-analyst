using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace invest_analyst
{
    public static class InitializeMongoDb
    {
        public static MongoClient MongoClient;
        public static MongoClient Init()
        {
            if (MongoClient != null)
            {
                return MongoClient;
            }
            var configProg = new ConfigurationBuilder()
                                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                .AddUserSecrets<Program>()
                                .Build();

            var user = configProg.GetSection("DbUser").Value;
            var password = configProg.GetSection("DbPassword").Value;
            MongoClient = new MongoClient($"mongodb://{user}:{password}@localhost:27017/admin");
            return MongoClient;
        }
    }
}