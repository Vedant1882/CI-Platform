using CI.Models;
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
using CI_Platform.Repository.Interface;
using Newtonsoft.Json.Linq;
using System.Reflection;

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
        private readonly IHomeRepository _IHome;
        public HomeController(CIDbContext CIDbContext, IHomeRepository IHome)
        {
            _IHome = IHome;
            _CIDbContext = CIDbContext;


        }

        public IActionResult Index()
        {
            return View();
        }
        

        public IActionResult NoMissionFound()
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

                var user = _IHome.Logindetails(model.Email, model.Password);
                
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
                var user = _IHome.UserByEmail(model.Email);
                if (user == null)
                {
                    ViewBag.er = "Incorrect Email Address";
                    return RedirectToAction("ForgetPass", "Home");

                }


                var token = Guid.NewGuid().ToString();


                var passwordReset = new PasswordReset
                {
                    Email = model.Email,
                    Token = token
                };
                _IHome.addtopassreset(passwordReset);


                var resetLink = Url.Action("ResetPassword", "Home", new { email = model.Email, token }, Request.Scheme);


                //var fromAddress = new MailAddress("tatvahl@gmail.com", "Sender Name");
                var fromAddress = new MailAddress("officehl1881@gmail.com", "Sender Name");
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
                    Credentials = new NetworkCredential("officehl1881@gmail.com", "vrbxqqayjlbvoihx"),
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
            var passwordReset = _IHome.UserBymail_token(email, token);
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
                var user = _IHome.UserByEmail(resetPasswordView.Email);
                if (user == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                // Find the password reset record by email and token
                var passwordReset = _IHome.UserBymail_token(resetPasswordView.Email,resetPasswordView.Token);
                if (passwordReset == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                // Update the user's password
                user.Password = resetPasswordView.Password;
                _IHome.savechanges();




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

            List<Mission> mission = _IHome.Allmissions();
            List<Mission> finalmission = _IHome.Allmissions();
            List<Mission> newmission = _IHome.Allmissions();
            List<GoalMission> goalMissions = _IHome.goalmission();
            List<Country> Countries = _IHome.allcountry().ToList();
            List<FavoriteMission> addfav = _IHome.favmission();
            List<Country> countryElements = _IHome.allcountry().ToList();
            List<City> Cities = _IHome.AllCity();
            List<MissionTheme> Themes = _IHome.alltheme().ToList();
            List<Skill> skills = _CIDbContext.Skills.ToList();
            List<City> cityname = new List<City>();
            List<City> cityname1 = new List<City>();
            ViewBag.countries = Countries;
            ViewBag.themes = Themes;
            ViewBag.cities = Cities;
            ViewBag.skills = skills;
            long[] missionempty;
            
            
            foreach (var item in mission)
            {
                
                var City = _IHome.AllCity().FirstOrDefault(u => u.CityId == item.CityId);
                var Theme = _IHome.alltheme().FirstOrDefault(u => u.MissionThemeId == item.ThemeId);
                
                


            }
            
            //mission = _CIDbContext.Missions.ToList();
            
           

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
                        var countryElement = _IHome.allcountry().Where(m => m.CountryId == country).ToList();
                        if (i1 == 0) {
                            countryElements = _IHome.allcountry().Where(m => m.CountryId == country+50000).ToList();
                            i1++;
                        }
                        countryElements.AddRange(countryElement);
                       
                    }
                }
                ViewBag.country = countryElements;
                
                
                
                
                
                
            }
            if (missionempty.Count() != 0)
            {

               
                    foreach (var a in missionempty)
                    {
                        cityname1 = _IHome.AllCity().Where(m => m.CountryId == a).ToList();
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
                            var city1 = _IHome.AllCity().Where(m => m.CityId == city).ToList();
                            if (j1 == 0)
                            {
                                Cities = _IHome.AllCity().Where(m => m.CityId == city + 50000).ToList();
                                j1++;
                            }
                            Cities.AddRange(city1);
                        }
                    }
                
                ViewBag.city = Cities;
                Cities = _IHome.AllCity();



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
                        var theme1 = _IHome.alltheme().Where(m => m.MissionThemeId == theme).ToList();
                        if (k1 == 0)
                        {
                            Themes = _IHome.alltheme().Where(m => m.MissionThemeId == theme + 50000).ToList();
                            k1++;
                        }
                        Themes.AddRange(theme1);
                    }
                }
                ViewBag.theme = Themes;
                Themes = _IHome.alltheme();



            }
            switch (sortOrder)
            {
                case "Newest":
                    mission = mission.OrderByDescending(m => m.StartDate).ToList();
                    ViewBag.sortby = "Newest";
                    break;
                case "Oldest":
                    mission = mission.OrderBy(m => m.StartDate).ToList();
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
                    City city = _IHome.AllCity().Where(e => e.CityId == missions.CityId).FirstOrDefault();
                    MissionTheme theme = _IHome.alltheme().Where(e => e.MissionThemeId == missions.ThemeId).FirstOrDefault();
                    GoalMission goalMission = _IHome.goalmission().Where(gm => gm.MissionId == missions.MissionId).FirstOrDefault();
                    FavoriteMission favoriteMissions = _IHome.favmission().Where(FM => FM.MissionId == missions.MissionId).FirstOrDefault();
                    var ratinglist = _IHome.missionRatings().Where(m => m.MissionId == missions.MissionId).ToList();
                    var applicationmission = _IHome.missionapplication().Where(m => m.UserId == useridforrating && m.MissionId == missions.MissionId).FirstOrDefault();
                    var close = "0";
                    if (DateTime.Now > missions.Deadline)
                    {
                        close = "1";
                    }
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
                    var ratings = _IHome.missionRatings().Where(e => e.MissionId == missions.MissionId).ToList();
                   
                    
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
                            isFavrouite = (user != null) ? _IHome.favmission().Any(e => e.MissionId == missions.MissionId && e.UserId == user) : false,
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
                            deadline=missions.Deadline,
                            isapplied= (applicationmission != null) ? 1:0,
                            isclosed= (close == "1") ? 0:1,
                        });
                    
                    
                    
                    
                   
                }

                if (mission.Count() == 0)
                {
                    return RedirectToAction("Nomission", "Home");
                }

                
            }
            

            //------------------------------------------------------------------------------------------------------------------------------
            List<User> Alluser = _IHome.alluser().ToList();
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
                var user = _IHome.UserByUserid(item);
                var resetLink = Url.Action("Volunteering", "Home", new { user = user.UserId , missionId =id}, Request.Scheme);


                //var fromAddress = new MailAddress("tatvahl@gmail.com", "Sender Name");
                var fromAddress = new MailAddress("officehl1881@gmail.com", "Sender Name");
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
                    Credentials = new NetworkCredential("officehl1881@gmail.com", "vrbxqqayjlbvoihx"),
                   
                    EnableSsl = true
                };
                smtpClient.Send(message);
                
            }
            return Json(new { success = true });
        }
        [HttpPost]
        public async Task<IActionResult> Addrating(int rating, long Id, long missionId)
        {
            MissionRating ratingExists = _IHome.MissionratingByUserid_Missionid(Id, missionId);
            if (ratingExists != null)
            {
                ratingExists = _IHome.updaterating(ratingExists, rating);
                return Json(new { success = true, ratingExists, isRated = true });
            }
            else
            {
                var ratingele= _IHome.addratings(rating,Id,missionId);
                return Json(new { success = true, ratingele, isRated = true });
            }
        }

        [HttpPost]
        public IActionResult comment(long MissionId, long UserId,String comment)
        {

            var newcomment=_IHome.addcomment(MissionId, UserId, comment);

          


                return RedirectToAction("Volunteering", new {id= UserId, missionId = MissionId });

            //return Json(new { success = true, newcomment });



        }

        [HttpPost]
        public async Task<IActionResult> AddToFav(long MissionId, long UserId)
        {
            FavoriteMission favoriteMission = _IHome.FavmissionByMissionid_Userid(MissionId, UserId);

            if (favoriteMission != null)
            {
                FavoriteMission favoriteMissiondel = _IHome.FavmissionByMissionid_Userid(MissionId, UserId);
               var favoriteMissionde=_IHome.removefav(favoriteMission);
                return Json(new { success = true, favoriteMissiondel, isRated = false });
            }
            else
            {

               var favoriteMissionadd=_IHome.addfav(MissionId, UserId);
                return Json(new { success = true, favoriteMissionadd, isRated = true });
            }
        }

        public IActionResult Volunteering(long id,int missionId,int pageIndex=1)
        {
            int? useridforrating = HttpContext.Session.GetInt32("userIDforfavmission");
            ViewBag.userid = useridforrating;


                List<Mission> missionlist = _IHome.Allmissions();
                List<VolunteeringVM> relatedlist = new List<VolunteeringVM>();
                var mission = _IHome.Allmissions().FirstOrDefault(m => m.MissionId == missionId);
            var favmission = _IHome.FavmissionByMissionid(missionId);
                var theme = _IHome.MissionThemeByThemeid(mission.ThemeId); 
                var city = _IHome.AllCity().FirstOrDefault(s => s.CityId == mission.CityId);
                var ratings =_IHome.missionRatings().FirstOrDefault(MR => MR.MissionId == missionId && MR.UserId == useridforrating);
                var recvoldet = from U in _IHome.alluser() join MA in _IHome.missionapplication() on U.UserId equals MA.UserId where MA.MissionId == mission.MissionId select U;
                GoalMission goalMission = _IHome.goalmission().Where(gm => gm.MissionId == mission.MissionId).FirstOrDefault();
                var goaltxt =_IHome.goalmission().FirstOrDefault(g => g.MissionId == mission.MissionId);
                missionlist = missionlist.Where(t => t.ThemeId == mission.ThemeId && t.MissionId != mission.MissionId).Take(3).ToList();
                string[] startDateNtime = mission.StartDate.ToString().Split(' ');
                string[] endDateNtime = mission.EndDate.ToString().Split(' ');
            var applicationmission =_IHome.missionapplication().FirstOrDefault(m => m.UserId == useridforrating && m.MissionId==missionId);
            VolunteeringVM volunteeringMission = new();
            int finalrating = 0;
            var ratinglist = _IHome.missionRatings().Where(m => m.MissionId == mission.MissionId).ToList();
            var close = "0";
            if (DateTime.Now > mission.Deadline)
            {
                close = "1";
            }
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
                isFavrouite = (useridforrating != null) ? _IHome.favmission().Any(e => e.MissionId == mission.MissionId && e.UserId == useridforrating) : false,
                UserId = useridforrating,
                avgrating = finalrating,
                available=mission.Availability,
                isapplied = (applicationmission != null) ? 1 : 0,
                isclosed = (close == "1") ? 0 : 1,
            };
            ViewBag.fav = volunteeringMission.isFavrouite;


            List<VolunteeringVM> missioncomment = new List<VolunteeringVM>();
            var commentlist = _IHome.comment().Where(m => m.MissionId == missionId).ToList();
            foreach (var comment in commentlist)
            {
                var user= _IHome.alluser().FirstOrDefault(m => m.UserId== comment.UserId);
                missioncomment.Add(new VolunteeringVM
                {
                    commenttext = comment.CommentText,
                    UserName=user.FirstName,
                    LastName=user.LastName,
                    createdAt=comment.CreatedAt,


                });


            }
            ViewBag.missioncomment = missioncomment.OrderByDescending(m => m.createdAt).ToList(); ;
            


            var relatedmission = _IHome.Allmissions().Where(m => m.ThemeId == mission.ThemeId && m.MissionId != mission.MissionId).ToList();
                foreach (var item in relatedmission.Take(3))
                {
                    var relcity = _IHome.AllCity().FirstOrDefault(m => m.CityId == item.CityId);
                    var reltheme = _IHome.MissionThemeByThemeid(item.ThemeId);
                var relgoalobj = _IHome.goalmission().FirstOrDefault(m => m.MissionId == item.MissionId);
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
                var recentvoluntered = from U in _IHome.alluser() join MA in _IHome.missionapplication() on U.UserId equals MA.UserId where MA.MissionId == mission.MissionId select U;
                foreach (var item in recentvoluntered)
                {
                    recentvolunteredlist.Add(new VolunteeringVM
                    {
                        UserName = item.FirstName,
                    });

                }
            int pageSize = 9; // Set the page size to 9
            var volunteers = recentvolunteredlist; // Retrieve all volunteers from data source
            int totalCount = volunteers.Count(); // Get the total number of volunteers
            int skip = (pageIndex - 1) * pageSize;            var volunteersOnPage = volunteers.Skip(skip).Take(pageSize).ToList(); // Get the volunteers for the current page

            ViewBag.TotalCount = totalCount;            ViewBag.PageSize = pageSize;            ViewBag.PageIndex = pageIndex;            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);            ViewBag.recentvolunteered = volunteersOnPage;
            

                List<User> Alluser = _IHome.alluser();
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
            List<Story> story = _IHome.story();
            
            List<storyListingViewModel> storylist = new List<storyListingViewModel>();
            foreach(var item in story)
            {
                var user = _IHome.UserByUserid(item.UserId);
                var mission = _IHome.Allmissions().FirstOrDefault(m => m.MissionId == item.MissionId);
                var missiontheme= _IHome.missiontheme().FirstOrDefault(m => m.MissionThemeId == mission.ThemeId);
                
                storylist.Add(new storyListingViewModel
                {
                    UserId= user.UserId,
                    StoryTitle = item.Title,
                    Description = item.Description,
                    StoryId= item.StoryId,
                    MissionId = item.MissionId,
                    UserName = user.FirstName,
                    LastName = user.LastName,
                    Theme= missiontheme.Title,

                });
                
            }
            
            ViewBag.story= storylist;
            int pageSize = 3;
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
            var user = _IHome.UserByUserid(id);
            var story=_IHome.StoryByStoryidList(storyid);
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
            List<User> Alluser = _IHome.alluser();
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
                var user = _IHome.UserByUserid(item);
                var story = _IHome.StoryByStoryid(id);
                var resetLink = Url.Action("StoryDetail", "Home", new { id=story.UserId, storyid = id }, Request.Scheme);


                //var fromAddress = new MailAddress("tatvahl@gmail.com", "Sender Name");
                var fromAddress = new MailAddress("officehl1881@gmail.com", "Sender Name");
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
                    Credentials = new NetworkCredential("officehl1881@gmail.com", "vrbxqqayjlbvoihx"),
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
            _IHome.apply(MissionId, UserId);

            return Json(new { success = true });
        }
    }
        
    }

