using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;

namespace invest_analyst
{
    public static class InitializeDynamoDb
    {
        private static BasicAWSCredentials credentials;
        private static AmazonDynamoDBConfig config;
        private static AmazonDynamoDBClient client;
        public static DynamoDBContext dynamoDBContext;

        public static DynamoDBContext CreateConnection()
        {
            var configProg = new ConfigurationBuilder()
                                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                .AddUserSecrets<Program>()
                                .Build();

            var accessKey = configProg.GetSection("accessKey").Value;
            var secretKey = configProg.GetSection("secretKey").Value;

            credentials = new BasicAWSCredentials(accessKey, secretKey);
            config = new AmazonDynamoDBConfig() { RegionEndpoint = Amazon.RegionEndpoint.USEast1 };
            client = new AmazonDynamoDBClient(credentials, config);
            
            if (dynamoDBContext == null)
            {
                dynamoDBContext = new DynamoDBContext(client);
                return dynamoDBContext;
            }

            return dynamoDBContext;
        }
    }
}