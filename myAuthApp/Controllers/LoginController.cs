using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using myAuthApp.Services;
using myAuthApp.Models;
using System.Net.Http;
using MongoDB.Driver;
using System.Security.Authentication;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace myAuthApp.Controllers
{

    // TODO maybe change to "AuthController"
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly ILogger<LoginController> _logger;
        private readonly ITokenService _tokenService;
        private readonly IGoogleAuth _googleAuth;

        public LoginController(ILogger<LoginController> logger, ITokenService tokenService, IGoogleAuth googleAuth)
        {
            _logger = logger;
            _tokenService = tokenService;
            _googleAuth = googleAuth;
        }


        [HttpGet] // default route
        public object Get()
        {
            return new { currentAction = "Get" };
        }

        [HttpPost("google")]
        public async System.Threading.Tasks.Task<object> GetGoogleAuthTokenAsync(AuthCode authCode)
        {
            Guid userId = Guid.NewGuid();

            var authToken = await _googleAuth.GetToken(authCode);

            //get auth token from google, save it, return JWT associated with auth token to allow app access

            string connectionString =
              @"mongodb://acloudgurucosmosdb:ZXJ6pGRF8Oy5HzHDI5MnCcdVBVfhaR5IQNOSoOta2BjrIo7Gwb49qnn6sttiXh7xClBNXJDAM3CeYyVXol3WzA==@acloudgurucosmosdb.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@acloudgurucosmosdb@";

            MongoClientSettings mongoSettings = MongoClientSettings.FromUrl(
              new MongoUrl(connectionString)
            );
            mongoSettings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(mongoSettings);

            var database = mongoClient.GetDatabase("userdb");

           // database.CreateCollection("ZachsNewCollection");

            var userCollection = database.GetCollection<object>("user");
            var users = userCollection.CountDocuments(x => true); // counting works! Figure out to Find() with model



            return new { theCode = authCode.code, theScope = authCode.scope, theToken = _tokenService.GetToken(userId) };
        }
    }

    public class User{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public object _id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public object[] children { get; set; }
    }

}
