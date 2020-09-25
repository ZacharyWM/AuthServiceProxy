using System;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace myAuthApp.Models
{
    public class AuthResponse
    {

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int expiresInSeconds { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("id_token")]
        public string IdToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        public string EmailAddress { get; set; }

        public int IssuedAtTime { get; set; }

        public int ExpirationTime { get; set; }
    }
}