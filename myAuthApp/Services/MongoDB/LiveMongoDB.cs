using System.Security.Authentication;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace myAuthApp.Services.MongoDB
{
    public class LiveMongoDB : IMongoDB
    {
        private MongoClient _client;
        private IConfiguration _config;
        private string ConnectionString => _config.GetConnectionString("MongoDBConnectionString");

        public LiveMongoDB(IConfiguration config)
        {
            _config = config;

            MongoClientSettings clientSettings = MongoClientSettings.FromUrl(
              new MongoUrl(ConnectionString)
            );
            clientSettings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

            _client = new MongoClient(clientSettings);
        }

        public IMongoClient GetClient() => _client;

    }
}