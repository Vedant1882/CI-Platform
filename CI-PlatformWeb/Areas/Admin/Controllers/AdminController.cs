using CI_Entity.Models;
using CI_Platform.Repository.Interface;
using CI_PlatformWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private readonly IHomeRepository _IHome;
        public AdminController(IHomeRepository IHome, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _IHome = IHome;

            _hostingEnvironment = hostingEnvironment;

        }
        public IActionResult Index()
        {
            HttpContext.Session.SetInt32("Nav", 1);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            var user = new AdminUserViewModel();
            user.users = _IHome.alluser().ToList();
            user.allcity=_IHome.AllCity();
            user.allcountry=_IHome.allcountry();
            return View(user);
            
        }
        [HttpPost]
        public IActionResult AddUser(AdminUserViewModel model)
        {
            HttpContext.Session.SetInt32("Nav", 4);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            if (model.userId == null || model.userId == 0)
            {
                _IHome.AddUser(model.firstname,model.lastname,model.email,model.password,model.department,model.status,model.employeeid);
            }
            else
            {
                //_IHome.UpdateTheme(model.themeName, model.themeId);
            }

            var uservm = new AdminUserViewModel();
            uservm.users = _IHome.alluser().ToList();
            
            return RedirectToAction("Index");
        }
        public IActionResult AdminCms()
        {
            HttpContext.Session.SetInt32("Nav", 2);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            return View();
        }
        public IActionResult AdminMission()
        {
            HttpContext.Session.SetInt32("Nav", 3);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            return View();
        }
        public IActionResult AdminTheme()
        {
            HttpContext.Session.SetInt32("Nav", 4);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            var themevm = new AdminThemeViewModel();
            themevm.missionThemes = _IHome.alltheme().Where(t => t.Status == 1).ToList();
            return View(themevm);
        }
        [HttpPost]
        public IActionResult AddTheme(AdminThemeViewModel model)
        {
            HttpContext.Session.SetInt32("Nav", 4);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            if(model.themeId == null || model.themeId == 0)
            {
                _IHome.AddTheme(model.themeName);
            }
            else
            {
                _IHome.UpdateTheme(model.themeName,model.themeId);
            }
            
            var themevm = new AdminThemeViewModel();
            themevm.missionThemes = _IHome.alltheme().Where(t => t.Status == 1).ToList();
            themevm.themeName = "";
            return RedirectToAction("AdminTheme");
        }

        [HttpPost]
        public IActionResult GetTheme(long themeId)
        {
            var theme = _IHome.alltheme().FirstOrDefault(t => t.MissionThemeId == themeId && t.Status==1);
            var themevm = new AdminThemeViewModel();
            themevm.missionThemes = _IHome.alltheme().Where(t=>t.Status == 1).ToList();
            themevm.themeName = theme.Title;
            themevm.themeId= theme.MissionThemeId;
            return View("AdminTheme", themevm);
        }
        [HttpPost]
        public IActionResult deleteTheme(long themeId)
        {
            _IHome.DeleteTheme(themeId);
            var themevm = new AdminThemeViewModel();
            themevm.missionThemes = _IHome.alltheme().Where(t => t.Status == 1).ToList();
            themevm.themeName = "";
            return RedirectToAction("AdminTheme");
        }
            
        public IActionResult AdminSkills()
        {
            HttpContext.Session.SetInt32("Nav", 5);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            var skill = new AdminSkillViewModel();
            skill.skill = _IHome.AllSkills().Where(t => t.Status == "1").ToList();

            return View(skill);
            
        }

        [HttpPost]
        public IActionResult AddSkill(AdminSkillViewModel model)
        {
            HttpContext.Session.SetInt32("Nav", 5);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            if (model.skillId == null || model.skillId == 0)
            {
                _IHome.AddSkill(model.skillName);
            }
            else
            {
                _IHome.UpdateSkill (model.skillName, model.skillId);
            }

            var skill = new AdminSkillViewModel();
            skill.skill = _IHome.AllSkills().Where(t => t.Status == "1").ToList();
            skill.skillName = "";
            return RedirectToAction("AdminSkills");

        }
        [HttpPost]
        public IActionResult GetSkill(long skillId)
        {
            var skilllist = _IHome.AllSkills().FirstOrDefault(t => t.SkillId==skillId && t.Status == "1");
            var skill = new AdminSkillViewModel();
            skill.skill = _IHome.AllSkills().Where(t => t.Status == "1").ToList();
            skill.skillName= skilllist.SkillName;
            skill.skillId = skilllist.SkillId;
            return View("AdminSkills", skill);
        }
        [HttpPost]
        public IActionResult DeleteSkill(long skillId)
        {
            _IHome.DeleteSkill(skillId);
            var skillvm = new AdminSkillViewModel();
            skillvm.skill = _IHome.AllSkills().Where(t => t.Status == "1").ToList();
            skillvm.skillName = "";
            return RedirectToAction("AdminSkills");
        }
        public IActionResult AdminMissionApplication()
        {
            HttpContext.Session.SetInt32("Nav", 6);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            return View();
        }

        public IActionResult AdminStory()
        {
            HttpContext.Session.SetInt32("Nav", 7);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            return View();
        }

        




        public IActionResult AdminBannerManagement()
        {
            HttpContext.Session.SetInt32("Nav", 8);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            return View();
        }




    }
}