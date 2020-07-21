using System.Threading.Tasks;
using myAuthApp.Models;

namespace myAuthApp.Services
{
    public interface IGoogleAuth
    {
        Task<string> GetToken(AuthCode authCode);

    }
}