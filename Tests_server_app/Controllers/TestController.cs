using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tests_server_app.Models.ViewModels;
using Tests_server_app.Services.DatabaseServ;

namespace Tests_server_app.Controllers
{
    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;
        const byte showCount = 6;

        public TestController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet]       
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

        [HttpGet]
        public async Task<ActionResult<TestVM>> GetTest([FromQuery]string title)
        {
            var test = _databaseService.GetTest(title);

            if(test != null)
            {
                return test;
            }

            return null;
        }

        [HttpGet]
        public async Task<ActionResult<List<TestVM>>> GetAll()
        {
            var test = _databaseService.GetTests();

            if (test != null)
            {
                return test;
            }

            return null;
        }

        [HttpGet]
        public async Task<ActionResult<List<TestVM>>> GetAllByLikes()
        {
            var test = _databaseService.GetTestsOrderedByLikes();
            return null;
        }

        [HttpPut]
        [Authorize(Policy = "Admin")]
        public async Task<bool> Edit([FromBody] EditedTestVM editedTest)
        {
            if(ModelState.IsValid)
            {
                return _databaseService.EditTest(editedTest);
            }

            return false;
        }
        [HttpPost]
        [Authorize]
        public async Task<bool> Add([FromBody] TestVM testVM)
        {
            if(ModelState.IsValid)
            {
                return _databaseService.AddNewTest(testVM);
            }

            return false;
        }

        [HttpPut]
        [Authorize]
        public async void LikeTest(string title)
        {
            if (title != null)
            {
                _databaseService.LikeTest(title);
            }
        }

        [HttpDelete]
        [Authorize(Policy = "Admin")]
        public async Task<bool> Delete([FromQuery] string title)
        {
            if(title != null)
            {
                return _databaseService.DeleteTest(title);
            }
            return false;
        }
    }
}