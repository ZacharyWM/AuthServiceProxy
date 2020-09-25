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
    public class AuthController : CustomControllerBase {
        private readonly ILogger<AuthController> _logger;
        private readonly IGoogleAuth _googleAuth;
        private readonly IUserStore _userStore;

        public AuthController(ILogger<AuthController> logger,
                               ITokenService tokenService,
                               IGoogleAuth googleAuth,
                               IUserStore userStore,
                               IConfiguration config)
        : base(tokenService, config) {
            _logger = logger;
            _googleAuth = googleAuth;
            _userStore = userStore;
        }

        [HttpPost("google")]
        public async Task<IActionResult> AuthWithGoogleAsync(AuthCode authCode) {
            AuthResponse authResponse = await _googleAuth.GetToken(authCode);
            User user = _userStore.UpsertUserFromGoogleAuth(authResponse);

            return Ok(new {
                client_redirect_uri = String.IsNullOrWhiteSpace(authCode.ClientRedirectUri) ? null : $"{authCode.ClientRedirectUri}?auth_code={user.AuthCode}",
                auth_code = user.AuthCode
            });
        }
    }



}
