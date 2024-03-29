﻿using CI.Models;
using CI_Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using System.Net.Http;
using CI_PlatformWeb.Models;
using System.Diagnostics.Metrics;
using System.Web;
using NuGet.Packaging;
using Microsoft.Data.SqlClient;
using System.Linq;
using NuGet.Common;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace CI_PlatformWeb.Controllers
{
    public class HomeController : Controller
    {
        int i = 0;
        int i1= 0;
        int j = 0;
        int j1 = 0;
        int k = 0;
        int k1 = 0;
        private readonly ILogger<HomeController> _logger;

        private readonly CIDbContext _CIDbContext;
        public HomeController(ILogger<HomeController> logger, CIDbContext CIDbContext)
        {
            _logger = logger;
            _CIDbContext = CIDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult Volunteering()
        //{
        //    return View();
        //}

        public IActionResult NoMissionFound()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        //public IActionResult storyListing()
        //{
        //    return View();
        //}
        public IActionResult Login()
        {
            HttpContext.Session.Clear();
            return View();
        }
        public IActionResult ForgetPass()
        {
            return View();
        }
        public IActionResult ResetPassword()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult StoryShare()
        {
            return View();
        }
       

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model)
        {

            if (ModelState.IsValid)
            {
               
                var user = await _CIDbContext.Users.Where(u => u.Email == model.Email && u.Password == model.Password).FirstOrDefaultAsync();
                //var firstname = model.Email.Split("@")[0];
                if (user != null)
                {
                    int userid = ((int)user.UserId);
                    HttpContext.Session.SetString("UserID", user.FirstName);
                    HttpContext.Session.SetString("user", user.UserId.ToString());
                    HttpContext.Session.SetInt32("userIDforfavmission", userid);
                    return RedirectToAction(nameof(HomeController.landingpage), "Home");
                }
                else
                {
                    ViewBag.Email = "email or pass is incorrect";                  
                }
                
            }
            
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPass(ForgetPass model)
        {
            if (ModelState.IsValid)
            {
                var user = _CIDbContext.Users.FirstOrDefault(u => u.Email == model.Email);
                if (user == null)
                {
                    ViewBag.er="Incorrect Email Address";
                    return RedirectToAction("ForgetPass", "Home");
                    
                }

                
                var token = Guid.NewGuid().ToString();

                
                var passwordReset = new PasswordReset
                {
                    Email = model.Email,
                    Token = token
                };
                _CIDbContext.PasswordResets.Add(passwordReset);
                _CIDbContext.SaveChanges();

              
                var resetLink = Url.Action("ResetPassword", "Home", new { email = model.Email, token }, Request.Scheme);


                //var fromAddress = new MailAddress("tatvahl@gmail.com", "Sender Name");
                var fromAddress = new MailAddress("officehl1882@gmail.com", "Sender Name");
                var toAddress = new MailAddress(model.Email);
                var subject = "Password reset request";
                var body = $"Hi,<br /><br />Please click on the following link to reset your password:<br /><br /><a href='{resetLink}'>{resetLink}</a>";
                var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                var smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("officehl1882@gmail.com", "yedkuuhuklkqfzwx"),
                    //Credentials = new NetworkCredential("tatvahl@gmail.com", "dvbexvljnrhcflfw"),
                    EnableSsl = true
                };
            smtpClient.Send(message);

            return RedirectToAction("Login", "Home");
        }

            return View();
    }

        [HttpGet]
        [AllowAnonymous]
       



        [HttpGet]
        
        public ActionResult ResetPassword(string email, string token)
        {
            var passwordReset = _CIDbContext.PasswordResets.FirstOrDefault(pr => pr.Email == email && pr.Token == token);
            if (passwordReset == null)
            {
                return RedirectToAction("Login", "Home");
            }
            // Pass the email and token to the view for resetting the password
            var model = new ResetPasswordModel
            {
                Email = email,
                Token = token
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel resetPasswordView)
        {
            if (ModelState.IsValid)
            {
                // Find the user by email
                var user = _CIDbContext.Users.FirstOrDefault(u => u.Email == resetPasswordView.Email);
                if (user == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                // Find the password reset record by email and token
                var passwordReset = _CIDbContext.PasswordResets.FirstOrDefault(pr => pr.Email == resetPasswordView.Email && pr.Token == resetPasswordView.Token);
                if (passwordReset == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                // Update the user's password
                user.Password = resetPasswordView.Password;
                _CIDbContext.SaveChanges();




            }

            return View(resetPasswordView);
        }
        private List<MissionViewModel> missionsVMList = new List<MissionViewModel>();
        public ActionResult landingpage(int? pageIndex, string searchQuery, long[] ACountries, long[] ACities, long[] ATheme,string sortOrder)
        {

            var userId = HttpContext.Session.GetString("user");
            int? useridforrating = HttpContext.Session.GetInt32("userIDforfavmission");
            int? user = HttpContext.Session.GetInt32("userIDforfavmission");
            int? useridfavmission = HttpContext.Session.GetInt32("userIDforfavmission");
            if(userId != null || user != null)
            {
                ViewBag.UserId = int.Parse(userId);
            }
            
            List<Mission> mission = _CIDbContext.Missions.ToList();

            List<City> cityname = new List<City>();
            List<City> cityname1 = new List<City>();
            List<Mission> finalmission = _CIDbContext.Missions.ToList();
            List<Mission> newmission = _CIDbContext.Missions.ToList();
            List<GoalMission> goalMissions = _CIDbContext.GoalMissions.ToList();
            long[] missionempty;
            
            
            foreach (var item in mission)
            {
                
                var City = _CIDbContext.Cities.FirstOrDefault(u => u.CityId == item.CityId);
                var Theme = _CIDbContext.MissionThemes.FirstOrDefault(u => u.MissionThemeId == item.ThemeId);
                
                


            }
            
            //mission = _CIDbContext.Missions.ToList();
            List<Country> Countries = _CIDbContext.Countries.ToList();
            List<FavoriteMission> addfav = _CIDbContext.FavoriteMissions.ToList();

            List<Country> countryElements = _CIDbContext.Countries.ToList();
            List<City> Cities = _CIDbContext.Cities.ToList();
            List<MissionTheme> Themes = _CIDbContext.MissionThemes.ToList();
            List<Skill> skills = _CIDbContext.Skills.ToList();
            ViewBag.countries = Countries;
            ViewBag.themes = Themes;
            ViewBag.cities = Cities;
            ViewBag.skills = skills;

            if (!string.IsNullOrEmpty(searchQuery))
            {
                mission = newmission.Where(m => m.Title.ToUpper().Contains(searchQuery.ToUpper())).ToList();
                ViewBag.searchQuery = Request.Query["searchQuery"];
                if (mission.Count() == 0)
                {
                    return RedirectToAction("NoMissionFound","Home");
                }
            }
            missionempty = ACountries;
            if (ACountries != null && ACountries.Length > 0)
            {
               
                foreach (var country in ACountries)
                {
                    //mission = mission.Where(m => m.CountryId == country).ToList();
                    if (i == 0)
                    {
                        mission = mission.Where(m => m.CountryId == country + 500).ToList();
                        i++;
                    }
                    
                    finalmission = newmission.Where(m => m.CountryId == country).ToList();

                    mission.AddRange(finalmission);
                    if (mission.Count() == 0)
                    {
                        return RedirectToAction("NoMissionFound", "Home");
                    }
                    ViewBag.countryId = country;
                    if (ViewBag.countryId != null)
                    {
                        var countryElement = _CIDbContext.Countries.Where(m => m.CountryId == country).ToList();
                        if (i1 == 0) {
                            countryElements = _CIDbContext.Countries.Where(m => m.CountryId == country+50000).ToList();
                            i1++;
                        }
                        countryElements.AddRange(countryElement);
                        //var c1 = _CIDbContext.Countries.FirstOrDefault(m => m.CountryId == country);
                        //ViewBag.country = c1.Name;
                    }
                }
                ViewBag.country = countryElements;
                //Countries = _CIDbContext.Countries.ToList();
                
                
                
                
                
            }
            if (missionempty.Count() != 0)
            {

               
                    foreach (var a in missionempty)
                    {
                        cityname1 = _CIDbContext.Cities.Where(m => m.CountryId == a).ToList();
                        cityname.AddRange(cityname1);
                    }
                

                
                ViewBag.cities = cityname;
            }
            if (ACities != null && ACities.Length > 0)
            {
                    foreach (var city in ACities)
                    {
                        //mission = mission.Where(m => m.CityId == city).ToList();
                        if (j == 0)
                        {
                            mission = mission.Where(m => m.CityId == city + 500).ToList();
                            j++;
                        }

                        finalmission = newmission.Where(m => m.CityId == city).ToList();

                        mission.AddRange(finalmission);
                        if (mission.Count() == 0)
                        {
                            return RedirectToAction("NoMissionFound", "Home");
                        }
                        ViewBag.city = city;
                        if (ViewBag.city != null)
                        {
                            var city1 = _CIDbContext.Cities.Where(m => m.CityId == city).ToList();
                            if (j1 == 0)
                            {
                                Cities = _CIDbContext.Cities.Where(m => m.CityId == city + 50000).ToList();
                                j1++;
                            }
                            Cities.AddRange(city1);
                            //var c1 = _CIDbContext.Cities.FirstOrDefault(m => m.CityId == city);
                            //ViewBag.city = c1.Name;
                        }
                    }
                
                ViewBag.city = Cities;
                Cities = _CIDbContext.Cities.ToList();
                
                
            }
            if (ATheme != null && ATheme.Length > 0)
            {
                foreach (var theme in ATheme)
                {
                     
                    if (k == 0)
                    {
                        mission = mission.Where(m => m.ThemeId == theme + 500).ToList();
                        k++;
                    }

                    finalmission = newmission.Where(m => m.ThemeId == theme).ToList();

                    mission.AddRange(finalmission);
                            
                    if (mission.Count() == 0)
                    {
                        return RedirectToAction("NoMissionFound", "Home");
                    }
                    ViewBag.theme = theme;
                    if (ViewBag.theme != null)
                    {
                        var theme1 = _CIDbContext.MissionThemes.Where(m => m.MissionThemeId == theme).ToList();
                        if (k1 == 0)
                        {
                            Themes = _CIDbContext.MissionThemes.Where(m => m.MissionThemeId == theme + 50000).ToList();
                            k1++;
                        }
                        Themes.AddRange(theme1);
                        //var c1 = _CIDbContext.MissionThemes.FirstOrDefault(m => m.MissionThemeId == theme);
                        //ViewBag.theme = c1.Title;
                    }
                }
                ViewBag.theme = Themes;
                Themes = _CIDbContext.MissionThemes.ToList();
                
                
            }
            switch (sortOrder)
            {
                case "Newest":
                    mission = newmission.OrderByDescending(m => m.StartDate).ToList();
                    ViewBag.sortby = "Newest";
                    break;
                case "Oldest":
                    mission = newmission.OrderBy(m => m.StartDate).ToList();
                    ViewBag.sortby = "Oldest";
                    break;
                case "Lowest seats":
                    mission = mission.OrderBy(m => int.Parse(m.Availability)).ToList();
                    break;
                case "Highest seats":
                    mission = mission.OrderByDescending(m => int.Parse(m.Availability)).ToList();
                    break;
                case "Registration deadline":
                    mission = mission.OrderBy(m => m.EndDate).ToList();
                    break;

            }

            //---------------------------------------------------------------------------------------------------------------------------------------------------------------

            if (mission.Count() != 0)
            {
                
                foreach (var missions in mission)
                {
                    int finalrating = 0;
                    City city = _CIDbContext.Cities.Where(e => e.CityId == missions.CityId).FirstOrDefault();
                    MissionTheme theme = _CIDbContext.MissionThemes.Where(e => e.MissionThemeId == missions.ThemeId).FirstOrDefault();
                    GoalMission goalMission = _CIDbContext.GoalMissions.Where(gm => gm.MissionId == missions.MissionId).FirstOrDefault();
                    FavoriteMission favoriteMissions = _CIDbContext.FavoriteMissions.Where(FM => FM.MissionId == missions.MissionId).FirstOrDefault();
                    var ratinglist = _CIDbContext.MissionRatings.Where(m => m.MissionId == missions.MissionId).ToList();
                    var applicationmission = _CIDbContext.MissionApplications.Where(m => m.UserId == useridforrating && m.MissionId == missions.MissionId).FirstOrDefault();
                    
                    if (ratinglist.Count() > 0)
                    {
                        int rat = 0;
                        foreach (var m in ratinglist)
                        {
                            rat = rat + (m.Rating);


                        }
                        finalrating = rat / ratinglist.Count();
                    }

                    string[] startDateNtime = missions.StartDate.ToString().Split(' ');
                    string[] endDateNtime = missions.EndDate.ToString().Split(' ');
                    var ratings = _CIDbContext.MissionRatings.Where(e => e.MissionId == missions.MissionId).ToList();
                   
                    
                        missionsVMList.Add(new MissionViewModel
                        {
                            MissionId = missions.MissionId,
                            Title = missions.Title,
                            Description = missions.ShortDescription,
                            City = city.Name,
                            Organization = missions.OrganizationName,
                            Theme = theme.Title,
                            //Rating = rating,
                            StartDate = (DateTime)missions.StartDate,
                            EndDate = (DateTime)missions.EndDate,
                            missionType = missions.MissionType,
                            isFavrouite = (user != null) ? _CIDbContext.FavoriteMissions.Any(e => e.MissionId == missions.MissionId && e.UserId == user) : false,
                            ImgUrl = "~/images/Grow-Trees-On-the-path-to-environment-sustainability-3.png",
                            StartDateEndDate = "From " + startDateNtime[0] + " until " + endDateNtime[0],
                            NoOfSeatsLeft = 10,
                            Deadline = endDateNtime[0],
                            createdAt = (DateTime)missions.CreatedAt,
                            GoalText = goalMission.GoalObjectiveText,
                            UserId = useridfavmission,
                            addedtofavM = favoriteMissions.MissionId,
                            addedtofavU = (long)favoriteMissions.UserId,
                            avgrating=finalrating,
                            available = missions.Availability,
                            isapplied= (applicationmission != null) ? 1:0,
                        });
                    
                    
                    
                    
                   
                }

                if (mission.Count() == 0)
                {
                    return RedirectToAction("Nomission", "Home");
                }

                
            }
            

            //------------------------------------------------------------------------------------------------------------------------------
            List<User> Alluser = _CIDbContext.Users.ToList();
            List<VolunteeringVM> usernamelist = new List<VolunteeringVM>();
            foreach (var i in Alluser)
            {
                usernamelist.Add(new VolunteeringVM
                {
                    UserName = i.FirstName,
                    LastName = i.LastName,
                    UserIdForMail = i.UserId,

                });
            }
            ViewBag.alluser = usernamelist;

            int pageSize =3;
            int skip = (pageIndex ?? 0) * pageSize;
            var Missions = missionsVMList.Skip(skip).Take(pageSize).ToList();
            int totalMissions = missionsVMList.Count();
            ViewBag.TotalMission = totalMissions;
            ViewBag.TotalPages = (int)Math.Ceiling(totalMissions / (double)pageSize);
            ViewBag.CurrentPage = pageIndex ?? 0;

            

            // Get the current URL
            UriBuilder uriBuilder = new UriBuilder(Request.Scheme, Request.Host.Host);
            if (Request.Host.Port.HasValue)
            {
                uriBuilder.Port = Request.Host.Port.Value;
            }
            uriBuilder.Path = Request.Path;

            // Remove the query parameter you want to exclude
            var query = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            query.Remove("pageIndex");
            uriBuilder.Query = query.ToString();


            ViewBag.missions = Missions;
            ViewBag.currentUrl = uriBuilder.ToString();
            ViewBag.goal = goalMissions;


            return View(Missions);
        }
        [HttpPost]
        public async Task<IActionResult> Sendmail(long[] userid,int id)
        {
            foreach(var item in userid)
            {
                var user = _CIDbContext.Users.FirstOrDefault(u => u.UserId == item);
                var resetLink = Url.Action("Volunteering", "Home", new { user = user.UserId , missionId =id}, Request.Scheme);


                //var fromAddress = new MailAddress("tatvahl@gmail.com", "Sender Name");
                var fromAddress = new MailAddress("officehl1882@gmail.com", "Sender Name");
                var toAddress = new MailAddress(user.Email);
                var subject = "Password reset request";
                var body = $"Hi,<br /><br />This is to <br /><br /><a href='{resetLink}'>{resetLink}</a>";
                var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                var smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("officehl1882@gmail.com", "yedkuuhuklkqfzwx"),
                    //Credentials = new NetworkCredential("tatvahl@gmail.com", "dvbexvljnrhcflfw"),
                    EnableSsl = true
                };
                smtpClient.Send(message);
                
            }
            return Json(new { success = true });
        }
        [HttpPost]
        public async Task<IActionResult> Addrating(int rating, long Id, long missionId)
        {
            MissionRating ratingExists = await _CIDbContext.MissionRatings.FirstOrDefaultAsync(fm => fm.UserId == Id && fm.MissionId == missionId);
            if (ratingExists != null)
            {
                ratingExists.Rating = rating;
                _CIDbContext.Update(ratingExists);
                _CIDbContext.SaveChanges();
                return Json(new { success = true, ratingExists, isRated = true });
            }
            else
            {
                var ratingele = new MissionRating();
                ratingele.Rating = rating;
                ratingele.UserId = Id;
                ratingele.MissionId = missionId;
                _CIDbContext.Add(ratingele);
                _CIDbContext.SaveChanges();
                return Json(new { success = true, ratingele, isRated = true });
            }
        }

        [HttpPost]
        public async Task<IActionResult> comment(long MissionId, long UserId,String comment)
        {
            
            var newcomment = new Comment();
            newcomment.UserId = UserId;
            newcomment.MissionId = MissionId;
            newcomment.CommentText = comment;
            _CIDbContext.Add(newcomment);
            _CIDbContext.SaveChanges();
            
            return Json(new { success = true, newcomment });


        }

            [HttpPost]
        public async Task<IActionResult> AddToFav(long MissionId, long UserId)
        {
            FavoriteMission favoriteMission = await _CIDbContext.FavoriteMissions.FirstOrDefaultAsync(FM => FM.MissionId == MissionId && FM.UserId == UserId);

            if (favoriteMission != null)
            {
                FavoriteMission favoriteMissiondel = _CIDbContext.FavoriteMissions.Where(FM => FM.MissionId == MissionId && FM.UserId == UserId).FirstOrDefault();
                _CIDbContext.Remove(favoriteMissiondel);
                _CIDbContext.SaveChanges();
                return Json(new { success = true, favoriteMissiondel, isRated = false });
            }
            else
            {

                var favoriteMissionadd = new FavoriteMission();
                favoriteMissionadd.UserId = UserId;
                favoriteMissionadd.MissionId = MissionId;
                _CIDbContext.Add(favoriteMissionadd);
                _CIDbContext.SaveChanges();
                return Json(new { success = true, favoriteMissionadd, isRated = true });
            }
        }

        public IActionResult Volunteering(long id,int missionId)
        {
            
            
                //List<VolunteeringMission> missionsVM = new List<VolunteeringMission>();
                List<Mission> missionlist = _CIDbContext.Missions.ToList();

                int? useridforrating = HttpContext.Session.GetInt32("userIDforfavmission");
            ViewBag.userid=useridforrating;
                List<VolunteeringVM> relatedlist = new List<VolunteeringVM>();
                var mission = _CIDbContext.Missions.FirstOrDefault(m => m.MissionId == missionId);
                var favmission = _CIDbContext.FavoriteMissions.FirstOrDefault(FM => FM.MissionId == missionId);
                missionlist = missionlist.Where(t => t.ThemeId == mission.ThemeId && t.MissionId != mission.MissionId).Take(3).ToList();
                var theme = _CIDbContext.MissionThemes.FirstOrDefault(t => t.MissionThemeId == mission.ThemeId);
                var goaltxt = _CIDbContext.GoalMissions.FirstOrDefault(g => g.MissionId == mission.MissionId);
                var city = _CIDbContext.Cities.FirstOrDefault(s => s.CityId == mission.CityId);
                var ratings = _CIDbContext.MissionRatings.FirstOrDefault(MR => MR.MissionId == missionId && MR.UserId == useridforrating);
                var recvoldet = from U in _CIDbContext.Users join MA in _CIDbContext.MissionApplications on U.UserId equals MA.UserId where MA.MissionId == mission.MissionId select U;
                GoalMission goalMission = _CIDbContext.GoalMissions.Where(gm => gm.MissionId == mission.MissionId).FirstOrDefault();
                string[] startDateNtime = mission.StartDate.ToString().Split(' ');
                string[] endDateNtime = mission.EndDate.ToString().Split(' ');
            var applicationmission = _CIDbContext.MissionApplications.FirstOrDefault(m => m.UserId == useridforrating && m.MissionId==missionId);
            
                //var favrioute = (id != null) ? _CIDbContext.FavoriteMissions.Any(u => u.UserId == id && u.MissionId == mission.MissionId) : false;
            VolunteeringVM volunteeringMission = new();
            int finalrating = 0;
            var ratinglist = _CIDbContext.MissionRatings.Where(m => m.MissionId == mission.MissionId).ToList();
            if (ratinglist.Count() > 0)
            {
                int rat = 0;
                foreach (var m in ratinglist)
                {
                    rat = rat + (m.Rating);


                }
                finalrating = rat / ratinglist.Count();
            }
            ViewBag.totalvol= ratinglist.Count();
            volunteeringMission = new()
            {
                SingleTitle = mission.Title,
                Description = mission.Description,
                GoalText = goalMission != null ? goalMission.GoalObjectiveText : "null",
                StartDate = (DateTime)mission.StartDate,
                EndDate = (DateTime)mission.EndDate,
                StartDateEndDate = "From " + startDateNtime[0] + " until " + endDateNtime[0],
                missionType = mission.MissionType,
                MissionId = mission.MissionId,
                City = city.Name,
                Theme = theme.Title,
                Organization = mission.OrganizationName,
                Rating = ratings != null ? ratings.Rating : 0,
                isFavrouite = (useridforrating != null) ? _CIDbContext.FavoriteMissions.Any(e => e.MissionId == mission.MissionId && e.UserId == useridforrating) : false,
                UserId = useridforrating,
                avgrating = finalrating,
                available=mission.Availability,
                isapplied = (applicationmission != null) ? 1 : 0,
            };
            ViewBag.fav = volunteeringMission.isFavrouite;


            List<VolunteeringVM> missioncomment = new List<VolunteeringVM>();
            var commentlist = _CIDbContext.Comments.Where(m => m.MissionId == missionId).ToList();
            foreach (var comment in commentlist)
            {
                var user=_CIDbContext.Users.FirstOrDefault(m => m.UserId== comment.UserId);
                missioncomment.Add(new VolunteeringVM
                {
                    commenttext = comment.CommentText,
                    UserName=user.FirstName,
                    LastName=user.LastName,

                });


            }
            ViewBag.missioncomment = missioncomment;


            var relatedmission = _CIDbContext.Missions.Where(m => m.ThemeId == mission.ThemeId && m.MissionId != mission.MissionId).ToList();
                foreach (var item in relatedmission.Take(3))
                {
                    var relcity = _CIDbContext.Cities.FirstOrDefault(m => m.CityId == item.CityId);
                    var reltheme = _CIDbContext.MissionThemes.FirstOrDefault(m => m.MissionThemeId == item.ThemeId);
                    var relgoalobj = _CIDbContext.GoalMissions.FirstOrDefault(m => m.MissionId == item.MissionId);
                    var Startdate1 = item.StartDate;
                    var Enddate2 = item.EndDate;

                    relatedlist.Add(new VolunteeringVM
                    {
                        MissionId = item.MissionId,
                        City = relcity.Name,
                        Theme = reltheme.Title,
                        SingleTitle = item.Title,
                        Description = item.ShortDescription,
                        StartDate = Startdate1,
                        EndDate = Enddate2,

                        Organization = item.OrganizationName,
                        GoalText = relgoalobj.GoalObjectiveText,
                        missionType = item.MissionType,


                    });
                }

                ViewBag.relatedmission = relatedlist.Take(3);
                List<VolunteeringVM> recentvolunteredlist = new List<VolunteeringVM>();
                var recentvoluntered = from U in _CIDbContext.Users join MA in _CIDbContext.MissionApplications on U.UserId equals MA.UserId where MA.MissionId == mission.MissionId select U;
                foreach (var item in recentvoluntered)
                {
                    recentvolunteredlist.Add(new VolunteeringVM
                    {
                        UserName = item.FirstName,
                    });

                }
                ViewBag.recentvolunteered = recentvolunteredlist;

                List<User> Alluser = _CIDbContext.Users.ToList();
                List<VolunteeringVM> usernamelist = new List<VolunteeringVM>();
                foreach (var i in Alluser)
                {
                    usernamelist.Add(new VolunteeringVM
                    {
                        UserName = i.FirstName,
                        LastName = i.LastName,
                        UserIdForMail = i.UserId,

                    });
                }
                ViewBag.alluser = usernamelist;
                return View(volunteeringMission);
            }

        public IActionResult storyListing(int? pageIndex)
        {
            List<Story> story=_CIDbContext.Stories.ToList();
            
            List<storyListingViewModel> storylist = new List<storyListingViewModel>();
            foreach(var item in story)
            {
                var user = _CIDbContext.Users.FirstOrDefault(u => u.UserId == item.UserId);
                storylist.Add(new storyListingViewModel
                {
                    UserId= user.UserId,
                    StoryTitle = item.Title,
                    Description = item.Description,
                    StoryId= item.StoryId,
                    MissionId = item.MissionId,
                    UserName = user.FirstName,
                    LastName = user.LastName,

                });
                
            }
            
            ViewBag.story= storylist;
            int pageSize = 1;
            int skip = (pageIndex ?? 0) * pageSize;
            var Missions = storylist.Skip(skip).Take(pageSize).ToList();
            int totalMissions = storylist.Count();
            ViewBag.TotalMission = totalMissions;
            ViewBag.TotalPages = (int)Math.Ceiling(totalMissions / (double)pageSize);
            ViewBag.CurrentPage = pageIndex ?? 0;



            // Get the current URL
            UriBuilder uriBuilder = new UriBuilder(Request.Scheme, Request.Host.Host);
            if (Request.Host.Port.HasValue)
            {
                uriBuilder.Port = Request.Host.Port.Value;
            }
            uriBuilder.Path = Request.Path;

            // Remove the query parameter you want to exclude
            var query = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            query.Remove("pageIndex");
            uriBuilder.Query = query.ToString();
            ViewBag.currentUrl = uriBuilder.ToString();
            return View(Missions);

        }

        public IActionResult StoryDetail(int id,int storyid)
        {
            var user = _CIDbContext.Users.FirstOrDefault(u => u.UserId == id);
            var story=_CIDbContext.Stories.Where(s=>s.StoryId==storyid).ToList();
            List<storyListingViewModel> storylist = new List<storyListingViewModel>();
            foreach (var item in story)
            {
                
                storylist.Add(new storyListingViewModel
                {
                    StoryTitle = item.Title,
                    Description = item.Description,
                    StoryId = item.StoryId,
                    MissionId = item.MissionId,
                    UserName = user.FirstName,
                    LastName = user.LastName,
                    UserId= user.UserId,

                });

            }
             List<User> Alluser = _CIDbContext.Users.ToList();
                List<VolunteeringVM> usernamelist = new List<VolunteeringVM>();
                foreach (var i in Alluser)
                {
                    usernamelist.Add(new VolunteeringVM
                    {
                        UserName = i.FirstName,
                        LastName = i.LastName,
                        UserIdForMail = i.UserId,

                    });
                }
                ViewBag.alluser = usernamelist;
            return View(storylist);
        }
        [HttpPost]
        public async Task<IActionResult> SendmailForStory(long[] userid, int id)
        {
            foreach (var item in userid)
            {
                var user = _CIDbContext.Users.FirstOrDefault(u => u.UserId == item);
                var story = _CIDbContext.Stories.FirstOrDefault(s => s.StoryId == id);
                var resetLink = Url.Action("StoryDetail", "Home", new { id=story.UserId, storyid = id }, Request.Scheme);


                //var fromAddress = new MailAddress("tatvahl@gmail.com", "Sender Name");
                var fromAddress = new MailAddress("officehl1882@gmail.com", "Sender Name");
                var toAddress = new MailAddress(user.Email);
                var subject = "Password reset request";
                var body = $"Hi,<br /><br />This is to <br /><br /><a href='{resetLink}'>{resetLink}</a>";
                var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                var smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("officehl1882@gmail.com", "yedkuuhuklkqfzwx"),
                    //Credentials = new NetworkCredential("tatvahl@gmail.com", "dvbexvljnrhcflfw"),
                    EnableSsl = true
                };
                smtpClient.Send(message);

            }
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> applymission(int MissionId,int UserId)
        {
            var missionapplication = new MissionApplication();
            missionapplication.UserId = UserId;
            missionapplication.MissionId = MissionId;
            missionapplication.AppliedAt= DateTime.Now;
            missionapplication.ApprovalStatus = "1";
            _CIDbContext.Add(missionapplication);
            _CIDbContext.SaveChanges();

            return Json(new { success = true });
        }
    }
        
    }

