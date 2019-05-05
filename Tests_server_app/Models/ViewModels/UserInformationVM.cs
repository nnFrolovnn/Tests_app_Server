using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests_server_app.Models.DBModels;

namespace Tests_server_app.Models.ViewModels
{
    public class UserInformationVM
    {
        public string Login { get; set; }
        public string SignedUpWithAccount { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }

        public List<TestVM> Tests { get; set; }
        public List<AchievementVM> Achievements { get; set; }

        public UserInformationVM(User user)
        {
            Login = user.Login;
            SignedUpWithAccount = user.SignedUpWithAccount.ToString();
            FirstName = user.FirstName;
            SecondName = user.SecondName;
            Email = user.Email;
            BirthDate = user.BirthDate;

            Tests = new List<TestVM>();
            foreach(var test in user.Tests)
            {
                Tests.Add(new TestVM(test));
            }

            Achievements = new List<AchievementVM>();
            foreach(var ach in user.Achievements)
            {
                Achievements.Add(new AchievementVM(ach));
            }
        }    
    }
}
