using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using myAuthApp.Models;

namespace myAuthApp.Services
{
    public class TokenService : ITokenService
    {
        // https://dotnetcoretutorials.com/2020/01/15/creating-and-validating-jwt-tokens-in-asp-net-core/

        private readonly string _tokenSecret = "asdv234234^&%&^%&^hjsdfb2%%%";
        private readonly string _issuer = "https://zach.com";
        private readonly string _audience = "https://zachsfriends.com";

        public string GetToken(User user)
        {

            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenSecret));

            var claims = new List<Claim>() {
                                new Claim(ClaimTypes.Name, user.FirstName),
                                new Claim(ClaimTypes.NameIdentifier, user.Id),
                                new Claim(ClaimTypes.Email, user.EmailAddress)
            };
            user.Roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public bool ValidateToken(string token)
        {
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenSecret));
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var validationParams = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _issuer,
                    ValidAudience = _audience,
                    IssuerSigningKey = mySecurityKey
                };

                tokenHandler.ValidateToken(token, validationParams, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }



        public IEnumerable<Claim> GetAllClaims(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            return securityToken.Claims;
        }
    }
}