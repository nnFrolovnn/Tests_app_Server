using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tests_server_app.Models.DBModels
{
    public class Achievement
    {
        [Key]
        public long AchievementId { get; set; }

        public long IconId { get; set; }
        public virtual Icon Icon { get; set; }

        public double Experience { get; set; }

        public long ThemeId { get; set; }
        public virtual Theme Theme { get; set; }

        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public virtual ICollection<UserAchievement> Users { get; set; }

        public Achievement()
        {
            Users = new List<UserAchievement>();
        }
    }
}
