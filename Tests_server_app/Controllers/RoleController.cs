using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tests_server_app.Models.ViewModels;
using Tests_server_app.Services.DatabaseServ;

namespace Tests_server_app.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;

        public RoleController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<List<RoleVM>> GetAll()
        {
            return _databaseService.GetAllRoles();
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<bool> SetToUser([FromQuery] string login, [FromQuery] string role)
        {
            if(login != null && role != null)
            {
                return _databaseService.SetRoleToUser(login, role);
            }

            return false;
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<bool> Add([FromQuery] string role)
        {
            if (role != null)
            {
                return _databaseService.AddRole(role);
            }

            return false;
        }
    }
}