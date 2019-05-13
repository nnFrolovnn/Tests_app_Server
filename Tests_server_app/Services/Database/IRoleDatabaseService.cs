using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests_server_app.Models.ViewModels;

namespace Tests_server_app.Services.Database
{
    public interface IRoleDatabaseService
    {
        bool SetRoleToUser(string login, string roleName);
        List<RoleVM> GetAllRoles();
        bool AddRole(string roleName);
    }
}
