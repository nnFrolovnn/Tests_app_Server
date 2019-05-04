using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests_server_app.Models;
using Tests_server_app.Models.DBModels;
using Tests_server_app.Models.ViewModels;

namespace Tests_server_app.Services.UsersMapping
{
    public class UsersMappingService
    {
        private readonly TestsDbContext _context;

        public UsersMappingService(TestsDbContext context)
        {
            _context = context;
        }

        public User GetUser(UserLoginVM userLoginVM)
        {
            return _context.Users.First(x => 
                    x.Login == userLoginVM.Login && 
                    x.PasswordHash == userLoginVM.PasswordHash);
        }

        public UserLoginVM GetUserLoginVM(User user)
        {
            return new UserLoginVM()
            {
                PasswordHash = user.PasswordHash,
                Login = user.Login
            };
        }

        public UserInformationVM GetUserInformationVM(User user)
        {
            return null;
        }
    }
}
