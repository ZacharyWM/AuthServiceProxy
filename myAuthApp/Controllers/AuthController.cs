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

    // TODO maybe change to "AuthController"
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


        [HttpGet] // default route
        public object Get()
        {
            return new { currentAction = "Get" };
        }

        [HttpPost("google")]
        public async Task<IActionResult> AuthWithGoogleAsync(AuthCode authCode)
        {
            try{

                // TODO: Write Token controller that accepts auth code and returns access token

                var authResponse = await _googleAuth.GetToken(authCode);

                User user = _userStore.UpsertUserFromGoogleAuth(authResponse);

                // string jwt = _tokenService.GetToken(user);

                // AddAuthCookie(jwt);

                // if (!HasRole(RolesEnum.User))
                // {
                //     return Unauthorized("You don't have permission to use Zach's Auth Center.");
                // }

                return Ok(new
                {
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    email = user.EmailAddress,
                    roles = user.Roles,
                    redirect_uri = $"https://www.hcss.com?auth_code={user.AuthCode}"
                });
            }
            catch(Exception ex){
                return Unauthorized();
            }

        }

    }



}
