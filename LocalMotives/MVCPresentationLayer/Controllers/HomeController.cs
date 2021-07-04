using DataObject;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        private IUserManager _userManager;

        public HomeController()
        {
            _userManager = new UserManager();
        }

        public ActionResult Index()
        {
            ViewBag.Roles = new List<string>();
            if(User.Identity.Name != "" && _userManager.FindUser(User.Identity.Name))
            {
                User user = _userManager.GetUserByEmail(User.Identity.Name);
                List<string> roles = _userManager.RetrieveEmployeeRoles(user.EmployeeID);

                ViewBag.Roles = roles;
            }
            ViewBag.Title = "Localmotives";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "View train schedules.";
            ViewBag.Title = "About";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page is blank for now.";
            ViewBag.Title = "Contact";

            return View();
        }

        public ActionResult Error()
        {
            ViewBag.Title = "Error";
            ViewBag.Message = "An error occured please try again later.";
            return View();
        }
    }
}