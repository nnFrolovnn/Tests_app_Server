using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tests_server_app.Models.ViewModels
{
    public class EditedTestVM
    {
        public string OldName { get; set; }
        public TestVM NewTest { get; set; }
    }
}
