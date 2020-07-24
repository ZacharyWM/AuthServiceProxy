using System.Security.Authentication;
using MongoDB.Driver;
using myAuthApp.Models;
using myAuthApp.Services;

namespace myAuthApp.Store.UserStore
{
    public class LiveUserStore : IUserStore
    {

        // testing DI
        // binaryintellect.net/articles/17ee0ba2-99bb-47f0-ab18-f4fc32f476f8.aspx
        // IGoogleAuth _googleAuth;
        // public LiveUserStore([FromServices] IGoogleAuth googleAuth)
        // {
        //     _googleAuth = googleAuth;
        // }
        
        //public IGoogleAuth GoogleAuth { get; set; }

        public void DoStuff()
        {
            // string connectionString = "mongodb://acloudgurucosmosdb:ZXJ6pGRF8Oy5HzHDI5MnCcdVBVfhaR5IQNOSoOta2BjrIo7Gwb49qnn6sttiXh7xClBNXJDAM3CeYyVXol3WzA==@acloudgurucosmosdb.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@acloudgurucosmosdb@";

            // MongoClientSettings mongoSettings = MongoClientSettings.FromUrl(
            //   new MongoUrl(connectionString)
            // );
            // mongoSettings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            // var mongoClient = new MongoClient(mongoSettings);

            // var database = mongoClient.GetDatabase("userdb");

            // var userCollection = database.GetCollection<User>("user");



            // var filterBuilder = new FilterDefinitionBuilder<User>();
            // var filter = filterBuilder.Where(u => u.FirstName.Contains("zach"));

            // var updateDefinitionBuilder = new UpdateDefinitionBuilder<User>();
            // var updateDefinition = updateDefinitionBuilder.Set(x => x.FirstName, "zachary");

            // userCollection.UpdateOne(filter, updateDefinition);




            // var users = userCollection.Find(x => x.FirstName.Contains("zach"));

            // User zach = users.First();
        }
    }
}