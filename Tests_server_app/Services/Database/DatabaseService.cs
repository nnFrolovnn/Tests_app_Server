using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tests_server_app.Models;
using Tests_server_app.Models.DBModels;
using Tests_server_app.Models.ViewModels;

namespace Tests_server_app.Services.DatabaseServ
{
    public class DatabaseService : IDatabaseService
    {
        private readonly TestsDbContext _context;

        public DatabaseService(TestsDbContext context)
        {
            _context = context;
        }

        #region User

        public UserLoginVM GetLoginVM(User user)
        {
            if (user != null)
            {
                return new UserLoginVM()
                {
                    Login = user.Login,
                    PasswordHash = user.PasswordHash
                };
            }
            return null;
        } 

        public User GetSignedUpUser(UserLoginVM userLoginVM)
        {
            if (userLoginVM != null)
            {
                return _context.Users.First(x =>
                            x.Login == userLoginVM.Login &&
                            x.PasswordHash == userLoginVM.PasswordHash);
            }

            return null;
        }

        public User SignUpUser(UserRegistrationVM userRegistrationVM)
        {
            var userExists = _context.Users.Any(x =>
                                x.Login == userRegistrationVM.Login &&
                                x.Email == userRegistrationVM.Email);

            if (!userExists)
            {
                Role role = _context.Roles.First(x =>
                                x.Name == "User" ||
                                x.Name == "user");

                if (role != null)
                {
                    var user = new User(userRegistrationVM, role);

                    _context.Users.Add(user);
                    _context.SaveChanges();

                    return user;
                }
            }

            return null;
        }

        public UserInformationVM GetUserInformationVM(string name)
        {
            var user = _context.Users.First(x =>
                                x.Login == name);

            if (user != null)
            {
                return GetUserInformationVM(user);
            }

            return null;
        }

        public List<TestVM> GetUserTests(string name)
        {
            var userTests = _context.Users
                            .Include(x => x.Tests)
                            .Include(x => x.Tests.Select(t => t.Test))
                            .First(x =>
                                x.Login == name);

            var tests = new List<TestVM>();
            foreach(var test in userTests.Tests)
            {
                tests.Add(new TestVM(test));
            }

            return tests;
        }

        public UserInformationVM GetUserInformationVM(UserLoginVM user)
        {
            if (user != null)
            {
                User dbUser = LoadFieldsForUserInformation(
                                    GetSignedUpUser(user));

                return GetUserInformationVM(dbUser);
            }
            return null;
        }

        public UserInformationVM GetUserInformationVM(User user)
        {
            if (user != null)
            {
                return new UserInformationVM(user);
            }
            return null;
        }

        #endregion

        #region Test

        public List<TestVM> GetTests(int from, int count)
        {
            var tests = _context.Tests
                            .Skip(from)
                            .Take(count);

            tests = LoadTestsFields(tests);

            return tests == null ? null : TransformToVM(tests.ToList());
        }

        public List<TestVM> GetTestsByTheme(int from, int count, string theme)
        {
            var tests = _context.Tests
                            .Where(t => t.Themes.Any(th => th.Theme.ThemeName == theme))
                            .Skip(from)
                            .Take(count);

            tests = LoadTestsFields(tests);

            return tests == null ? null : TransformToVM(tests.ToList());
        }

        public List<TestVM> GetTestsOrderedByLikes(int from, int count)
        {
            var tests = _context.Tests
                            .OrderBy(t => t.LikesCount)
                            .Skip(from)
                            .Take(count);

            tests = LoadTestsFields(tests);

            return tests == null ? null : TransformToVM(tests.ToList());
        }

        public List<TestVM> GetTestsOrderedByLikes(int from, int count, string theme)
        {
            var tests = _context.Tests
                            .Where(t => t.Themes.Any(th => th.Theme.ThemeName == theme))
                            .OrderBy(t => t.LikesCount)
                            .Skip(from)
                            .Take(count);

            tests = LoadTestsFields(tests);
            return tests == null ? null : TransformToVM(tests.ToList());
        }

        public List<TestVM> GetTests()
        {
            var tests = _context.Tests
                                .AsQueryable();

            tests = LoadTestsFields(tests);

            return tests == null? null : TransformToVM(tests.ToList());
        }

