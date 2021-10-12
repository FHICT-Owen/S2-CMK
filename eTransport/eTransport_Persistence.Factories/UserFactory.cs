using eTransport_Persistence.Interfaces;
using eTransport_Utility;
using Microsoft.AspNetCore.Identity;

namespace eTransport_Persistence.Factories
{
    public class UserFactory
    {
        public static IUserDal CreateUserDal()
        {
            return new UserDal();
        }
    }
}