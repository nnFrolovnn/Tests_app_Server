using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tests_server_app.Models.ViewModels
{
    public class UserInformationVM
    {
        public string Login { get; set; }

        public string PasswordHash { get; set; }

        public string SignedUpWithAccount { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Email { get; set; }

        public long RoleId { get; set; }

        public List<UserTestVM> Tests { get; set; }
        public List<UserAchievementVM> Achievements { get; set; }
    }
}
