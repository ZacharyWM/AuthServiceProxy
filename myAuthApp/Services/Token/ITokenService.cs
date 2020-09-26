using System;
using System.Collections.Generic;
using System.Security.Claims;
using myAuthApp.Models;

namespace myAuthApp.Services {
    public interface ITokenService {
        string GetToken(User user);

        bool VerifyToken(string token);

        IEnumerable<Claim> GetAllClaims(string token);

    }
}