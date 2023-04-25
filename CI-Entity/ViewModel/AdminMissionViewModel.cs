using CI_Entity.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage = "City Name is a Required field.")]
        public long cityId { get; set; }

        [Required(ErrorMessage = "Country Name is a Required field.")]
        public long countryId { get; set; }

        [Required(ErrorMessage = "First Name is a Required field.")]
        public string title { get; set; }

        [StringLength(50, MinimumLength = 25, ErrorMessage = "Short Desciption must be atleast 25 characters")]
        public string shortdescription { get; set; }
        public string goalObjectiveText { get; set; }
        public string goalValue { get; set; }

        [Required(ErrorMessage = "Discription is a Required field.")]
        [StringLength(50, MinimumLength = 25, ErrorMessage = "Description must be atleast 25 characters")]
        public string editor2 { get; set; }
        public string organizationName { get; set; }
        public string selectedSkills { get; set; }
        public string organizationDetail{ get; set; }

        public List <Country> countries { get; set; }
        public List<City> cities { get; set; }
        public List<MissionTheme> themes { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public DateTime? deadline{ get; set; }
        public string missionType { get; set; }

        public List<Skill> AllSkills { get; set; }
        public List<MissionMedium> ImageFiles { get; set; }
        public List<IFormFile> DocFiles { get; set; }
        public List<SkillListVM> MissionSkill { get; set; }
        public List<Skill> RemainingSkills { get; set; }
        public string totalseats { get; set; }
        public string timeavailability { get; set; }
        
        public string url { get; set; }
    }
}
