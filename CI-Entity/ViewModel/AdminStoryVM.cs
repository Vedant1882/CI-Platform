using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Entity.ViewModel
{
    public class AdminStoryVM
    {
        public long StoryId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string StoryTitle { get; set; }
        public string MissionTitle { get; set; }

    }
}