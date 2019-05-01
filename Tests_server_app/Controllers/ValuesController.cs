using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tests_server_app.Models;
using Tests_server_app.Models.DBModels;

namespace Tests_server_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        TestsDbContext db;
        // GET api/user
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return db.Users.ToList();
        }

        // GET api/user/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Achievement>> Get(int id)
        {
            db.Roles.Add(new Role()
            {
                RoleName = RolesNames.Boss
            });
            db.SaveChanges();
            db.Users.Add(new User()
            {
                FirstName = "Vlad",
                SecondName = "Frolov",
                Email = "vlad@mail.ru",
                Login = "vlad",
                PasswordHash = "fsdfsdfsdf",
                SignedUpWithAccount = SignedUpWith.Application,
                BirthDate = DateTime.Now.Date,
                RoleId = 1
            });
            db.SaveChanges();
            return db.Achievements.ToList();
        }

        // POST api/user
        [HttpPost]
        public void Post([FromBody] User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        public ValuesController(TestsDbContext testsDbContext)
        {
            db = testsDbContext;
        }
    }
}
