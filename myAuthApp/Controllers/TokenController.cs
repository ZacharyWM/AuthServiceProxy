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
    public class TokenController : CustomControllerBase
    {


        public TokenController(ITokenService tokenService, IConfiguration config) 
            : base(tokenService, config)
        {

        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyToken(string accessToken){


            return Ok(new
            {

            });         
        }

        [HttpPost("token")]
        public async Task<IActionResult> Token(string authCode){

            // lookup user by authcode

            // if user found, create access token, save for the user, and add token to cookies

            /*
            string jwt = _tokenService.GetToken(user);

            AddAuthCookie(jwt);
            */

            // if not found, return Unauthorized? 



            return Ok(new
            {
                // return user info here? And not from the auth controller?
            });         
        }


        // verify token method

        // take a auth code, return access token


    }
}