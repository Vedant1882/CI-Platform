using CI_Entity.Models;
using CI_Platform.Repository.Interface;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Repository
{
    public class HomeRepository:IHomeRepository
    {
        public readonly CIDbContext _CIDbContext;

        public HomeRepository(CIDbContext CIDbContext)
        {
            _CIDbContext = CIDbContext;


        }
        public User Logindetails(String Email, String Password)
        {
            return _CIDbContext.Users.Where(u => u.Email == Email && u.Password == Password).FirstOrDefault();
        }
        public Admin AdminEmail(String Email)
        {
            return _CIDbContext.Admins.Where(u => u.Email == Email).FirstOrDefault();
        }
        public User UserByEmail(String Email)
        {
            return _CIDbContext.Users.FirstOrDefault(u => u.Email == Email);
        }
        public PasswordReset UserBymail_token(String mail, String token)
        {
            return _CIDbContext.PasswordResets.FirstOrDefault(pr => pr.Email == mail && pr.Token == token);
        }
        public User UserByUserid(long userid)
        {
            return _CIDbContext.Users.FirstOrDefault(u => u.UserId == userid);
        }
        public void addtopassreset(PasswordReset passreset)
        {
            _CIDbContext.PasswordResets.Add(passreset);
            _CIDbContext.SaveChanges();
        }
        public void savechanges()
        {
            _CIDbContext.SaveChanges();
        }
        public List<Mission> Allmissions()
        {
            return _CIDbContext.Missions.ToList();
        }
        public List<City> AllCity()
        {
            return _CIDbContext.Cities.ToList();
        }
        public List<Skill> AllSkills()
        {
           return _CIDbContext.Skills.ToList();
        }
        public List<MissionRating> missionRatings()
        {
            return _CIDbContext.MissionRatings.ToList();   
        }
        public List<GoalMission> goalmission()
        {
            return _CIDbContext.GoalMissions.ToList();
        }
        public List<MissionApplication> missionapplication()
        {
            return _CIDbContext.MissionApplications.ToList();
        }
        public List<FavoriteMission> favmission()
        {
            return _CIDbContext.FavoriteMissions.ToList();
        }
        public MissionRating MissionratingByUserid_Missionid(long userid, long missionid)
        {
            return _CIDbContext.MissionRatings.FirstOrDefault(fm => fm.UserId == userid && fm.MissionId == missionid);
        }
        public MissionTheme MissionThemeByThemeid(long themeid)
        {
            return _CIDbContext.MissionThemes.FirstOrDefault(t => t.MissionThemeId == themeid);
        }
        public List<MissionTheme> missiontheme()
        {
            return _CIDbContext.MissionThemes.ToList();
        }
        public FavoriteMission FavmissionByMissionid(long missionid)
        {
            return _CIDbContext.FavoriteMissions.FirstOrDefault(FM => FM.MissionId == missionid);
        }
        public FavoriteMission FavmissionByMissionid_Userid(long missionid, long userid)
        {
            return _CIDbContext.FavoriteMissions.Where(FM => FM.MissionId == missionid && FM.UserId == userid).FirstOrDefault();
        }
        public MissionRating updaterating(MissionRating ratingExists,int rating)
        {
            ratingExists.Rating = rating;
            _CIDbContext.Update(ratingExists);
            _CIDbContext.SaveChanges();
            return ratingExists;
        }
        public MissionRating addratings(int rating, long id, long missionid)
        {
            var ratingele = new MissionRating();
            ratingele.Rating = rating;
            ratingele.UserId = id;
            ratingele.MissionId = missionid;
            _CIDbContext.Add(ratingele);
            _CIDbContext.SaveChanges();
            return ratingele;
        }
        public Comment addcomment(long MissionId, long UserId, String comment)
        {
            var newcomment = new Comment();
            newcomment.UserId = UserId;
            newcomment.MissionId = MissionId;
            newcomment.CommentText = comment;
            newcomment.CreatedAt= DateTime.Now;
            _CIDbContext.Add(newcomment);
            _CIDbContext.SaveChanges();
            return newcomment;
        }
        public FavoriteMission removefav(FavoriteMission favmission)
        {
            _CIDbContext.Remove(favmission);
            _CIDbContext.SaveChanges();
            return favmission;
        }
        public FavoriteMission addfav(long MissionId, long UserId)
        {
            var favoriteMissionadd = new FavoriteMission();
            favoriteMissionadd.UserId = UserId;
            favoriteMissionadd.MissionId = MissionId;
            _CIDbContext.Add(favoriteMissionadd);
            _CIDbContext.SaveChanges();
            return favoriteMissionadd;
        }
        public List<Comment> comment()
        {
            return _CIDbContext.Comments.ToList();
        }
        public List<MissionTheme> alltheme()
        {
            return _CIDbContext.MissionThemes.ToList();
        }
        public List<Country> allcountry()
        {
            return _CIDbContext.Countries.ToList();
        }
        
        public List<Story> story()
        {
            return _CIDbContext.Stories.ToList();
        }
        public List<StoryMedium> storymedia()
        {
            return _CIDbContext.StoryMedia.ToList();
        }
        public List<MissionMedium> allmedia()
        {
            return _CIDbContext.MissionMedia.ToList();
        }
        public List<Story> StoryByStoryidList(long storyid)
        {
            return _CIDbContext.Stories.Where(s => s.StoryId == storyid).ToList(); ;
        }
        public List<User> alluser()
        {
            return _CIDbContext.Users.ToList(); 
        }
        public Story StoryByStoryid(long storyid)
        {
            return _CIDbContext.Stories.FirstOrDefault(s => s.StoryId == storyid);
        }
        public void apply(long missionid, long userid)
        {
            var missionapplication = new MissionApplication();
            missionapplication.UserId = userid;
            missionapplication.MissionId = missionid;
            missionapplication.AppliedAt = DateTime.Now;
            missionapplication.ApprovalStatus = "1";
            _CIDbContext.Add(missionapplication);
            _CIDbContext.SaveChanges();
        }
        public long addstory(long MissionId, string title, DateTime date, string discription, long id,long storyId)
        {
            if (storyId == 0)
            {
                var Stories = new Story();
                Stories.MissionId = MissionId;
                Stories.UserId = id;
                Stories.Title = title;
                Stories.Description = discription;
                Stories.Status = "1";
                Stories.CreatedAt = date;
                _CIDbContext.Add(Stories);
                _CIDbContext.SaveChanges();
                return Stories.StoryId;
            }
            else
            {
                var story=_CIDbContext.Stories.Where(s=>s.StoryId==storyId).FirstOrDefault();
                
                story.MissionId = MissionId;
                story.UserId = id;
                story.Title = title;
                story.Description = discription;
                story.Status = "1";
                story.UpdatedAt =DateTime.Now;
                _CIDbContext.Update(story);
                _CIDbContext.SaveChanges();
                return story.StoryId;
            }
            
                
            
        }
        public long addstorydraft(long MissionId, string title, DateTime date, string discription, long id,long storyId)
        {
            if (storyId == 0)
            {
                var Stories = new Story();
                Stories.MissionId = MissionId;
                Stories.UserId = id;
                Stories.Title = title;
                Stories.Description = discription;
                Stories.Status = "draft";
                Stories.CreatedAt = date;
                _CIDbContext.Add(Stories);
                _CIDbContext.SaveChanges();
                return Stories.StoryId;
            }
            else
            {
                var story = _CIDbContext.Stories.Where(s => s.StoryId == storyId).FirstOrDefault();
                story.MissionId = MissionId;
                story.UserId = id;
                story.Title = title;
                story.Description = discription;
                story.Status = "draft";
                story.UpdatedAt = DateTime.Now;
                _CIDbContext.Update(story);
                _CIDbContext.SaveChanges();
                return story.StoryId;

            }
           


        }
        public void addstoryMedia(long MissionId, string mediatype, string mediapath, long id,long storyId, long sId)
        {
            

                
                StoryMedium st = new StoryMedium();
                st.StoryId = sId;
                st.StoryType = mediatype;
                st.StoryPath = mediapath;
                _CIDbContext.Add(st);
                _CIDbContext.SaveChanges();
            
            
            
        }
        public void removemedia(long storyId)
        {
            var storyMedia = _CIDbContext.StoryMedia.Where(s => s.StoryId == storyId).ToList();
            foreach (var s in storyMedia)
            {
                _CIDbContext.StoryMedia.Remove(s);
                _CIDbContext.SaveChanges();
            }
        }
        public void addtimesheet(long MissionId, long id, int? hour, int? minute, DateTime date, string message, int? action,long? timesheetid)
        {
            if(timesheetid== 0)
            {
                if (hour != null && minute != null)
                {
                    var timesheet = new Timesheet();
                    timesheet.MissionId = MissionId;
                    timesheet.UserId = id;
                    timesheet.TimesheetTime = hour + ":" + minute;
                    timesheet.DateVolunteered = date;
                    timesheet.Notes = message;
                    timesheet.Status = "1";
                    timesheet.CreatedAt = DateTime.Now;
                    _CIDbContext.Add(timesheet);
                    _CIDbContext.SaveChanges();
                }
                else
                {
                    var timesheet = new Timesheet();
                    timesheet.MissionId = MissionId;
                    timesheet.UserId = id;
                    timesheet.DateVolunteered = date;
                    timesheet.Action = action;
                    timesheet.Notes = message;
                    timesheet.Status = "1";
                    timesheet.CreatedAt = DateTime.Now;
                    _CIDbContext.Add(timesheet);
                    _CIDbContext.SaveChanges();
                }
            }
            else
            {
                if (hour != null && minute != null)
                {
                    var timesheet = _CIDbContext.Timesheets.FirstOrDefault(t => t.TimesheetId == timesheetid);
                    timesheet.MissionId = MissionId;
                    timesheet.UserId = id;
                    timesheet.TimesheetTime = hour + ":" + minute;
                    timesheet.DateVolunteered = date;
                    timesheet.Notes = message;
                    timesheet.Status = "1";
                    timesheet.UpdatedAt = DateTime.Now;
                    _CIDbContext.Update(timesheet);
                    _CIDbContext.SaveChanges();
                }
                else
                {
                    var timesheet = _CIDbContext.Timesheets.FirstOrDefault(t => t.TimesheetId == timesheetid);
                    timesheet.MissionId = MissionId;
                    timesheet.UserId = id;
                    timesheet.DateVolunteered = date;
                    timesheet.Action = action;
                    timesheet.Notes = message;
                    timesheet.Status = "1";
                    timesheet.UpdatedAt = DateTime.Now;
                    _CIDbContext.Update(timesheet);
                    _CIDbContext.SaveChanges();
                }
            }
            
            
           





        }
        public List<Timesheet> alltimesheet()
        {
            return _CIDbContext.Timesheets.Where(e => e.DeletedAt == null).ToList();
        }
        public void deletetimesheet(long timesheetid)
        {
            var time = _CIDbContext.Timesheets.FirstOrDefault(t => t.TimesheetId == timesheetid);
            time.DeletedAt= DateTime.Now;
            time.Status= "deleted";
            _CIDbContext.Update(time);
            _CIDbContext.SaveChanges();
        }
        public void commentdelete(long? userId, long? commentId)
        {
            var comment=_CIDbContext.Comments.FirstOrDefault(c => c.CommentId == commentId);
            _CIDbContext.Comments.Remove(comment);
            _CIDbContext.SaveChanges();
        }
        public void changepass(long? id, string? password)
        {
            var user = _CIDbContext.Users.FirstOrDefault(t => t.UserId == id);
            
            user.Password = password;
            _CIDbContext.Update(user);
            _CIDbContext.SaveChanges();
        }
        public void updateuser(User user)
        {
            _CIDbContext.Update(user);
            _CIDbContext.SaveChanges();
        }
        public List<UserSkill> UserSkills(long userid)
        {
            return _CIDbContext.UserSkills.ToList();
        }
        public void AddUserSkills(long skillid, long userId)
        {
            var userskills = new UserSkill();
            userskills.UserId = userId;
            userskills.SkillId = skillid;
            _CIDbContext.Add(userskills);
            _CIDbContext.SaveChanges();
        }
        public ContactU addContactUs(string subject,string message, string username, string email)
        {
            var contactUs = new ContactU();
            contactUs.UserName= username;
            contactUs.Email= email;
            contactUs.Subject = subject;
            contactUs.Message = message;
         
            _CIDbContext.Add(contactUs);
            _CIDbContext.SaveChanges();
            return contactUs;
        }

    }
}
