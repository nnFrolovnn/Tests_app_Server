using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Tests_server_app.Models.DBModels;

namespace Tests_server_app.Models.ViewModels
{
    public class UserLoginVM
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}
