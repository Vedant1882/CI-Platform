using CI_Entity.Models;
using CI_Platform.Repository.Interface;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

    }
}
