using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tests_server_app.Models;
using Tests_server_app.Models.DBModels;
using Tests_server_app.Models.ViewModels;
using Tests_server_app.Services.Authentication;
using Tests_server_app.Services.UsersMapping;

namespace Tests_server_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly TestsDbContext _context;
        private readonly IJWTTokenGenerationService _jWTTokenGenerationService;
        private readonly IUsersMappingService _usersMappingService;

        public AccountController(TestsDbContext testsDbContext, 
                                 IJWTTokenGenerationService generationService,
                                 IUsersMappingService usersMappingService)
        {
            _context = testsDbContext;
            _jWTTokenGenerationService = generationService;
            _usersMappingService = usersMappingService;
        }

        #region Routed methods

        public async Task<string> SignIn([FromBody] UserLoginVM user)
        {
            if (ModelState.IsValid)
            {
                var dbUser = _usersMappingService.GetUser(user);

                if(dbUser != null)
                {
                    return AuthenticateUser(dbUser);
                }
            }

            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            return "";
        }

        public async Task<string> SignUp([FromBody] UserRegistrationVM user)
        {

            if (ModelState.IsValid && !UserExists(user))
            {
                var role = _context.Roles.Single(x => x.Name == "User");

                var dbUser = new User()
                {
                    FirstName = user.FirstName,
                    SecondName = user.SecondName,
                    BirthDate = user.BirthDate,

                    PasswordHash = user.PasswordHash,
                    Login = user.Login,
                    SignedUpWithAccount = SignedUpWith.Application,
                    RoleId = role.RoleId,
                    Role = role
                };

                _context.Users.Add(dbUser);
                _context.SaveChanges();

                return AuthenticateUser(dbUser);
            }

            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            return "";
        }

        public async Task<UserInformationVM> Information()
        {

            // ....loading.....
            // temp code
            var user = _context.Users
                            .Where(x => x.Login == User.Identity.Name)
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

            return _usersMappingService.GetUserInformationVM(user);
        }

        #endregion


        private string AuthenticateUser(User user)
        {
            var token = _jWTTokenGenerationService.GetToken(user);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool UserExists(UserRegistrationVM user)
        {
            return _context.Users.Any(e => 
                        e.Login == user.Login && 
                        e.Email == user.Email);
        }
    }
}