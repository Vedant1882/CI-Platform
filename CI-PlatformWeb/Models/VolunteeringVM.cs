namespace CI_PlatformWeb.Models
{
    public class VolunteeringVM
    {
        public long? MissionId { get; set; }

        public string? SingleTitle { get; set; }
        public string? Description { get; set; }

        public string? Organization { get; set; }

        public long? Rating { get; set; } = null;

        public string? ImgUrl { get; set; }

        public string? Theme { get; set; }

        public string? missionType { get; set; }
        public bool? isFavrouite { get; set; } = null;

        public bool? userApplied { get; set; }

        public string? City { get; set; }

        public string? StartDateEndDate { get; set; }

        public DateTime? StartDate { get; set; } = null;

        public DateTime? EndDate { get; set; }=null;

        public int? NoOfSeatsLeft { get; set; }

        public string? Deadline { get; set; }

        public DateTime? createdAt { get; set; }
        public long? UserId { get; set; }
        public string? GoalText { get; set; }
        public long? addedtofav { get; set; }

        public string? allusers { get; set; }
        public string? UserName { get; set; }
        public string? LastName { get; set; }

        public string? commenttext { get; set; }
        public long? UserIdForMail { get; set; }
        
        public int ? avgrating { get; set; }

        public int? available { get; set; }

        public int? appMissionId { get; set; }

        public int? isapplied { get; set; }
        public int? isclosed { get; set; }
        public int? ispending { get; set; }
        public int? isrejected { get; set; }

        public string? path { get; set; }
        public string? avtarpath { get; set; }
        public string? cmtavtarpath { get; set; }
        public int? goalval { get; set; }

        public long? userIdForComment { get; set; }
        public long? commentId { get; set; }
        public int goal { get; set; }
        public int? progressInPerc { get; set; }

        public int? progress { get; set; }
    }
}
