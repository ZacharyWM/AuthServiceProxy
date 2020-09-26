using System.Threading.Tasks;
using myAuthApp.Models;

namespace myAuthApp.Store.UserStore {
    public interface IUserStore {
        User UpsertFromGoogleAuth(IdentityProviderAuthResponse auth);

        User FindByAuthCode(string authCode);

        Task DeleteAuthCode(User updatedUser);

        Task SetAccessToken(User updatedUser);
    }
}