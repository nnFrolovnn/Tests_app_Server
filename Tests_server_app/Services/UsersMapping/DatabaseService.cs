using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Tests_server_app.Models;
using Tests_server_app.Models.DBModels;
using Tests_server_app.Models.ViewModels;

namespace Tests_server_app.Services.UsersMapping
{
    public class DatabaseService : IDatabaseService
    {
        private readonly TestsDbContext _context;

        public DatabaseService(TestsDbContext context)
        {
            _context = context;
        }



        public UserLoginVM GetLoginVM(User user)
        {
            if (user != null)
            {
                return new UserLoginVM()
                {
                    Login = user.Login,
                    PasswordHash = user.PasswordHash
                };
            }
            return null;
        }

        public User GetSignedUpUser(UserLoginVM userLoginVM)
        {
            if (userLoginVM != null)
            {
                return _context.Users.First(x =>
                            x.Login == userLoginVM.Login &&
                            x.PasswordHash == userLoginVM.PasswordHash);
            }

            return null;
        }

        public User SignUpUser(UserRegistrationVM userRegistrationVM)
        {
            var userExists = _context.Users.Any(x => 
                                x.Login == userRegistrationVM.Login &&
                                x.Email == userRegistrationVM.Email);

            if (!userExists)
            {
                Role role = _context.Roles.First(x => 
                                x.Name == "User" || 
                                x.Name == "user");

                if(role != null)
                {
                    var user = new User(userRegistrationVM, role);

                    _context.Users.Add(user);
                    _context.SaveChanges();

                    return user;
                }
            }

            return null;
        }

        public UserInformationVM GetUserInformationVM(HttpContext context)
        {
            var user = _context.Users.First(x =>
                                x.Login == context.User.Identity.Name);

            if(user != null)
            {
                return GetUserInformationVM(user);
            }

            return null;
        }

        public UserInformationVM GetUserInformationVM(UserLoginVM user)
        {
            if (user != null)
            {
                User dbUser = LoadFieldsForUserInformation(
                                    GetSignedUpUser(user));

                return GetUserInformationVM(dbUser);
            }
            return null;
        }

        public UserInformationVM GetUserInformationVM(User user)
        {
            if (user != null)
            {
                return new UserInformationVM(user);
            }
            return null;
        }



        private User LoadFieldsForUserInformation(User user)
        {

            var loadedUser = _context.Users
                            .Where(x => x.Equals(user))
                            .Include(x => x.Achievements)
                            .Include(x => x.Achievements.Select(a => a.Achievement))
                            .Include(x => x.Achievements.Select(a => a.Achievement.Icon))
                            .Include(x => x.Achievements.Select(a => a.Achievement.Theme))
                            .Include(x => x.Tests)
                            .Include(x => x.Tests.Select(t => t.Test))
                            .Include(x => x.Tests.Select(t => t.Test.Questions))
                            .Include(x => x.Tests.Select(t => t.Test.Questions.Select(q => q.Question)))
                            .Include(x => x.Tests.Select(t => t.Test.Questions.Select(q => q.Question.Answers)))
                            .Include(x => x.Tests.Select(t => t.Test.Questions.Select(q => q.Question.Answers.Select(a => a.Answer))))
                            .Include(x => x.Tests.Select(t => t.Test.Themes))
                            .Include(x => x.Tests.Select(t => t.Test.Themes.Select(s => s.Theme)))
                            .FirstOrDefault();

            return loadedUser;
        }
    }
}
