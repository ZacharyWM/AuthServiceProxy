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
    public class AuthController : CustomControllerBase {
        private readonly IGoogleAuth _googleAuth;
        private readonly IUserStore _userStore;

        public AuthController(ITokenService tokenService,
                               IGoogleAuth googleAuth,
                               IUserStore userStore,
                               IConfiguration config)
        : base(tokenService, config) {
            _googleAuth = googleAuth;
            _userStore = userStore;
        }

        [HttpPost("google")]
        public async Task<IActionResult> AuthWithGoogleAsync(IdentityProviderAuthCodeDetails authCode) {
            IdentityProviderAuthResponse authResponse = await _googleAuth.GetToken(authCode);
            User user = _userStore.UpsertFromGoogleAuth(authResponse);

            bool hasRedirectUri = !String.IsNullOrWhiteSpace(authCode.ClientRedirectUri);
            string redirectUri = hasRedirectUri ? $"{authCode.ClientRedirectUri}?auth_code={user.AuthCode}" : null;

            return Ok(new {
                client_redirect_uri = redirectUri,
                auth_code = user.AuthCode
            });
        }
    }



}
