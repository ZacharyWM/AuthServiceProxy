using System.Threading.Tasks;
using myAuthApp.Models;
using System;

namespace myAuthApp.Services
{
    public interface IGoogleAPIs
    {
        Task<UserInfo_Google> GetUserInfo(string accessToken);

    }
}