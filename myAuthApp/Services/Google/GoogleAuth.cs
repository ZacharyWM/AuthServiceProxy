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

        public string TokenEndpoint => _config.GetValue<string>("GoogleTokenEndpoint");
        public string ClientId => _config.GetValue<string>("GoogleClientId");
        public string ClientSecret => _config.GetValue<string>("GoogleClientSecret");



        // TODO Add PKCE
        // https://auth0.com/docs/flows/guides/auth-code-pkce/call-api-auth-code-pkce

        public GoogleAuth(IConfiguration config)
        {
            _config = config;
        }

        public async Task<AuthResponse> GetToken(AuthCode authCode)
        {
            HttpClient client = new HttpClient();
            var data = new StringContent("", Encoding.UTF8, "application/x-www-form-urlencoded");

            var queryParams = new Dictionary<string, string>() {
                                                    {"grant_type", "authorization_code"},
                                                    {"code", authCode.code},
                                                    {"redirect_uri", authCode.redirect_uri},
                                                    {"client_id", ClientId},
                                                    {"client_secret", ClientSecret},
                                                    {"include_granted_scopes", "true"} // optional
                                                };


            string uri = QueryHelpers.AddQueryString(TokenEndpoint, queryParams);
            var response = await client.PostAsync(uri, data);
            
            string jsonResult = await response.Content.ReadAsStringAsync();

            return ConvertResultToAuthResponse(jsonResult);
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