using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tests_server_app.Models.DBModels
{
    public class TestTheme
    {
        public long TestId { get; set; }
        public Test Test { get; set; }

        public long ThemeId { get; set; }
        public Theme Theme { get; set; }
    }
}
