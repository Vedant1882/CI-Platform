using CI_Entity.Models;

namespace CI_PlatformWeb.Models
{
    public class AdminSkillViewModel
    {
        public List<Skill> skill { get; set; }
        public string skillName { get; set; }
        public long skillId { get; set; }
    }
}
