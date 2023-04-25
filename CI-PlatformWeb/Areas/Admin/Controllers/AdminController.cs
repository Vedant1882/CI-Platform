using CI_Entity.Models;
using CI_Entity.ViewModel;
using CI_Platform.Repository.Interface;
using CI_PlatformWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;

namespace CI_PlatformWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private readonly IHomeRepository _IHome;
        private readonly CIDbContext _db;
        public AdminController(IHomeRepository IHome, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment
            , CIDbContext db)
        {
            _IHome = IHome;
            _db= db;
            _hostingEnvironment = hostingEnvironment;

        }
        public IActionResult Index()
        {

            HttpContext.Session.SetInt32("Nav", 1);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            var user = new CI_Entity.ViewModel.AdminUserViewModel();
            user.users = _IHome.alluser().ToList();
            user.allcity = _IHome.AllCity();
            user.allcountry = _IHome.allcountry();
            return View(user);

        }

        [HttpPost]
        public async Task<IActionResult> AddUser(string firstname, string lastname, string email, string password, string profiletext, string department,
            string status, string employeeid, string avatar, long userId, long cityid, long countryid)
        {
            try
            {


                HttpContext.Session.SetInt32("Nav", 4);
                ViewBag.nav = HttpContext.Session.GetInt32("Nav");
                if (userId == null || userId == 0)
                {

                    var savedUser = _IHome.AddUser(firstname, lastname, email, password, department, profiletext, status, employeeid, avatar, cityid, countryid);
                    if (savedUser.Email == email)
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {

                    var savedUser = _IHome.UpdateUser(firstname, lastname, email, password, department, profiletext, status, employeeid, avatar, cityid, countryid, userId);
                    return RedirectToAction("Index");
                }

                var uservm = new CI_Entity.ViewModel.AdminUserViewModel();
                uservm.users = _IHome.alluser().ToList();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new {area="Employee"});
            }
        }

        [HttpPost]
        public IActionResult GetUser(long userId)
        {
            try
            {


                var user = _IHome.UserByUserid(userId);
                var userVM = new CI_Entity.ViewModel.AdminUserViewModel();
                userVM.users = _IHome.alluser().ToList();
                userVM.allcity = _IHome.AllCity();
                userVM.allcountry = _IHome.allcountry();
                userVM.firstname = user.FirstName;
                userVM.lastname = user.LastName;
                userVM.email = user.Email;
                userVM.avatar = user.Avatar;
                userVM.status = user.Status;
                userVM.cityid = user.CityId;
                userVM.employeeid = user.EmployeeId;
                userVM.countryid = user.CountryId;
                userVM.profiletext = user.ProfileText;
                userVM.department = user.Department;
                return View("Index", userVM);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { area = "Employee" });
            }
        }

        [HttpPost]
        public IActionResult DeleteUser(long userId)
        {
            _IHome.DeleteUser(userId);
            var user = new CI_Entity.ViewModel.AdminUserViewModel();
            user.users = _IHome.alluser().ToList();
            user.allcity = _IHome.AllCity();
            user.allcountry = _IHome.allcountry();
            return RedirectToAction("Index");
        }
        public IActionResult AdminCms()
        {
            HttpContext.Session.SetInt32("Nav", 2);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            var cms = new CI_Entity.ViewModel.AdminCmsPageVM();
            cms.CmsPages = _IHome.GetCmsPages();
            return View(cms);
        }

        [HttpPost]
        public IActionResult AddCms(CI_Entity.ViewModel.AdminCmsPageVM model)
        {
            try
            {

                if (model.CmsPageId == 0 || model.CmsPageId == null)
                {
                    _IHome.AddCms(model);

                }
                else
                {
                    _IHome.UpdateCms(model);
                }
                return RedirectToAction("AdminCms");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { area = "Employee" });
            }
        }


        [HttpPost]
        public IActionResult GetCms(long CmsPageId)
        {
            var page = _IHome.GetCmsPages(CmsPageId);
            return View("AdminCms", page);
        }

        [HttpPost]
        public IActionResult DeleteCms(long CmsPageId)
        {
            _IHome.Deletecms(CmsPageId);
            return RedirectToAction("AdminCms");
        }



        public IActionResult AdminMission()
        {
            HttpContext.Session.SetInt32("Nav", 3);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            var missionvm=new AdminMissionViewModel();
            missionvm.missions=_IHome.Allmissions();
            missionvm.countries=_IHome.allcountry();
            missionvm.cities=_IHome.AllCity();
            missionvm.themes=_IHome.alltheme();
            missionvm.AllSkills = _IHome.AllSkills();

            //var skills = _IHome.MissionSkilljoinSkill();
            //var uskills = skills.Where(e => e.MissionId == ).ToList();
            //ViewBag.userskills = uskills;
            //foreach (var skill in uskills)
            //{
            //    var rskill = allskills.FirstOrDefault(e => e.SkillId == skill.SkillId);
            //    allskills.Remove(rskill);
            //}
            //ViewBag.remainingSkills = allskills;
            return View(missionvm);
        }
        
        [HttpPost]
        public IActionResult AddMission(AdminMissionViewModel model)
        {
            if (model.missionId == null || model.missionId == 0)
            {
                var files = Request.Form.Files;
                _IHome.AddMission(model,files);
                
            }
            else
            {
                var files = Request.Form.Files;
                var savedUser = _IHome.UpdateMission(model,files);
                return RedirectToAction("AdminMission");
            }

            var missionvm = new AdminMissionViewModel();
            missionvm.missions = _IHome.Allmissions();
            missionvm.countries = _IHome.allcountry();
            missionvm.cities = _IHome.AllCity();
            missionvm.themes = _IHome.alltheme();

            return RedirectToAction("AdminMission");
        }
        [HttpPost]
        public IActionResult GetMission(long missionId)
        {
            var mission = _IHome.Allmissions().FirstOrDefault(t => t.MissionId == missionId && t.Status=="1" && t.DeletedAt==null);
            var goalmission=_IHome.goalmission().FirstOrDefault(g=>g.MissionId==missionId);
            var allskills= _IHome.AllSkills();
            var skillsJoin = _IHome.MissionSkilljoinSkill();
            var missionskill = skillsJoin.Where(m=>m.MissionId==missionId).ToList();
            foreach (var skill in missionskill)
            {
                var rskill = allskills.FirstOrDefault(e => e.SkillId == skill.SkillId);
                allskills.Remove(rskill);
            }
            var finalurl = "";
            var VideoURLs = _IHome.allmedia().Where(e => e.MissionId == missionId && e.MediaType == "Video").ToList();
            foreach (var videoURL in VideoURLs)
            {
                finalurl = finalurl + videoURL.MediaPath + "\r\n";
            }
            var missionVm = new AdminMissionViewModel();
            missionVm.missions = _IHome.Allmissions().Where(t => t.Status == "1" && t.DeletedAt == null).ToList();
            missionVm.title= mission.Title;
            mission.MissionId = mission.MissionId;
            missionVm.editor2 = mission.Description;
            missionVm.shortdescription = mission.ShortDescription;
            missionVm.timeavailability = mission.AvailabilityTime;
            missionVm.startDate=mission.StartDate;
            missionVm.endDate=mission.EndDate;
            missionVm.deadline=mission.Deadline;
            missionVm.cityId=mission.CityId;
            missionVm.countryId=mission.CountryId;
            missionVm.themeId=mission.ThemeId;
            missionVm.organizationDetail = mission.OrganizationDetail;
            missionVm.organizationName = mission.OrganizationName;
            missionVm.totalseats = mission.Availability;
            missionVm.goalValue = goalmission.GoalValue;
            missionVm.goalObjectiveText = goalmission.GoalObjectiveText;
            missionVm.missionType=mission.MissionType;
            missionVm.countries = _IHome.allcountry();
            missionVm.cities = _IHome.AllCity();
            missionVm.themes = _IHome.alltheme();
            missionVm.AllSkills = _IHome.AllSkills();
            missionVm.MissionSkill = missionskill;
            missionVm.RemainingSkills = allskills;
            missionVm.url = finalurl;
            missionVm.ImageFiles = new List<MissionMedium>();
            missionVm.DocFiles = new List<IFormFile>();
            var imgfiles = _db.MissionMedia.Where(m => m.MissionId == missionId && m.MediaType != "Video" && m.DeletedAt == null).ToList();
            var docfiles = _db.MissionDocuments.Where(m => m.MissionId == missionId && m.DeletedAt == null).ToList();
            int i = 1;
            if (imgfiles.Count > 0)
            {
                foreach (var ifile in imgfiles)
                {
                    missionVm.ImageFiles.Add(ifile);
                }
            }
            i = 0;
            foreach (var ifile in docfiles)
            {
                i++;
                string fileName = "example" + i + "." + ifile.DocumentType;  // specify the file name and extension
                string contentType =  ifile.MissionDocumentId.ToString();
                MemoryStream ms = new MemoryStream(ifile.DocumentPath);
                var file = new FormFile(ms, 0, ms.Length, contentType, fileName);
                missionVm.DocFiles.Add(file);
            }

            //Request.Form.Files= missionVm.ImageFiles;
            return View("AdminMission", missionVm);
        }
        [HttpPost]
        public IActionResult DeleteMission(long missionId)
        {
            _IHome.DeleteMission(missionId);
            var missionvm = new AdminMissionViewModel();
            missionvm.missions = _IHome.Allmissions();
            missionvm.countries = _IHome.allcountry();
            missionvm.cities = _IHome.AllCity();
            missionvm.themes = _IHome.alltheme();
            return RedirectToAction("AdminMission");
        }
        [HttpPost]
        public IActionResult delDoc(string docId)
        {
            _IHome.delDoc(long.Parse(docId));
            return Json(new {success = true});
        }

        [HttpPost]
        public IActionResult delImg(long imgId)
        {
            _IHome.delImg(imgId);
            return Json(new { success = true });
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
            var applicationsList = _IHome.GetPendingMissionApplications().ToList();
            return View(applicationsList);
        }




        [HttpPost]
        public IActionResult ApproveApplication(long MaId, string status)
        {
            _IHome.Approveapplication(MaId, status);
            return RedirectToAction("AdminMissionApplication");
        }

        public IActionResult AdminStory()
        {
            HttpContext.Session.SetInt32("Nav", 7);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            var strories = _IHome.GetPendingStories().ToList();
            return View(strories);
        }

        [HttpPost]
        public IActionResult ApproveStory(long MaId, string status)
        {
            _IHome.Approvestory(MaId, status);
            return RedirectToAction("AdminStory");
        }

        [HttpPost]
        public IActionResult DeleteStory(long storyId)
        {
            _IHome.DeleteStory(storyId);
          
            return RedirectToAction("AdminStory");
        }




        public IActionResult AdminBannerManagement()
        {
            HttpContext.Session.SetInt32("Nav", 8);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            AdminBannerViewModel bannerVm= new AdminBannerViewModel();
            bannerVm.banner = _IHome.AllBanners();
            return View(bannerVm);
        }

        [HttpPost]
        public IActionResult AddBanner(string discrption,string image,int sortorder,long bannerId)
        {
            try
            {

                if (bannerId == 0 || bannerId == null)
                {
                    _IHome.AddBanner(discrption,image,sortorder);

                }
                else
                {
                    _IHome.UpdateBanner(discrption, image, sortorder,bannerId);
                }
                return RedirectToAction("AdminBannerManagement");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { area = "Employee" });
            }
        }
        [HttpPost]
        public IActionResult GetBanner(long bannerId)
        {
            var bannerlist = _IHome.AllBanners().FirstOrDefault(t => t.BannerId == bannerId);
            var banner = new AdminBannerViewModel();
            banner.img = bannerlist.Image;
            banner.BannerText = bannerlist.Text;
            banner.BannerSortOrder = bannerlist.SortOrder;
            banner.BannerId = bannerId;
            banner.banner = _IHome.AllBanners();
            return View("AdminBannerManagement", banner);
        }
        [HttpPost]
        public IActionResult DeleteBanner(long bannerId)
        {
            _IHome.DeleteBanner(bannerId);

            return RedirectToAction("AdminBannerManagement");
        }
    }
}