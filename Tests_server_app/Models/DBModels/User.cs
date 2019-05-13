using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Tests_server_app.Models.ViewModels;

namespace Tests_server_app.Models.DBModels
{
    public enum SignedUpWith
    {
        Google, Application, Facebook
    }

    public class User
    {
        [Key]
        public long UserId { get; set; }

        public string Login { get; set; }

        public string PasswordHash { get; set; }

        public SignedUpWith SignedUpWithAccount { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public long RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<UserTest> Tests { get; set; }
        public virtual ICollection<UserAchievement> Achievements { get; set; }

        public User()
        {
            Tests = new List<UserTest>();
            Achievements = new List<UserAchievement>();
        }

        public User(UserRegistrationVM user, Role role):this()
        {
            Login = user.Login;
            PasswordHash = user.PasswordHash;
            SignedUpWithAccount = SignedUpWith.Application;
            FirstName = user.FirstName;
            SecondName = user.SecondName;
            Email = user.Email;
            RoleId = role.RoleId;
            Role = role;

            //parse date
            var dateParts = user.BirthDate.Split('.', '-');
            if (dateParts.Count() == 3)
            {
                var dateInts = new List<int>();

                foreach(var s in dateParts)
                {
                    if(int.TryParse(s, out int res))
                    {
                        dateInts.Add(res);
                    }
                    else
                    {
                        BirthDate = DateTime.Today;
                        return;
                    }
                }

                BirthDate = new DateTime(dateInts[2], dateInts[1], dateInts[0]);            
            }
        }

        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }

            if(obj == this)
            {
                return true;
            }

            User user = obj as User;

            if (obj == null)
            {
                return false;
            }

            bool currEqual = FirstName == user.FirstName && SecondName == user.SecondName &&
                             Login == user.Login && RoleId == user.RoleId && PasswordHash == user.PasswordHash &&
                             BirthDate == user.BirthDate && Email == user.Email &&
                             SignedUpWithAccount == user.SignedUpWithAccount;

            return currEqual && base.Equals(obj);
        }
    }
}
