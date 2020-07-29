using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using myAuthApp.Services;
using myAuthApp.Models;
using myAuthApp.Store.UserStore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Linq;
using static myAuthApp.Enums.AllEnums;

namespace myAuthApp.Controllers
{

    // TODO maybe change to "AuthController"
    [ApiController]
    [Route("[controller]")]
    public class LoginController : CustomControllerBase
    {

        private readonly ILogger<LoginController> _logger;
        private readonly IGoogleAuth _googleAuth;
        private readonly IUserStore _userStore;

        public LoginController(ILogger<LoginController> logger, ITokenService tokenService, IGoogleAuth googleAuth, IUserStore userStore)
            : base(tokenService)
        {
            _logger = logger;
            _googleAuth = googleAuth;
            _userStore = userStore;
        }


        [HttpGet] // default route
        public object Get()
        {
            return new { currentAction = "Get" };
        }

        [HttpPost("google")]
        public async Task<IActionResult> AuthWithGoogleAsync(AuthCode authCode)
        {
            var authResponse = await _googleAuth.GetToken(authCode);

            User user = _userStore.UpdateUserGoogleAuth(authResponse);

            string jwt = _tokenService.GetToken(user);

            AddAuthCookie(jwt);

            if (!HasRole(RolesEnum.User))
            {
                return Unauthorized("You don't have permission to use Zach's Auth Center.");
            }


            return Ok(new
            {
                firstName = user.FirstName,
                lastName = user.LastName,
                email = user.EmailAddress,
                roles = user.Roles
            });
        }

        private void AddAuthCookie(string jwt)
        {
            var cookieOptions = new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            };

            string authCookieName = "zachsauthcenter";

            Response.Cookies.Delete(authCookieName);
            Response.Cookies.Append(authCookieName, jwt, cookieOptions);
        }
    }



}
