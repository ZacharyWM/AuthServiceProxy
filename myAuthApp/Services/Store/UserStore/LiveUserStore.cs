using System;
using System.Collections.Generic;
using System.Security.Authentication;
using MongoDB.Bson;
using MongoDB.Driver;
using myAuthApp.Models;
using myAuthApp.Services;
using Microsoft.Extensions.Configuration;
using static myAuthApp.Enums.AllEnums;
using myAuthApp.Services.MongoDB;

namespace myAuthApp.Store.UserStore
{
    public class LiveUserStore : IUserStore
    {
        private readonly IGoogleAPIs _googleAPIs;
        private readonly IMongoDB _mongoDB;
        private readonly IMongoCollection<User> _collection;

        public LiveUserStore(IGoogleAPIs googleAPIs, IMongoDB mongoDB)
        {
            _googleAPIs = googleAPIs;
            _mongoDB = mongoDB;
            _collection = _mongoDB.GetClient()
                                  .GetDatabase("userdb")
                                  .GetCollection<User>("user");
        }

        public User UpsertUserFromGoogleAuth(AuthResponse auth)
        {
            var findResult = _collection.Find(x => x.EmailAddress == auth.EmailAddress);
            if (findResult.CountDocuments() == 0)
            {
                return InsertGoogleAuthUser(auth);
            }

            return UpdateGoogleAuthUser(auth);
        }

        private User UpdateGoogleAuthUser(AuthResponse auth)
        {
            UserInfo_Google userInfo =  _googleAPIs.GetUserInfo(auth.AccessToken).Result;

            var filterBuilder = new FilterDefinitionBuilder<User>();
            var filter = filterBuilder.Where(x => x.EmailAddress == auth.EmailAddress);

            var updateDefinitionBuilder = new UpdateDefinitionBuilder<User>();
            var updateDefinition = updateDefinitionBuilder
                                                    .Set(x => x.GoogleAuth, auth)
                                                    .Set(x => x.FirstName, userInfo.FirstName)
                                                    .Set(x => x.LastName, userInfo.LastName);

            var updatedUser = _collection.FindOneAndUpdate(filter, updateDefinition);
            updatedUser.GoogleAuth = auth;

            return updatedUser;
        }

        private User InsertGoogleAuthUser(AuthResponse auth)
        {
            UserInfo_Google userinfo =  _googleAPIs.GetUserInfo(auth.AccessToken).Result;

            var newUser = new User();
            newUser.FirstName = userinfo.FirstName;
            newUser.LastName = userinfo.LastName;
            newUser.Roles = new List<string>() { RolesEnum.None.ToString() };
            newUser.EmailAddress = userinfo.Email;
            newUser.GoogleAuth = auth;
            _collection.InsertOne(newUser);

            return newUser;
        }
    }
}