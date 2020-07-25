using System;

namespace myAuthApp.Services
{
    public interface ITokenService
    {
        string GetToken(string userId);

        bool ValidateToken(string token);

        string GetClaim(string token, string claimType);
        
    }
}