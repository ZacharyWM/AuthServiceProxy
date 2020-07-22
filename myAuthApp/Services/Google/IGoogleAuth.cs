using System.Threading.Tasks;
using myAuthApp.Models;

namespace myAuthApp.Services
{
    public interface IGoogleAuth
    {
        Task<AuthResponse> GetToken(AuthCode authCode);

        Task<AuthResponse> RefreshToken(AuthCode authCode);


    }
}