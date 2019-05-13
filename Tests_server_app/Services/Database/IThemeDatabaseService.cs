using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests_server_app.Models.ViewModels;

namespace Tests_server_app.Services.Database
{
    public interface IThemeDatabaseService
    {
        List<ThemeVM> GetAllThemes();
        bool AddTheme(string themeName);
        bool DeleteTheme(string themeName);

        bool SetThemesToTest(ThemeVM[] themes, string title);
    }
}
