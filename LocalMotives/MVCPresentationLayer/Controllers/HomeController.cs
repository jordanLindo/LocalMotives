using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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