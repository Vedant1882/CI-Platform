using CI_Entity.Models;
using CI_Platform.Repository.Interface;
using CI_PlatformWeb.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CI.Controllers
{
    public class UserController : Controller
    {

        private readonly CIDbContext _CIDbContext;
        private readonly IUserRepository _IUser;
        public UserController(CIDbContext CIDbContext, IUserRepository IUser)
        {
            _IUser = IUser;
            _CIDbContext = CIDbContext;


        }
        public IActionResult Index()
        {
            List<User> Users = _CIDbContext.Users.ToList();
            return View(Users);
        }



        public IActionResult Register()
        {
            User user = new User();
            return View(user);
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            //var obj = _IUser.UserExist(user.Email);




            if (_IUser.UserExist(user))
            {
                
                return RedirectToAction("Login", "Home");
                
            }
            else
            {
                ViewBag.RegEmail = "email exist";

            }
            return View();
        }

      
    }
}