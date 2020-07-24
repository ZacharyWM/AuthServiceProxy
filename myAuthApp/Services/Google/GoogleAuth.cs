using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using myAuthApp.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System;

namespace myAuthApp.Services
{
    public class GoogleAuth : IGoogleAuth
    {
        private const string _tokenEndpoint = "https://accounts.google.com/o/oauth2/token";


        public GoogleAuth()
        {

        }

        public async Task<AuthResponse> GetToken(AuthCode authCode)
        {
            HttpClient client = new HttpClient();
            var data = new StringContent("", Encoding.UTF8, "application/x-www-form-urlencoded");

            var queryParams = new Dictionary<string, string>() {
                                                    {"grant_type", "authorization_code"},
                                                    {"code", authCode.code},
                                                    {"redirect_uri", authCode.redirect_uri},
                                                    {"client_id", GetClientId()},
                                                    {"client_secret", GetClientSecret()},
                                                    {"include_granted_scopes", "true"} // optional
                                                };
            

            string uri = QueryHelpers.AddQueryString(_tokenEndpoint, queryParams);
            var response = await client.PostAsync(uri, data);
            string jsonResult = response.Content.ReadAsStringAsync().Result;

            AuthResponse authResponse = JsonConvert.DeserializeObject<AuthResponse>(jsonResult);

            return authResponse;
        }

        public async Task<AuthResponse> RefreshToken(AuthCode authCode){

            

            return new AuthResponse();
        }

        private static string GetClientId()
        {
            // TODO: store securely
            return "884429750806-4lj7ea238v67c5681d707r3napu02q1e.apps.googleusercontent.com";
        }

        private static string GetClientSecret()
        {
            // TODO: store securely
            return "h23gSHfnBSv5QMXmbp4zdJch";
        }
    }
}