using System.Threading.Tasks;
using myAuthApp.Models;
using System;

namespace myAuthApp.Services
{
    public interface IGoogleAuth
    {
        Task<AuthResponse> GetToken(AuthCode authCode);

        Task<AuthResponse> RefreshToken(AuthCode authCode);

        Task<UserInfo_Google> GetUserInfo(string accessToken);

    }
}