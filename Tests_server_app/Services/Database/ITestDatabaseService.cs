using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests_server_app.Models.ViewModels;

namespace Tests_server_app.Services.Database
{
    public interface ITestDatabaseService
    {
        List<TestVM> GetTests(int from, int count);
        List<TestVM> GetTests();
        List<TestVM> GetTestsByTheme(int from, int count, string theme);
        List<TestVM> GetTestsByTheme(string theme);
        List<TestVM> GetTestsOrderedByLikes(int from, int count);
        List<TestVM> GetTestsOrderedByLikes(int from, int count, string theme);
        List<TestVM> GetTestsOrderedByLikes(string theme);
        List<TestVM> GetTestsOrderedByLikes();

        TestVM GetTest(string title);
        bool AddNewTest(TestVM testVM);
        void LikeTest(string title);
        bool EditTest(EditedTestVM editedTest);
        bool DeleteTest(string title);
    }
}
