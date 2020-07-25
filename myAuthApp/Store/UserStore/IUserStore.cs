using myAuthApp.Models;

namespace myAuthApp.Store.UserStore
{
    public interface IUserStore
    {
         User UpdateUserGoogleAuth(AuthResponse auth);

    }
}