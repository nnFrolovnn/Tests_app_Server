using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tests_server_app.Models;
using Tests_server_app.Models.DBModels;
using Tests_server_app.Models.ViewModels;
using Tests_server_app.Services.Authentication;

namespace Tests_server_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TestsDbContext _context;
        private readonly IJWTTokenGenerationService jWTTokenGenerationService;

        public UsersController(TestsDbContext testsDbContext, IJWTTokenGenerationService generationService)
        {
            _context = testsDbContext;
            jWTTokenGenerationService = generationService;
        }

        #region Route methods
        public async Task<string> LogIn([FromBody] UserLoginVM user)
        {
            var resCode = StatusCodes.Status400BadRequest;

            if (ModelState.IsValid)
            {
                
            }

            HttpContext.Response.StatusCode = resCode;
            return "";
        }
        #endregion






        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}