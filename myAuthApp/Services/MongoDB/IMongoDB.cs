using MongoDB.Driver;
using myAuthApp.Models;

namespace myAuthApp.Services.MongoDB {
    public interface IMongoDB {
        IMongoClient GetClient();
        IMongoCollection<User> GetUserCollection();
    }
}