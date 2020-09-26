using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using myAuthApp.Services;
using myAuthApp.Models;
using myAuthApp.Store.UserStore;
using System.Threading.Tasks;
using System;



namespace myAuthApp.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class TokenController : CustomControllerBase {
        private readonly IUserStore _userStore;
        public TokenController(ITokenService tokenService,
                               IUserStore userStore,
                               IConfiguration config)
        : base(tokenService, config) {
            _userStore = userStore;
        }


        [HttpPost("verify")]
        public async Task<IActionResult> VerifyToken(string accessToken) {
            return Ok(new {
                isValidAccessToken = _tokenService.VerifyToken(accessToken)
            });
        }

        [HttpPost("exchange")]
        public async Task<IActionResult> Token(AuthCode authCode) {

            User user = _userStore.FindByAuthCode(authCode.Code);

            if (user == null) {
                return Unauthorized("Invalid authorization code.");
            }

            user.AccessToken = _tokenService.GetToken(user);
            user.AuthCode = string.Empty;

            bool validToken = _tokenService.VerifyToken(user.AccessToken);

            _ = _userStore.DeleteAuthCode(user);
            _ = _userStore.SetAccessToken(user);

            AddAuthCookie(user.AccessToken);

            return Ok(new {
                firstName = user.FirstName,
                lastName = user.LastName,
                email = user.EmailAddress,
                roles = user.Roles
            });
        }


    }
}