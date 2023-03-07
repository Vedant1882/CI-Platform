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

namespace CI_PlatformWeb.Controllers
{
    public class HomeController : Controller
    {
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
        public IActionResult Volunteering()
        {
            return View();
        }

        public IActionResult NoMissionFound()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult storyListing()
        {
            return View();
        }
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
        //public IActionResult Landingpage()
        //{
        //    return View();
        //}

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model)
        {

            if (ModelState.IsValid)
            {
               
                var user = await _CIDbContext.Users.Where(u => u.Email == model.Email && u.Password == model.Password).FirstOrDefaultAsync();
                var firstname = model.Email.Split("@")[0];
                if (user != null)
                {
                    HttpContext.Session.SetString("UserID", firstname);
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
        
        public ActionResult landingpage(int? pageIndex, string searchQuery, long[] ACountries, long[] ACities, long[] ATheme)
        {
            
            List<Mission> mission = _CIDbContext.Missions.ToList();
            foreach (var item in mission)
            {
                var City = _CIDbContext.Cities.FirstOrDefault(u => u.CityId == item.CityId);
                var Theme = _CIDbContext.MissionThemes.FirstOrDefault(u => u.MissionThemeId == item.ThemeId);

            }
            mission = _CIDbContext.Missions.ToList();
            List<Country> Countries = _CIDbContext.Countries.ToList();
            List<Country> countryElements = _CIDbContext.Countries.ToList();
            List<City> Cities = _CIDbContext.Cities.ToList();
            List<MissionTheme>Themes = _CIDbContext.MissionThemes.ToList();
            List<Skill> skills= _CIDbContext.Skills.ToList();
            ViewBag.countries = Countries;
            ViewBag.themes = Themes;
            ViewBag.cities=Cities;
            ViewBag.skills = skills;
            if (!string.IsNullOrEmpty(searchQuery))
            {
                mission = mission.Where(m => m.Title.ToUpper().Contains(searchQuery.ToUpper())).ToList();
                ViewBag.searchQuery = Request.Query["searchQuery"];
                if (mission.Count() == 0)
                {
                    return RedirectToAction("NoMissionFound","Home");
                }
            }
            
            if (ACountries != null && ACountries.Length > 0)
            {
                foreach (var country in ACountries)
                {
                    mission = mission.Where(m => m.CountryId == country).ToList();
                    if (mission.Count() == 0)
                    {
                        return RedirectToAction("NoMissionFound", "Home");
                    }
                    ViewBag.country = country;
                    if (ViewBag.country != null)
                    {
                        var c1 = _CIDbContext.Countries.FirstOrDefault(m => m.CountryId == country);
                        ViewBag.country =c1.Name;
                    }
                }
                Countries = _CIDbContext.Countries.ToList();
                
                
            }
            if (ACities != null && ACities.Length > 0)
            {
                foreach (var city in ACities)
                {
                    mission = mission.Where(m => m.CityId == city).ToList();
                    if (mission.Count() == 0)
                    {
                        return RedirectToAction("NoMissionFound", "Home");
                    }
                    ViewBag.city = city;
                    if (ViewBag.city != null)
                    {
                        var c1 = _CIDbContext.Cities.FirstOrDefault(m => m.CityId == city);
                        ViewBag.city = c1.Name;
                    }
                }
                Cities = _CIDbContext.Cities.ToList();
                
                
            }
            if (ATheme != null && ATheme.Length > 0)
            {
                foreach (var theme in ATheme)
                {
                    mission = mission.Where(m => m.ThemeId == theme).ToList();
                    if (mission.Count() == 0)
                    {
                        return RedirectToAction("NoMissionFound", "Home");
                    }
                    ViewBag.theme = theme;
                    if (ViewBag.theme != null)
                    {
                        var c1 = _CIDbContext.MissionThemes.FirstOrDefault(m => m.MissionThemeId == theme);
                        ViewBag.theme = c1.Title;
                    }
                }
                Themes = _CIDbContext.MissionThemes.ToList();
                
                
            }
            int pageSize =3;
            int skip = (pageIndex ?? 0) * pageSize;
            var Missions = mission.Skip(skip).Take(pageSize).ToList();
            int totalMissions = mission.Count();
            ViewBag.TotalMission = totalMissions;
            ViewBag.TotalPages = (int)Math.Ceiling(totalMissions / (double)pageSize);
            ViewBag.CurrentPage = pageIndex ?? 0;
            


            return View(Missions);
        }







        // GET: ForgetController


        // GET: ForgetController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ForgetController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ForgetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ForgetController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ForgetController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ForgetController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ForgetController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}