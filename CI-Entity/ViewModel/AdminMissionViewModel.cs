using CI_Entity.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Entity.ViewModel
{
    public class AdminMissionViewModel
    {
        public List<Mission> missions { get; set; }

        public long missionId { get; set; }
        public long themeId { get; set; }
        public long cityId { get; set; }
        public long countryId { get; set; }
        public string title { get; set; }
        public string shortdescription { get; set; }
        public string goalObjectiveText { get; set; }
        public string goalValue { get; set; }
        public string description { get; set; }
        public string organizationName { get; set; }
        public string selectedSkills { get; set; }
        public string organizationDetail{ get; set; }

        public List <Country> countries { get; set; }
        public List<City> cities { get; set; }
        public List<MissionTheme> themes { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public DateTime deadline{ get; set; }
        public string missionType { get; set; }

        public List<Skill> AllSkills { get; set; }
        public List<SkillListVM> UserSkills { get; set; }
        public List<SkillListVM> RemainingSkills { get; set; }
        public string totalseats { get; set; }
        public string timeavailability { get; set; }
        
        public string url { get; set; }
    }
}
