using myAuthApp.Models;

namespace myAuthApp.Store.UserStore
{
    public interface IUserStore
    {
         User UpsertUserFromGoogleAuth(AuthResponse auth);

    }
}