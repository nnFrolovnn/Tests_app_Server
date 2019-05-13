using System;
using Tests_server_app.Models.DBModels;

namespace Tests_server_app.Models.ViewModels
{
    public class AchievementVM
    {
        public byte[] Icon { get; set; }
        public string AchDate { get; set; }
        public string ThemeName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public AchievementVM(UserAchievement ach)
        {
            Title = ach.Achievement?.Title;
            Description = ach.Achievement?.Description;
            AchDate = ach.Date.ToShortDateString();

            ThemeName = ach.Achievement?.Theme?.ThemeName;
            Icon = ach.Achievement?.Icon?.Data;
        }
    }
}