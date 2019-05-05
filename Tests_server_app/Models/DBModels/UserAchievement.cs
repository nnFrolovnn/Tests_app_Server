using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tests_server_app.Models.DBModels
{
    public class UserAchievement
    {
        public long UserId { get; set; }
        public virtual User User { get; set; }

        public long AchievementId { get; set; }
        public virtual Achievement Achievement { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
