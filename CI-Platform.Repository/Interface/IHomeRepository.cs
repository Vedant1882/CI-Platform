using CI_Entity.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Interface
{
    public interface IHomeRepository
    {
        public User Logindetails(String Email,String Password);
        
        public Admin AdminEmail(String Email);

        public List<User> alluser();
        public User UserByEmail(String Email);

        public User UserByUserid(long userid);

        public PasswordReset UserBymail_token(String mail, String token);

        public List<Mission> Allmissions();

        public List<City>  AllCity();
        public List<Skill>  AllSkills();

        public List<MissionRating> missionRatings();

        public List<GoalMission> goalmission();

        public List<MissionApplication> missionapplication();
        public List<MissionTheme> missiontheme();
        public MissionRating MissionratingByUserid_Missionid(long userid, long missionid);

        public FavoriteMission FavmissionByMissionid_Userid(long missionid, long userid);
        public FavoriteMission FavmissionByMissionid(long missionid);

        public List<FavoriteMission> favmission();

        public MissionTheme MissionThemeByThemeid(long themeid);

        public MissionRating addratings(int rating,long id,long missionid);
        public MissionRating updaterating(MissionRating ratingExists,int rating);

        public Comment addcomment(long MissionId, long UserId, String comment);

        public FavoriteMission removefav(FavoriteMission favmission);

        public FavoriteMission addfav(long MissionId, long UserId);

        public void addtopassreset(PasswordReset passreset);

        public List<Comment> comment();

        public List<MissionTheme> alltheme();
        public List<Country> allcountry();
        public List<Timesheet> alltimesheet();
        public void deletetimesheet(long timesheetid);
        public void commentdelete(long? userId,long? commentId);
        public List<MissionMedium> allmedia();

        public void savechanges();

        public List<Story> story();
        public List<StoryMedium> storymedia();
        public List<Story> StoryByStoryidList(long storyid); 
        public Story StoryByStoryid(long storyid);

        public void apply(long missionid,long userid);
        public long addstory(long MissionId,string title,DateTime date,string discription,long id, long storyId);
        public void addtimesheet(long MissionId,long id, int? hour,int? minute, DateTime date,string message,int? action,long? timesheetid);
        public void addstoryMedia(long MissionId,string mediatype,string mediapath,long id,long storyId, long sId);

        public long addstorydraft(long MissionId, string title, DateTime date, string discription, long id,long storyId);

        public void removemedia(long storyId);

        public void changepass(long? id, string? password);

        public void updateuser(User user);

        public List<UserSkill> UserSkills(long userid);

        public void AddUserSkills(long skillid,long userId);
        public ContactU addContactUs(string subject, string message,string username,string email);
        public MissionTheme AddTheme(string themeName);
        public MissionTheme UpdateTheme(string themeName,long themeId);
        public MissionTheme DeleteTheme(long themeId);
        public Skill AddSkill(string skillName);
        public Skill UpdateSkill(string skillName, long skillId);
        public Skill DeleteSkill(long skillId);
        
        public User AddUser(string firstname, string lastname, string email, string password, string department, string status, string employeeid);
    }
}
