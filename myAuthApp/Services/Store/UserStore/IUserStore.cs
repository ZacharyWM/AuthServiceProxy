using System.Threading.Tasks;
using myAuthApp.Models;

namespace myAuthApp.Store.UserStore {
    public interface IUserStore {
        User UpsertUserFromGoogleAuth(AuthResponse auth);

        User FindUserWithAuthCode(string authCode);

        Task UpdateUser(User updatedUser);

    }
}