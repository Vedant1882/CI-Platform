using CI_Entity.Models;
using System.ComponentModel.DataAnnotations;

namespace CI_PlatformWeb.Models
{
    public class TimesheetViewModel
    {
        [Required(ErrorMessage = "Mission is a Required field.")]
        public long missionId { get; set; }
        [Required(ErrorMessage = "Action is a Required field.")]
        public int? action { get; set; }
       
        public List<Mission> missions { get; set; }
      
        public List<MissionApplication> missionapplication { get; set; }
        public List<Timesheet> timesheet { get; set; }
        [Required(ErrorMessage = "Date is a Required field.")]
        public DateTime date { get; set; }
        public long hiddenInput { get; set; }
        [Required(ErrorMessage = "Hour is a Required field.")]
        public int? hour { get; set; }
        [Required(ErrorMessage = "Minute is a Required field.")]
        public int? minute { get; set; }
        [Required(ErrorMessage = "Message is a Required field.")]
        public string? message { get; set; }
        [Required(ErrorMessage = "Title is a Required field.")]
        public string? title { get; set; }
    }
}
