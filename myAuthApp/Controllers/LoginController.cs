using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using myAuthApp.Services;

namespace myAuthApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly ILogger<LoginController> _logger;
        private readonly ITokenService _tokenService;

        public LoginController(ILogger<LoginController> logger, ITokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
        }


        [HttpGet] // default route
        public object Get()
        {
            return new { currentAction = "Get" };
        }

        [HttpPost("auth")]
        public object GetAuthToken(AuthCode authCode)
        {
            Guid userId = Guid.NewGuid();

            //get auth token from google, save it, return JWT associated with auth token to allow app access

            return new { theCode = authCode.code, theScope = authCode.scope, theToken = _tokenService.GetToken(userId) };
        }
    }

    public struct AuthCode
    {
        public string code { get; set; }
        public string scope { get; set; }
        public string state { get; set; }
        public string redirect_uri { get; set; }
    }
}
