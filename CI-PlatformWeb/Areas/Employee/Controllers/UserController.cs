﻿using CI_Entity.Models;
using CI_PlatformWeb.Models;
using CI_Platform.Repository.Interface;

using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Employee.Controllers
{
    [Area("Employee")]
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
            ViewBag.firstBanner = _IUser.AllBanners().Where(e => e.SortOrder == 1).ToList();
            ViewBag.Banners = _IUser.AllBanners().OrderBy(e => e.SortOrder).ToList().Skip(1);
            //User user = new User();
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegistrationViewModel user)
        {
            //var obj = _IUser.UserExist(user.Email);



            if (ModelState.IsValid)
            {
                if (_IUser.UserExist(user.FirstName, user.LastName, user.Email, user.PhoneNumber, user.ConfirmPassword))
                {
                    TempData["reg"] = "Registration Done Successfully";
                    return RedirectToAction("Login", "Home");

                }
                else
                {
                    ViewBag.RegEmail = "email exist";

                }

            }
            return View();
        }


    }
}