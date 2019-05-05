using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests_server_app.Models;
using Tests_server_app.Models.DBModels;
using Tests_server_app.Models.ViewModels;

namespace Tests_server_app.Services.UsersMapping
{
    public class UsersMappingService : IUsersMappingService
    {
        private readonly TestsDbContext _context;

        public UsersMappingService(TestsDbContext context)
        {
            _context = context;
        }

        public User GetUser(UserLoginVM userLoginVM)
        {
            if (userLoginVM != null)
            {
                return _context.Users.First(x =>
                        x.Login == userLoginVM.Login &&
                        x.PasswordHash == userLoginVM.PasswordHash);
            }
            return null;
        }

        public UserLoginVM GetUserLoginVM(User user)
        {
            if (user != null)
            {
                return new UserLoginVM()
                {
                    PasswordHash = user.PasswordHash,
                    Login = user.Login
                };
            }
            return null;
        }

        public UserInformationVM GetUserInformationVM(User user)
        {
            if(user != null)
            {
                return new UserInformationVM(user);
            }
            return null;
        }

        public UserLoginVM GetLoginVM(User user)
        {
            if(user != null)
            {
                return new UserLoginVM()
                {
                    Login = user.Login,
                    PasswordHash = user.PasswordHash
                };
            }
            return null;
        }

        public UserInformationVM GetUserInformationVM(UserLoginVM user)
        {
            if (user != null)
            {
                User dbUser = GetUser(user);
                return GetUserInformationVM(dbUser);
            }
            return null;
        }
    }
}
