using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests_server_app.Models.DBModels;

namespace Tests_server_app.Models.ViewModels
{
    public class RoleVM
    {
        public string Name { get; set; }

        public RoleVM(string name)
        {
            Name = name;
        }

        public RoleVM(Role role)
        {
            Name = role.Name;
        }
    }
}
