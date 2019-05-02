using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public ActionResult<IEnumerable<User>> Index()
        {
            return db.Users.ToList();
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<IEnumerable<Achievement>> Get(int id)
        {
            return db.Achievements.ToList();
        }

        [HttpGet]
        public string Login()
        {
            UserVM user = new UserVM()
            {
                PasswordHash = "fsdfsdfsdf",
                Login = "vlad"
            };

            User dbUser = db.Users.FirstOrDefault(x => 
                             x.Login == user.Login && x.PasswordHash == user.PasswordHash);

            if (dbUser != null)
            {
                var token = jWTTokenGenerationService.GetToken(dbUser);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            return null;
        }

        [HttpGet]
        [Authorize]
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
