using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using myAuthApp.Services;
using myAuthApp.Models;
using myAuthApp.Store.UserStore;
using System.Threading.Tasks;
using static myAuthApp.Enums.AllEnums;
using System;

namespace myAuthApp.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AuthController : CustomControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IGoogleAuth _googleAuth;
        private readonly IUserStore _userStore;

        public AuthController(ILogger<AuthController> logger,
                               ITokenService tokenService,
                               IGoogleAuth googleAuth,
                               IUserStore userStore,
                               IConfiguration config)
            : base(tokenService, config)
        {
            _logger = logger;
            _googleAuth = googleAuth;
            _userStore = userStore;
        }

        [HttpPost("google")]
        public async Task<IActionResult> AuthWithGoogleAsync(AuthCode authCode)
        {
            AuthResponse authResponse = await _googleAuth.GetToken(authCode);
            User user = _userStore.UpsertUserFromGoogleAuth(authResponse);

            string clientRedirectUri = String.IsNullOrWhiteSpace(authCode.ClientRedirectUri) ? null : $"{authCode.ClientRedirectUri}?auth_code={user.AuthCode}";

            return Ok(new
            {
                firstName = user.FirstName,
                lastName = user.LastName,
                email = user.EmailAddress,
                roles = user.Roles,
                client_redirect_uri = clientRedirectUri
            });
        }


        

    }



}
