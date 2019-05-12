using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests_server_app.Models.DBModels;
using Tests_server_app.Models.ViewModels;

namespace Tests_server_app.Services.Database
{
    public interface IUserDatabaseService
    {
        User GetSignedUpUser(UserLoginVM userLoginVM);
        User SignUpUser(UserRegistrationVM userRegistrationVM);

        UserLoginVM GetLoginVM(User user);
        UserInformationVM GetUserInformationVM(User user);
        UserInformationVM GetUserInformationVM(UserLoginVM user);
        UserInformationVM GetUserInformationVM(string name);
        List<TestVM> GetUserTests(string name);

        bool AddPassedTestToUser(string title, string userName, int countRightAnswers);
    }
}
