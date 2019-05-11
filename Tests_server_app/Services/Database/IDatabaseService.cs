﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests_server_app.Models.DBModels;
using Tests_server_app.Models.ViewModels;

namespace Tests_server_app.Services.DatabaseServ
{
    public interface IDatabaseService
    {
        User GetSignedUpUser(UserLoginVM userLoginVM);
        User SignUpUser(UserRegistrationVM userRegistrationVM);

        UserLoginVM GetLoginVM(User user);
        UserInformationVM GetUserInformationVM(User user);
        UserInformationVM GetUserInformationVM(UserLoginVM user);
        UserInformationVM GetUserInformationVM(string name);
        List<TestVM> GetUserTests(string name);


        List<TestVM> GetTests(int from, int count);
        List<TestVM> GetTests();
        List<TestVM> GetTestsByTheme(int from, int count, string theme);
        List<TestVM> GetTestsByTheme(string theme);
        List<TestVM> GetTestsOrderedByLikes(int from, int count);
        List<TestVM> GetTestsOrderedByLikes(int from, int count, string theme);
        List<TestVM> GetTestsOrderedByLikes(string theme);

        TestVM GetTest(string title);
        bool AddNewTest(TestVM testVM);
        void LikeTest(string title);

        List<ThemeVM> GetAllThemes();
        bool AddTheme(string themeName);
        bool DeleteTheme(string themeName);
    }
}
