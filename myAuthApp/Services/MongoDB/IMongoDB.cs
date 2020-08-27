using MongoDB.Driver;

namespace myAuthApp.Services.MongoDB
{
    public interface IMongoDB
    {
         IMongoClient GetClient();
    }
}