using System.Threading.Tasks;

namespace myAuthApp.Services
{
    public interface IGoogleAuth
    {
        Task<string> GetToken(string authCode);

    }
}