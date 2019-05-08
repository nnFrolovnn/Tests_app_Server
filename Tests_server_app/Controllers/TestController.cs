using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tests_server_app.Models.ViewModels;
using Tests_server_app.Services.DatabaseServ;

namespace Tests_server_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;
        const byte showCount = 6;

        public TestController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<ActionResult<List<TestVM>>> GetPage([FromQuery]int page, [FromQuery] string likes, [FromQuery] string theme)
        {
            if (likes != null && theme != null)
            {
                return _databaseService.GetTestsOrderedByLikes(page * showCount, showCount, theme);
            }
            if (likes != null)
            {
                return _databaseService.GetTestsOrderedByLikes(page * showCount, showCount);
            }
            if (theme != null)
            {
                return _databaseService.GetTestsByTheme(page * showCount, showCount, theme);
            }
            return null;
        }

        public async Task<ActionResult<TestVM>> GetTest([FromQuery]string title)
        {
            var test = _databaseService.GetTest(title);

            if(test != null)
            {
                return test;
            }

            return null;
        }

        public async Task<bool> AddTest([FromBody] TestVM testVM)
        {
            if(ModelState.IsValid)
            {
                return _databaseService.AddNewTest(testVM);
            }

            return false;
        }

        public async void LikeTest(string title)
        {
            if (title != null)
            {
                _databaseService.LikeTest(title);
            }
        }
    }
}