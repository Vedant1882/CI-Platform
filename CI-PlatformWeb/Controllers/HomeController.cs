using CI.Models;
using CI_Entity.Models;
using CI_PlatformWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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
            return View();
        }
        public IActionResult ForgotPassword()
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
        public IActionResult landingpage()
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
                //var user = await _CIDbContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                var user = await _CIDbContext.Users.Where(u => u.Email == model.Email && u.Password == model.Password).FirstOrDefaultAsync();

                if (user != null)
                {
                    //                var claims = new List<Claim>
                    //{
                    //new Claim(ClaimTypes.Name, user.Email)
                    //};
                    //                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    //                var principal = new ClaimsPrincipal(identity);
                    //                var authProperties = new AuthenticationProperties
                    //                {
                    //                    //IsPersistent = model.RememberMe
                    //                };
                    //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
                    return RedirectToAction(nameof(HomeController.landingpage), "Home");
                }
                else
                {
                    return RedirectToAction(nameof(LoginController.Login), "Home");
                }
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}