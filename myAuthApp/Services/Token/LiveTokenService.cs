using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using myAuthApp.Models;
using Microsoft.Extensions.Configuration;

namespace myAuthApp.Services {
    public class LiveTokenService : ITokenService {
        private readonly IConfiguration _config;
        private string TokenSecret => _config.GetValue<string>("TokenPrivateKey");
        private string TokenAudience => _config.GetValue<string>("TokenAudience");
        private string TokenIssuer => _config.GetValue<string>("TokenIssuer");

        public LiveTokenService(IConfiguration config) {
            _config = config;
        }

        public string GetToken(User user) {
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenSecret));

            var claims = new List<Claim>() {
                                new Claim(ClaimTypes.Name, user.FirstName),
                                new Claim(ClaimTypes.NameIdentifier, user.Id),
                                new Claim(ClaimTypes.Email, user.EmailAddress)
            };
            user.Roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = TokenIssuer,
                Audience = TokenAudience,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public bool VerifyToken(string token) {
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenSecret));
            var tokenHandler = new JwtSecurityTokenHandler();
            try {
                var validationParams = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = TokenIssuer,
                    ValidAudience = TokenAudience,
                    IssuerSigningKey = mySecurityKey
                };

                tokenHandler.ValidateToken(token, validationParams, out SecurityToken validatedToken);

                return validatedToken != null;
            } catch (Exception ex) {
                return false;
            }
        }

        public IEnumerable<Claim> GetAllClaims(string token) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            return securityToken.Claims;
        }
    }
}