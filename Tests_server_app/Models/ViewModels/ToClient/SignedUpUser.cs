using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tests_server_app.Models.ViewModels
{
    public class SignedUpUser
    {
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }
    }
}
