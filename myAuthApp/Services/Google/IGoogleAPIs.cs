using System.Threading.Tasks;
using myAuthApp.Models;
using System;

namespace myAuthApp.Services {
    public interface IGoogleAPIs {
        Task<GoogleUserInfo> GetUserInfo(string accessToken);

    }
}