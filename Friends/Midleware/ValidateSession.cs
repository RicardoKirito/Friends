using Friends.Core.Application.Helpers;
using Friends.Core.Application.ViewModels.User;

namespace WebAppFriends.Midleware
{
    public class ValidateSession
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public ValidateSession(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public bool HasSession()
        {
            UserViewModel user = _contextAccessor.HttpContext.Session.Get<UserViewModel>("User");

            if(user == null)
            {
                return false;
            }

            return true;
        }
    }
}
