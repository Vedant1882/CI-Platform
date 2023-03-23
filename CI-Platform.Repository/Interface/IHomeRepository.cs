﻿using CI_Entity.Models;
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

        public List<User> alluser();
        public User UserByEmail(String Email);

        public User UserByUserid(long userid);

        public PasswordReset UserBymail_token(String mail, String token);

        public List<Mission> Allmissions();

        public List<City>  AllCity();

        public List<MissionRating> missionRatings();

        public List<GoalMission> goalmission();

        public List<MissionApplication> missionapplication();
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

        public void savechanges();

        public List<Story> story();
        public List<Story> StoryByStoryidList(long storyid); 
        public Story StoryByStoryid(long storyid);

        public void apply(long missionid,long userid);




    }
}