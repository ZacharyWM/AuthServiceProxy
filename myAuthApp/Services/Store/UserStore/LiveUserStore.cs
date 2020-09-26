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
using myAuthApp.Utility;
using System.Threading.Tasks;

namespace myAuthApp.Store.UserStore {
    public class LiveUserStore : IUserStore {
        private readonly IGoogleAPIs _googleAPIs;
        private readonly IMongoCollection<User> _collection;

        public LiveUserStore(IGoogleAPIs googleAPIs, IMongoDB mongoDB) {
            _googleAPIs = googleAPIs;
            _collection = mongoDB.GetUserCollection();
        }

        public User UpsertFromGoogleAuth(IdentityProviderAuthResponse auth) {
            var findResult = _collection.Find(x => x.EmailAddress == auth.EmailAddress);
            if (findResult.CountDocuments() == 0) {
                return InsertUserWithGoogleAuth(auth);
            }

            return UpdateUserWithGoogleAuth(auth);
        }

        public User FindByAuthCode(string authCode) {
            if (string.IsNullOrWhiteSpace(authCode)) {
                return null;
            }

            return _collection.Find<User>(x => x.AuthCode == authCode)
                              .FirstOrDefault<User>();
        }

        public Task DeleteAuthCode(User updatedUser) {
            return Task.Run(() => {
                _collection.FindOneAndUpdate(
                    new FilterDefinitionBuilder<User>().Where(x => x.Id == updatedUser.Id),
                    new UpdateDefinitionBuilder<User>().Set(x => x.AuthCode, string.Empty)
                );
            });
        }

        public Task SetAccessToken(User updatedUser) {
            return Task.Run(() => {
                _collection.FindOneAndUpdate(
                    new FilterDefinitionBuilder<User>().Where(x => x.Id == updatedUser.Id),
                    new UpdateDefinitionBuilder<User>().Set(x => x.AccessToken, updatedUser.AccessToken)
                );
            });
        }

        private User UpdateUserWithGoogleAuth(IdentityProviderAuthResponse auth) {
            GoogleUserInfo userInfo = _googleAPIs.GetUserInfo(auth.AccessToken).Result;
            string newAuthCode = AuthCodeUtility.GenerateAuthCode();

            var updatedUser = _collection.FindOneAndUpdate(
                                    new FilterDefinitionBuilder<User>().Where(x => x.EmailAddress == auth.EmailAddress),
                                    new UpdateDefinitionBuilder<User>().Set(x => x.GoogleAuth, auth)
                                                                       .Set(x => x.FirstName, userInfo.FirstName)
                                                                       .Set(x => x.LastName, userInfo.LastName)
                                                                       .Set(x => x.AuthCode, newAuthCode)
                              );

            updatedUser.GoogleAuth = auth;
            updatedUser.AuthCode = newAuthCode;

            return updatedUser;
        }

        private User InsertUserWithGoogleAuth(IdentityProviderAuthResponse auth) {
            GoogleUserInfo userinfo = _googleAPIs.GetUserInfo(auth.AccessToken).Result;

            var newUser = new User();
            newUser.FirstName = userinfo.FirstName;
            newUser.LastName = userinfo.LastName;
            newUser.Roles = new List<string>() { RolesEnum.None.ToString() };
            newUser.EmailAddress = userinfo.Email;
            newUser.GoogleAuth = auth;
            newUser.AuthCode = AuthCodeUtility.GenerateAuthCode();

            _collection.InsertOne(newUser);

            return newUser;
        }
    }
}