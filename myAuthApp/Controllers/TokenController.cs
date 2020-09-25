using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using myAuthApp.Services;
using myAuthApp.Models;
using myAuthApp.Store.UserStore;
using System.Threading.Tasks;
using static myAuthApp.Enums.AllEnums;
using System;



namespace myAuthApp.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class TokenController : CustomControllerBase {

        private readonly ILogger<AuthController> _logger;
        private readonly IGoogleAuth _googleAuth;
        private readonly IUserStore _userStore;

        public TokenController(ILogger<AuthController> logger,
                               ITokenService tokenService,
                               IGoogleAuth googleAuth,
                               IUserStore userStore,
                               IConfiguration config)
        : base(tokenService, config) {
            _logger = logger;
            _googleAuth = googleAuth;
            _userStore = userStore;
        }


        [HttpPost("verify")]
        public async Task<IActionResult> VerifyToken(string accessToken) {
            return Ok(new {
                isValidAccessToken = _tokenService.ValidateToken(accessToken)
            });
        }

        [HttpPost("exchange")]
        public async Task<IActionResult> Token(AuthorizationCode authCode) {

            User user = _userStore.FindUserWithAuthCode(authCode.Code);

            if (user == null) {
                return Unauthorized("Invalid authorization code.");
            }

            user.AccessToken = _tokenService.GetToken(user);
            user.AuthCode = string.Empty;

            bool validToken = _tokenService.ValidateToken(user.AccessToken);

            _ = _userStore.UpdateUser(user);

            AddAuthCookie(user.AccessToken);

            return Ok(new {
                firstName = user.FirstName,
                lastName = user.LastName,
                email = user.EmailAddress,
                roles = user.Roles
            });
        }


    }

    public class AuthorizationCode {
        public string Code { get; set; }
    }
}