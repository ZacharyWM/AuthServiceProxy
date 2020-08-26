using System;
using System.Collections.Generic;
using System.Security.Authentication;
using MongoDB.Bson;
using MongoDB.Driver;
using myAuthApp.Models;
using myAuthApp.Services;
using Microsoft.Extensions.Configuration;
using static myAuthApp.Enums.AllEnums;

namespace myAuthApp.Store.UserStore
{
    public class LiveUserStore : IUserStore
    {

        private readonly IConfiguration _config;
        private readonly IGoogleAuth _googleAuth;

        private string ConnectionString => _config.GetConnectionString("MongoDBConnectionString");

        public LiveUserStore(IConfiguration config, IGoogleAuth googleAuth)
        {
            _config = config;
            _googleAuth = googleAuth;
        }

        public User UpdateUserGoogleAuth(AuthResponse auth)
        {
            MongoClientSettings mongoSettings = MongoClientSettings.FromUrl(
              new MongoUrl(ConnectionString)
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

        private User UpdateGoogleAuthUser(AuthResponse auth, IMongoCollection<User> userCollection)
        {

            UserInfo_Google userinfo =  _googleAuth.GetUserInfo(auth.AccessToken).Result;
            // TODO update user info
            var filterBuilder = new FilterDefinitionBuilder<User>();
            var filter = filterBuilder.Where(x => x.EmailAddress == auth.EmailAddress);

            var updateDefinitionBuilder = new UpdateDefinitionBuilder<User>();
            var updateDefinition = updateDefinitionBuilder.Set(x => x.GoogleAuth, auth);

            var updatedUser = userCollection.FindOneAndUpdate(filter, updateDefinition);
            updatedUser.GoogleAuth = auth;

            return updatedUser;
        }

        private User CreateNewGoogleAuthUser(AuthResponse auth, IMongoCollection<User> userCollection)
        {

            UserInfo_Google userinfo =  _googleAuth.GetUserInfo(auth.AccessToken).Result;

            var newUser = new User();
            newUser.FirstName = userinfo.FirstName;
            newUser.LastName = userinfo.LastName;
            newUser.Roles = new List<string>() { RolesEnum.None.ToString() };
            newUser.EmailAddress = userinfo.Email;
            newUser.GoogleAuth = auth;
            userCollection.InsertOne(newUser);

            return newUser;
        }
    }
}