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
    public class GoogleAuth : IGoogleAuth
    {

        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _clientFactory;

        public string TokenEndpoint => _config.GetValue<string>("GoogleTokenEndpoint");
        public string ClientId => _config.GetValue<string>("GoogleClientId");
        public string ClientSecret => _config.GetValue<string>("GoogleClientSecret");



        // TODO Add PKCE
        // https://auth0.com/docs/flows/guides/auth-code-pkce/call-api-auth-code-pkce

        public GoogleAuth(IConfiguration config, IHttpClientFactory clientFactory)
        {
            _config = config;
            _clientFactory = clientFactory;
        }

        public async Task<AuthResponse> GetToken(AuthCode authCode)
        {
            var queryParams = new Dictionary<string, string>() {
                                                    {"grant_type", "authorization_code"},
                                                    {"code", authCode.code},
                                                    {"redirect_uri", authCode.redirect_uri},
                                                    {"client_id", ClientId},
                                                    {"client_secret", ClientSecret},
                                                    {"include_granted_scopes", "true"} // optional
                                                };

            var client = _clientFactory.CreateClient();
            string uri = QueryHelpers.AddQueryString(TokenEndpoint, queryParams);

            var request = new HttpRequestMessage(HttpMethod.Post,uri);
            request.Content = new StringContent("", Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await client.SendAsync(request);
            
            string jsonResult = await response.Content.ReadAsStringAsync();

            return ConvertResultToAuthResponse(jsonResult);
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

        public async Task<AuthResponse> RefreshToken(AuthCode authCode)
        {



            return new AuthResponse();
        }

        private AuthResponse ConvertResultToAuthResponse(string jsonResult)
        {
            AuthResponse authResponse = JsonConvert.DeserializeObject<AuthResponse>(jsonResult);

            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.ReadJwtToken(authResponse.IdToken);
            
            authResponse.EmailAddress = token.Payload.GetValueOrDefault("email").ToString();
            authResponse.IssuedAtTime = Convert.ToInt32(token.Payload.GetValueOrDefault("iat"));
            authResponse.ExpirationTime = Convert.ToInt32(token.Payload.GetValueOrDefault("exp"));

            return authResponse;
        }



    }
}