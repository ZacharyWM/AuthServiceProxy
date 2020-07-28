using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using myAuthApp.Services;
using static myAuthApp.Enums.AllEnums;

namespace myAuthApp.Controllers
{
    public class CustomControllerBase : ControllerBase
    {
        protected readonly ITokenService _tokenService;
        protected readonly string _securityTokenName = "zachsauthcenter";

        public CustomControllerBase(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        protected bool HasRole(RolesEnum role)
        {

            if (Request.Cookies.TryGetValue(_securityTokenName, out string jwt))
            {
                return _tokenService.GetAllClaims(jwt)
                                    .Any(claim => claim.Type == "role" && claim.Value == role.ToString());
            }

            return false;
        }



    }
}