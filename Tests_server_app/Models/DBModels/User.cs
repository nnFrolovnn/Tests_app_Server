using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
