using CI_Entity.Models;

namespace CI_Entity.ViewModel
{
    public class MissionApplicationViewModel
    {
        public long UserId { get; set; }
        public long MissionId { get; set; }
        public long MissionApplicationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Title { get; set; }

        public DateTime AppliedAt { get; set; }
    }
}