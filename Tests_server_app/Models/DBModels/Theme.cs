using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tests_server_app.Models.DBModels
{
    public class Theme
    {
        [Key]
        public long ThemeId { get; set; }

        public string ThemeName { get; set; }

        public virtual ICollection<TestTheme> Tests { get; set; }
        public virtual ICollection<Achievement> Achievements { get; set; }

        public Theme()
        {
            Tests = new List<TestTheme>();
            Achievements = new List<Achievement>();
        }
    }
}
