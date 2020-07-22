using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using myAuthApp.Services;
using myAuthApp.Models;
using System.Net.Http;

namespace myAuthApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly ILogger<LoginController> _logger;
        private readonly ITokenService _tokenService;
        private readonly IGoogleAuth _googleAuth;
        private readonly IHttpClientFactory _clientFactory;

        public LoginController(ILogger<LoginController> logger, ITokenService tokenService, IGoogleAuth googleAuth, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _tokenService = tokenService;
            _googleAuth = googleAuth;
            _clientFactory = clientFactory;
        }


        [HttpGet] // default route
        public object Get()
        {
            return new { currentAction = "Get" };
        }

        [HttpPost("google")]
        public async System.Threading.Tasks.Task<object> GetGoogleAuthTokenAsync(AuthCode authCode)
        {
            Guid userId = Guid.NewGuid();

            var authToken = await _googleAuth.GetToken(authCode);

            //get auth token from google, save it, return JWT associated with auth token to allow app access

            return new { theCode = authCode.code, theScope = authCode.scope, theToken = _tokenService.GetToken(userId) };
        }
    }

}
