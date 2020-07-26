using System;
using myAuthApp.Models;

namespace myAuthApp.Services
{
    public interface ITokenService
    {
        string GetToken(User user);

        bool ValidateToken(string token);

        string GetClaim(string token, string claimType);
        
    }
}