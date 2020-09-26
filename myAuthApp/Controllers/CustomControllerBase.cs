using Microsoft.AspNetCore.Mvc;
using System.Linq;
using myAuthApp.Services;
using Microsoft.Extensions.Configuration;
using static myAuthApp.Enums.AllEnums;
using Microsoft.AspNetCore.Http;

namespace myAuthApp.Controllers {
    public class CustomControllerBase : ControllerBase {
        protected readonly ITokenService _tokenService;
        private readonly IConfiguration _config;
        protected string IssuedTokenName => _config.GetValue<string>("IssuedTokenName");

        public CustomControllerBase(ITokenService tokenService, IConfiguration config) {
            _tokenService = tokenService;
            _config = config;
        }

        protected bool HasRole(RolesEnum role) {

            if (Request.Cookies.TryGetValue(IssuedTokenName, out string jwt)) {
                return _tokenService.GetAllClaims(jwt)
                                    .Any(claim => claim.Type == "role" && claim.Value == role.ToString());
            }

            return false;
        }
        protected void AddAuthCookie(string jwt) {
            var cookieOptions = new CookieOptions {
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            };

            string authCookieName = IssuedTokenName;

            Response.Cookies.Delete(authCookieName);
            Response.Cookies.Append(authCookieName, jwt, cookieOptions);
        }


    }
}