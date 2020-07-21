using System;

namespace myAuthApp.Services
{
    public interface ITokenService
    {
        string GetToken(Guid userId);

        bool ValidateToken(string token);

        string GetClaim(string token, string claimType);
        
    }
}