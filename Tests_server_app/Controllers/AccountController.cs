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
        [Authorize]
        public async Task<string> SignIn([FromBody] UserLoginVM user)
        {
            if (ModelState.IsValid)
            {
                var dbUser = _databaseService.GetSignedUpUser(user);

                if(dbUser != null)
                {
                    return AuthenticateUser(dbUser);
                }
            }

            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            return "";
        }

        [HttpPost]
        [Authorize]
        public async Task<string> SignUp([FromBody] UserRegistrationVM user)
        {
            if (ModelState.IsValid)
            {
                var dbUser = _databaseService.SignUpUser(user);

                if (dbUser != null)
                {
                    return AuthenticateUser(dbUser);
                }
            }

            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            return "";
        }

        [HttpGet]
        [Authorize]
        public async Task<UserInformationVM> Info()
        {
            return _databaseService.GetUserInformationVM(HttpContext.User.Identity.Name);           
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<TestVM>>> GetUserTests()
        {
            return _databaseService.GetUserTests(HttpContext.User.Identity.Name);
        }

        public async Task<ActionResult<bool>> TestPassed([FromBody] TestVM testVM)
        {
            throw new NotImplementedException();
        }

        #endregion

        private string AuthenticateUser(User user)
        {
            var token = _jWTTokenGenerationService.GetToken(user);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}