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

        public async Task<UserInformationVM> Info()
        {
            return _databaseService.GetUserInformationVM(HttpContext);           
        }

        #endregion

        private string AuthenticateUser(User user)
        {
            var token = _jWTTokenGenerationService.GetToken(user);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}