using CI_Entity.Models;
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



        public IActionResult Create()
        {
            User user = new User();
            return View(user);
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            _CIDbContext.Users.Add(user);
            _CIDbContext.SaveChanges();
            return RedirectToAction("Login", "Home");
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