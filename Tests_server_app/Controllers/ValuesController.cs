using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Tests_server_app.Models;
using Tests_server_app.Models.DBModels;
using Tests_server_app.Models.ViewModels;
using Tests_server_app.Services.Authentication;

namespace Tests_server_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly TestsDbContext db;
        private readonly IJWTTokenGenerationService jWTTokenGenerationService;


        [HttpGet("{id:int}")]
        public ActionResult<Role> Get(int id)
        {
            var r = db.Roles.First();
            var u = r.Users;
            return r;
        }

        [HttpGet("/user")]
        public ActionResult<string> Get()
        {
            return User.Identity.Name;
        }
        [HttpGet]
        [Route("/login")]
        public string Login()
        {
            UserLoginVM user = new UserLoginVM()
            {
                PasswordHash = "fsdfsdfsdf",
                Login = "vlad"
            };

            User dbUser = db.Users.FirstOrDefault(x => 
                             x.Login == user.Login && x.PasswordHash == user.PasswordHash);

            if (dbUser != null)
            {
                dbUser.Role = db.Roles.Single(
                        x => x.RoleId == dbUser.RoleId);

                var token = jWTTokenGenerationService.GetToken(dbUser);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            return null;
        }

        [HttpGet]
        [Authorize]
        [Route("/acc")]
        public string Acc()
        {
            return $"you are authorized {User.Identity.Name}, {User.Identity.IsAuthenticated}";
        }

        public ValuesController(TestsDbContext testsDbContext, IJWTTokenGenerationService generationService)
        {
            db = testsDbContext;
            jWTTokenGenerationService = generationService;
        }
    }
}
