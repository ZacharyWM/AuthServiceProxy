using Microsoft.AspNetCore.Mvc;

namespace myAuthApp.Controllers
{
    public class CustomControllerBase : ControllerBase
    {
        protected bool IsAdmin()
        {

            return true;
        }
        protected bool IsOwner()
        {

            return true;
        }
        protected bool IsUser()
        {

            return true;
        }


    }
}