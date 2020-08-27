using System.Security.Authentication;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using myAuthApp.Models;


namespace myAuthApp.Services.MongoDB
{
    public class LiveMongoDB : IMongoDB
    {
        private MongoClient _client;
        IMongoCollection<User> _userCollection;
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
            _userCollection = _client.GetDatabase("userdb").GetCollection<User>("user");
        }

        public IMongoClient GetClient() => _client;

        public IMongoCollection<User> GetUserCollection() => _userCollection;

    }
}