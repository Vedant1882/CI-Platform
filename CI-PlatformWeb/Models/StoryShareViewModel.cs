using CI_Entity.Models;


namespace CI_PlatformWeb.Models
{
    public class StoryShareViewModel
    {
        public List<Mission> missions { get; set; }
        public List<MissionApplication> missionapplication { get; set; }

        public long MissionId { get; set; }

        public string title { get; set; }

        public string editor1 { get; set; }

        public DateTime date { get; set; }

        public List<IFormFile> attachment { get; set; }
    }
}
