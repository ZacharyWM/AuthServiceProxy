using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace myAuthApp.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("firstName")]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        public string LastName { get; set; }

        [BsonElement("emailAddress")]
        public string EmailAddress { get; set; }
        
        [BsonElement("googleAuth")]
        public AuthResponse GoogleAuth { get; set; }

        // property to store ZachsAuthCenterToken
    }
}