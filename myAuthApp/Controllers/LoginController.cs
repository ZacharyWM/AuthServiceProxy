using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace myAuthApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }


        [HttpGet] // default route
        public object Get()
        {
            return new {currentAction = "Get"};
        }

        [HttpPost("auth")]
        public object GetAuthToken(AuthCode authCode)
        {
            //get auth token from google, save it, return JWT associated with auth token to allow app access
            
            return new {theCode = authCode.code, theScope = authCode.scope};
        }
        
    }

    public struct AuthCode{
        public string code { get; set; }
        public string scope { get; set; }
        public string state { get; set; }
        public string redirect_uri { get; set; }
    }
}
