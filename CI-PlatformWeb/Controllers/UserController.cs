using CI_Entity.Models;
using CI_PlatformWeb.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CI.Controllers
{
    public class UserController : Controller
    {

        private readonly CIDbContext _CIDbContext;

        public UserController(CIDbContext CIDbContext)
        {
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
            var obj = _CIDbContext.Users.FirstOrDefault(u => u.Email == user.Email);
            
            
            
            if (obj == null)
            {
                _CIDbContext.Users.Add(user);
                _CIDbContext.SaveChanges();
                return RedirectToAction("Login", "Home");
                
            }
            else
            {
                ViewBag.RegEmail = "email exist";

            }
            return View();
        }

        //public IActionResult Edit(int? id)
        //{
        //    Users user = _testingContext.Users.Where(x => x.Id == id).FirstOrDefault();
        //    return View(user);
        //}

        //[HttpPost]
        //public IActionResult Edit(Users user)
        //{
        //    _testingContext.Users.Update(user);
        //    _testingContext.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //public IActionResult Delete(int? id)
        //{
        //    Users user = _testingContext.Users.Where(x => x.Id == id).FirstOrDefault();
        //    return View(user);
        //}

        //[HttpPost]
        //public IActionResult Delete(Users user)
        //{
        //    _testingContext.Users.Remove(user);
        //    _testingContext.SaveChanges();
        //    return RedirectToAction("index");
        //}
    }
}