using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using myAuthApp.Models;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System.Text;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace myAuthApp.Services
{
    public class LiveGoogleAPIs : IGoogleAPIs
    {

        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _clientFactory;


        public LiveGoogleAPIs(IConfiguration config, IHttpClientFactory clientFactory)
        {
            _config = config;
            _clientFactory = clientFactory;
        }


        public async Task<UserInfo_Google> GetUserInfo(string accessToken){

            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get,"https://www.googleapis.com/oauth2/v2/userinfo");
            request.Headers.Add("Authorization", $"Bearer {accessToken}");

            var response = await client.SendAsync(request);
            string jsonResult = await response.Content.ReadAsStringAsync();

            UserInfo_Google userInfo = JsonConvert.DeserializeObject<UserInfo_Google>(jsonResult);

            return userInfo;
        }


    }
}