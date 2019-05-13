using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tests_server_app.Models.DBModels;
using Tests_server_app.Models.ViewModels;
using Tests_server_app.Services.Authentication;
using Tests_server_app.Services.DatabaseServ;

namespace Tests_server_app.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IJWTTokenGenerationService _jWTTokenGenerationService;
        private readonly IDatabaseService _databaseService;

        public AccountController(
                                 IJWTTokenGenerationService generationService,
                                 IDatabaseService databaseService)
        {
            _jWTTokenGenerationService = generationService;
            _databaseService = databaseService;
        }

        #region Routed methods

        [HttpPost]
        public async Task<SignedUpUser> SignIn([FromBody] UserLoginVM user)
        {
            if (ModelState.IsValid)
            {
                var dbUser = _databaseService.GetSignedUpUser(user);

                if(dbUser != null)
                {
                    var token = AuthenticateUser(dbUser);
                    var userInfo = _databaseService.GetUserInformationVM(dbUser.Login);
                    return new SignedUpUser()
                    {
                        Token = token,
                        FirstName = dbUser.FirstName,
                        Role = dbUser.Role.Name,
                        Login = dbUser.Login
                    };
                }
            }

            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            return null;
        }

        [HttpPost]
        public async Task<SignedUpUser> SignUp([FromBody] UserRegistrationVM user)
        {
            if (ModelState.IsValid)
            {
                var dbUser = _databaseService.SignUpUser(user);

                if (dbUser != null)
                {
                    var token = AuthenticateUser(dbUser);
                    var userInfo = _databaseService.GetUserInformationVM(dbUser.Login);
                    return new SignedUpUser()
                    {
                        Token = token,
                        FirstName = dbUser.FirstName,
                        Role = dbUser.Role.Name,
                        Login = dbUser.Login
                    };

                }
            }

            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            return null;
        }

        [HttpGet]
        [Authorize]
        public async Task<UserInformationVM> Info()
        {
            if (HttpContext.User?.Identity?.Name != null)
            {
                return _databaseService.GetUserInformationVM(HttpContext.User.Identity.Name);
            }
            return null;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<TestVM>>> GetUserTests()
        {
            return _databaseService.GetUserTests(HttpContext.User.Identity.Name);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<bool>> TestPassed([FromQuery] string title, [FromQuery] int countRightAnswers, [FromQuery] int questionsNumber)
        {
            return _databaseService.AddPassedTestToUser(title, HttpContext.User.Identity.Name, countRightAnswers, questionsNumber);
        }

        public async Task<SignedUpUser> Edit([FromBody] EditedUserVM editedUser)
        {
            if(ModelState.IsValid)
            {
                var user = _databaseService.EditUser(HttpContext.User.Identity.Name, editedUser);

                if(user != null)
                {
                    return new SignedUpUser()
                    {
                        FirstName = user.FirstName,
                        Login = user.Login,
                        Role = user.Role?.Name,
                        Token = ""
                    };
                }              
            }

            return null;
        }

        #endregion

        private string AuthenticateUser(User user)
        {
            var token = _jWTTokenGenerationService.GetToken(user);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}