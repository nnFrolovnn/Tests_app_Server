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

        public virtual List<TestTheme> Tests { get; set; }
        public virtual List<Achievement> Achievements { get; set; }
    }
}
