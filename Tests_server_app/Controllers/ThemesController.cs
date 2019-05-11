using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tests_server_app.Models.ViewModels;
using Tests_server_app.Services.DatabaseServ;

namespace Tests_server_app.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ThemesController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;
        const byte showCount = 6;

        public ThemesController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ThemeVM>>> GetAll()
        {
            return _databaseService.GetAllThemes();
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> Delete([FromBody] string themeName)
        {
            return _databaseService.DeleteTheme(themeName);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Add([FromBody] string themeName)
        {
            return _databaseService.AddTheme(themeName);
        }
    }
}