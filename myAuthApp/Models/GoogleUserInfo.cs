using Newtonsoft.Json;

namespace myAuthApp.Models {
    public class GoogleUserInfo {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("given_name")]
        public string FirstName { get; set; }

        [JsonProperty("family_name")]
        public string LastName { get; set; }

        [JsonProperty("picture")]
        public string PictureUri { get; set; }
    }
}