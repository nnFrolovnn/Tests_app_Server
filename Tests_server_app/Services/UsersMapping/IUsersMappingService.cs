using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests_server_app.Models.DBModels;
using Tests_server_app.Models.ViewModels;

namespace Tests_server_app.Services.UsersMapping
{
    public interface IUsersMappingService
    {
        User GetUser(UserLoginVM userLoginVM);

        UserLoginVM GetLoginVM(User user);
        UserInformationVM GetUserInformationVM(User user);
        UserInformationVM GetUserInformationVM(UserLoginVM user);
    }
}
