using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using myAuthApp.Services;
using myAuthApp.Models;
using myAuthApp.Store.UserStore;

namespace myAuthApp.Controllers
{

    // TODO maybe change to "AuthController"
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly ILogger<LoginController> _logger;
        private readonly ITokenService _tokenService;
        private readonly IGoogleAuth _googleAuth;
        private readonly IUserStore _userStore;

        public LoginController(ILogger<LoginController> logger, ITokenService tokenService, IGoogleAuth googleAuth, IUserStore userStore)
        {
            _logger = logger;
            _tokenService = tokenService;
            _googleAuth = googleAuth;
            _userStore = userStore;
        }


        [HttpGet] // default route
        public object Get()
        {
            return new { currentAction = "Get" };
        }

        [HttpPost("google")]
        public async System.Threading.Tasks.Task<object> AuthWithGoogleAsync(AuthCode authCode)
        {
            var authResponse = await _googleAuth.GetToken(authCode);

            User user = _userStore.UpdateUserGoogleAuth(authResponse);

            // TODO: create some type of return object with everything needed.
            return new {
                userFirstName = user.FirstName,
                userLastName = user.LastName,
                token = _tokenService.GetToken(user.Id)
            };
        }
    }



}
