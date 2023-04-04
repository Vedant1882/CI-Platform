﻿using CI_Entity.Models;
using System.ComponentModel.DataAnnotations;

namespace CI_PlatformWeb.Models
{
    public class StoryShareViewModel
    {
        public List<Mission> missions { get; set; }
        public List<MissionApplication> missionapplication { get; set; }

        public long MissionId { get; set; }
        public long storyId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter Title")]
        public string title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter Discription")]
        public string editor1 { get; set; }

        public DateTime date { get; set; }

        public List<IFormFile> attachment { get; set; }
    }
}
