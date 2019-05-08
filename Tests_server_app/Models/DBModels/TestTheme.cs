using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tests_server_app.Models.DBModels
{
    public class TestTheme
    {
        public TestTheme(Test test, Theme dbTheme)
        {
            Test = test;
            Theme = dbTheme;
        }

        public TestTheme()
        {

        }

        public long TestId { get; set; }
        public virtual Test Test { get; set; }

        public long ThemeId { get; set; }
        public virtual Theme Theme { get; set; }
    }
}
