using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using myAuthApp.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace myAuthApp.Services
{
    public class GoogleAuth : IGoogleAuth
    {
        private const string _tokenEndpoint = "https://accounts.google.com/o/oauth2/token";

        public GoogleAuth()
        {
        }




        public async Task<string> GetToken(AuthCode authCode)
        {
            HttpClient client = new HttpClient();
            var data = new StringContent("", Encoding.UTF8, "application/json");

            var queryParams = new Dictionary<string, string>() {
                                                    {"grant_type", "authorization_code"},
                                                    {"code", authCode.code},
                                                    {"redirect_uri", authCode.redirect_uri},
                                                    {"client_id", GetClientId()},
                                                    {"client_secret", GetClientSecret()},
                                                    {"state", authCode.state}
                                                };

            string uri = QueryHelpers.AddQueryString(_tokenEndpoint, queryParams);
            
            var response = await client.PostAsync(uri, data);
            string result = response.Content.ReadAsStringAsync().Result;

            return result;
        }

        private static string GetClientId()
        {
            return "884429750806-4lj7ea238v67c5681d707r3napu02q1e.apps.googleusercontent.com";
        }

        private static string GetClientSecret()
        {
            return "h23gSHfnBSv5QMXmbp4zdJch";
        }
    }
}