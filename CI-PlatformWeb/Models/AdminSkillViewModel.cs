using CI_Entity.Models;
using System.ComponentModel.DataAnnotations;

namespace CI_PlatformWeb.Models
{
    public class AdminSkillViewModel
    {
        public List<Skill> skill { get; set; }
        [Required(ErrorMessage = "skill Name is a Required field.")]
        public string skillName { get; set; }
        public long skillId { get; set; }
    }
}