        public TestVM GetTest(string title)
        {
            var test = _context.Tests
                            .Where(t => t.Title == title)
                            .Take(1);

            test = LoadTestsFields(test);

            if (test != null && test.Count() > 0)
            {
                return new TestVM(test.First());
            }

            return null;
        }

        public bool AddNewTest(TestVM testVM)
        {
            if (testVM != null)
            {
                var test = new Test(testVM);

                foreach (var theme in testVM.Themes)
                {
                    var dbTheme = _context.Themes.First(x => x.ThemeName == theme.ThemeName);
                    if (dbTheme != null)
                    {
                        test.Themes.Add(new TestTheme(test, dbTheme));
                    }
                    else
                    {
                        test.Themes.Add(new TestTheme(
                            test,
                            new Theme(theme.ThemeName)));
                    }
                }

                _context.Tests.Add(test);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public void LikeTest(string title)
        {
            _context.Tests
                    .First(x => x.Title == title).LikesCount++;

            _context.SaveChanges();
        }

        public List<TestVM> GetTestsByTheme(string theme)
        {
            var tests = _context.Tests
                            .Where(t => t.Themes.Any(th => th.Theme.ThemeName == theme))
                            .AsQueryable();

            tests = LoadTestsFields(tests);

            return tests == null ? null : TransformToVM(tests.ToList());
        }

        public List<TestVM> GetTestsOrderedByLikes(string theme)
        {
            var tests = _context.Tests
                            .Where(t => t.Themes.Any(th => th.Theme.ThemeName == theme))
                            .OrderBy(t => t.LikesCount).AsQueryable();

            tests = LoadTestsFields(tests);
            return tests == null ? null : TransformToVM(tests.ToList());
        }

        #endregion

        #region Theme

        public List<ThemeVM> GetAllThemes()
        {
            var themesDb = _context.Themes?.ToList();

            var themesVM = new List<ThemeVM>();

            foreach(var theme in themesDb)
            {
                themesVM.Add(new ThemeVM(theme));
            }

            return themesVM;
        }

        public bool AddTheme(string themeName)
        {
            var theme = _context.Themes
                                .First(x => x.ThemeName == themeName);
            if(theme == null)
            {
                _context.Themes.Add(new Theme(themeName));
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool DeleteTheme(string themeName)
        {
            var theme = _context.Themes
                                .First(x => x.ThemeName == themeName);
            if (theme == null)
            {
                _context.Themes.Remove(theme);
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        #endregion

        private User LoadFieldsForUserInformation(User user)
        {

            var loadedUser = _context.Users
                            .Where(x => x.Equals(user))
                            .Include(x => x.Achievements)
                            .Include(x => x.Achievements.Select(a => a.Achievement))
                            .Include(x => x.Achievements.Select(a => a.Achievement.Icon))
                            .Include(x => x.Achievements.Select(a => a.Achievement.Theme))
                            .Include(x => x.Tests)
                            .Include(x => x.Tests.Select(t => t.Test))
                            .Include(x => x.Tests.Select(t => t.Test.Questions))
                            .Include(x => x.Tests.Select(t => t.Test.Questions.Select(q => q.Question)))
                            .Include(x => x.Tests.Select(t => t.Test.Questions.Select(q => q.Question.Answers)))
                            .Include(x => x.Tests.Select(t => t.Test.Questions.Select(q => q.Question.Answers.Select(a => a.Answer))))
                            .Include(x => x.Tests.Select(t => t.Test.Themes))
                            .Include(x => x.Tests.Select(t => t.Test.Themes.Select(s => s.Theme)))
                            .FirstOrDefault();

            return loadedUser;
        }

        private IQueryable<Test> LoadTestsFields(IQueryable<Test> tests)
        {
            if (tests == null)
            {
                return null;
            }

            tests.Include(t => t.Questions)
                 .Include(t => t.Questions.Select(q => q.Question))
                 .Include(t => t.Questions.Select(q => q.Question.Answers))
                 .Include(t => t.Questions.Select(q => q.Question.Answers.Select(a => a.Answer)))
                 .Include(t => t.Themes)
                 .Include(t => t.Themes.Select(th => th.Theme));

            return tests;
        }

        private List<TestVM> TransformToVM(List<Test> tests)
        {
            if (tests == null)
            {
                return null;
            }

            var testvms = new List<TestVM>(tests.Count);

            foreach (var t in tests)
            {
                testvms.Add(new TestVM(t));
            }

            return testvms;
        }
    }
}
