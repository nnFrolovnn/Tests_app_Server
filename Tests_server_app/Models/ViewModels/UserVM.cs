using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests_server_app.Models.DBModels;

namespace Tests_server_app.Models.ViewModels
{
    public class UserVM
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
    }
}
