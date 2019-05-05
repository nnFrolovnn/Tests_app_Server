using Tests_server_app.Models.DBModels;

namespace Tests_server_app.Models.ViewModels
{
    public class ThemeVM
    {
        public string ThemeName { get; set; }

        public ThemeVM(TestTheme t)
        {
            ThemeName = t.Theme?.ThemeName;
        }
    }
}