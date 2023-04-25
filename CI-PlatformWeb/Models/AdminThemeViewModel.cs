using CI_Entity.Models;
using System.ComponentModel.DataAnnotations;

namespace CI_PlatformWeb.Models
{
    public class AdminThemeViewModel
    {
        public List<MissionTheme> missionThemes { get; set; }
        [Required(ErrorMessage = "Theme Name is a Required field.")]
        public string themeName { get; set; }
        public long themeId { get; set; }
    }
}
