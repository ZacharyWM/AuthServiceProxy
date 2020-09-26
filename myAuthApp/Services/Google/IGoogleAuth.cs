using System.Threading.Tasks;
using myAuthApp.Models;
using System;

namespace myAuthApp.Services {
    public interface IGoogleAuth {
        Task<IdentityProviderAuthResponse> GetToken(IdentityProviderAuthCodeDetails authCode);

        Task<IdentityProviderAuthResponse> RefreshToken(IdentityProviderAuthCodeDetails authCode);

    }
}