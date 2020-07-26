using System;
using System.Collections.Generic;
using System.Security.Authentication;
using MongoDB.Bson;
using MongoDB.Driver;
using myAuthApp.Models;
using myAuthApp.Services;
using static myAuthApp.Enums.AllEnums;

namespace myAuthApp.Store.UserStore
{
    public class LiveUserStore : IUserStore
    {
        private const string _connectionString = "mongodb://acloudgurucosmosdb:ZXJ6pGRF8Oy5HzHDI5MnCcdVBVfhaR5IQNOSoOta2BjrIo7Gwb49qnn6sttiXh7xClBNXJDAM3CeYyVXol3WzA==@acloudgurucosmosdb.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@acloudgurucosmosdb@";

        public LiveUserStore()
        {

        }
        // testing DI
        // binaryintellect.net/articles/17ee0ba2-99bb-47f0-ab18-f4fc32f476f8.aspx
        // IGoogleAuth _googleAuth;
        // public LiveUserStore([FromServices] IGoogleAuth googleAuth)
        // {
        //     _googleAuth = googleAuth;
        // }

        //public IGoogleAuth GoogleAuth { get; set; }


        public User UpdateUserGoogleAuth(AuthResponse auth)
        {

            MongoClientSettings mongoSettings = MongoClientSettings.FromUrl(
              new MongoUrl(_connectionString)
            );
            mongoSettings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(mongoSettings);

            var database = mongoClient.GetDatabase("userdb");

            var userCollection = database.GetCollection<User>("user");

            var findResult = userCollection.Find(x => x.EmailAddress == auth.EmailAddress);
            if (findResult.CountDocuments() == 0)
            {
                return CreateNewGoogleAuthUser(auth, userCollection);
            }

            return UpdateGoogleAuthUser(auth, userCollection);
        }

        private static User UpdateGoogleAuthUser(AuthResponse auth, IMongoCollection<User> userCollection)
        {
            var filterBuilder = new FilterDefinitionBuilder<User>();
            var filter = filterBuilder.Where(x => x.EmailAddress == auth.EmailAddress);

            var updateDefinitionBuilder = new UpdateDefinitionBuilder<User>();
            var updateDefinition = updateDefinitionBuilder.Set(x => x.GoogleAuth, auth);

            var updatedUser = userCollection.FindOneAndUpdate(filter, updateDefinition);
            updatedUser.GoogleAuth = auth;

            return updatedUser;
        }

        private static User CreateNewGoogleAuthUser(AuthResponse auth, IMongoCollection<User> userCollection)
        {

            var newUser = new User();
            newUser.FirstName = "firstname"; // TODO: get user details from Google
            newUser.LastName = "lastname";
            newUser.Roles = new List<string>() { RolesEnum.None.ToString() };
            newUser.EmailAddress = auth.EmailAddress;
            newUser.GoogleAuth = auth;
            userCollection.InsertOne(newUser);

            return newUser;
        }
    }
}