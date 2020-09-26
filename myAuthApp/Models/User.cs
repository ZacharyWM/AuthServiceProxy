using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace myAuthApp.Models {
    public class User {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("firstName")]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        public string LastName { get; set; }

        [BsonElement("emailAddress")]
        public string EmailAddress { get; set; }

        [BsonElement("authCode")]
        public string AuthCode { get; set; }

        [BsonElement("accessToken")]
        public string AccessToken { get; set; }

        [BsonElement("roles")]
        public List<string> Roles { get; set; }

        [BsonElement("googleAuth")]
        public IdentityProviderAuthResponse GoogleAuth { get; set; }

    }
}