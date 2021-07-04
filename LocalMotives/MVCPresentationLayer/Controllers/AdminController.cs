using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataObject;
using LogicLayer;
using LogicLayerUtilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using MVCPresentationLayer.Models;

namespace MVCPresentationLayer.Controllers
{
    
    public class AdminController : Controller
    {

        private ApplicationUserManager userManager;
        private IUserManager _userManager;

        public AdminController()
        {
            _userManager = new UserManager();
        }

        // GET: Admin
        [Authorize]
        public ActionResult Index()
        {
            if (_userManager.FindUser(User.Identity.Name))
            {
                List<string> roles = _userManager.RetrieveEmployeeRoles(_userManager.RetrieveUserIDFromEmail(User.Identity.Name));
                if (roles.Contains("Administrator"))
                {
                    userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    return View(userManager.Users.OrderBy(n => n.FamilyName).ToList());
                }
                else
                {
                    return Redirect("~/");
                }
            }
            else
            {
                return Redirect("~/");
            }

        }

        // GET: Admin/Details/5
        [Authorize]
        public ActionResult Details(string id)
        {
            ViewBag.Title = "Details";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser applicationUser = userManager.FindById(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            if (_userManager.FindUser(User.Identity.Name))
            {
                var allRoles = _userManager.RetrieveAllEmployeeRoles();

                var roles = _userManager.RetrieveEmployeeRoles(_userManager.RetrieveUserIDFromEmail(User.Identity.Name));
                var noRoles = allRoles.Except(roles);

                ViewBag.Roles = roles;
                ViewBag.NoRoles = noRoles;
                User user = _userManager.GetUserByEmail(User.Identity.Name);
                user.PhoneNumber = PhoneNumberManager.PhoneNumberFixer(user.PhoneNumber);
                user.PhoneNumber = PhoneNumberManager.FormatPhoneNumberForOutput(user.PhoneNumber);
                ViewBag.User = user;
                applicationUser.PhoneNumber = user.PhoneNumber;




                return View(applicationUser);
            }
            else
            {
                return Redirect("~/");
            }



        }

        [Authorize]
        public ActionResult RemoveRole(string email, string role)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = _userManager.GetUserByEmail(email);

            if (role == "Administrator")
            {
                int adminUsers = _userManager.CountAdmin();
                if (adminUsers < 2)
                {
                    ViewBag.Error = "Cannot remove last administrator.";
                    return RedirectToAction("Details", "Admin", new { id = userManager.FindByEmail(email).Id });
                }
            }
            //userManager.RemoveFromRole(id, role);

            if (_userManager.FindUser(user.Email))
            {
                try
                {
                    _userManager.DeleteUserRole(user.EmployeeID, role);
                }
                catch (Exception)
                {
                    // do nothing
                }
            }
            return RedirectToAction("Details", "Admin", new { id = userManager.FindByEmail(email).Id });
        }

        [Authorize]
        public ActionResult AddRole(string email, string role)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = _userManager.GetUserByEmail(email);


            //userManager.AddToRole(id, role);

            if (_userManager.FindUser(email))
            {
                try
                {
                    _userManager.InsertUserRole(user.EmployeeID, role);
                }
                catch (Exception)
                {
                    // do nothing
                }
            }
            return RedirectToAction("Details", "Admin", new { id = userManager.FindByEmail(email).Id });
        }
    }
}
