using DataObject;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCPresentationLayer.Models;
using Microsoft.AspNet.Identity;
using System.Globalization;

namespace MvcPresentationLayer.Controllers
{
    [HandleError(ExceptionType = typeof(ApplicationException),
        View = "Error")]
    public class TrainScheduleController : Controller
    {

        private ITrainScheduleManager _trainScheduleManager;
        private IStationManager _stationManager;
        private ITrainManager _trainManager;
        private IUserManager _userManager;

        public TrainScheduleController()
        {
            _trainScheduleManager = new TrainScheduleManager();
            _stationManager = new StationManager();
            _trainManager = new TrainManager();
            _userManager = new UserManager();
        }

        // GET: TrainSchedule
        public ActionResult Index()
        {
            ViewBag.Title = "Schedule";
            List<string> userRoles = new List<string>();

            if (User.Identity.Name != "")
            {
                userRoles = _userManager.RetrieveEmployeeRoles(_userManager.RetrieveUserIDFromEmail(User.Identity.Name));
            }
            ViewBag.Roles = userRoles;

            var schedule = _trainScheduleManager.GetTrainScheduleLinesByTrainScheduleID(
                _trainScheduleManager.GetTrainSchedule().TrainScheduleID);

            var sortedLines = from l in schedule
                              orderby DateTime.Parse(l.ArrivalTime)
                              select l;

            if (schedule != null)
            {
                return View(sortedLines.ToList());
            }
            else
            {
                return View();
            }

        }


        [Authorize]
        // GET: TrainSchedule/CreateNewTrainSchedule
        public ActionResult CreateNewTrainSchedule()
        {
            if(User.Identity.Name != "")
            {
                List<string> roles = _userManager.RetrieveEmployeeRoles(
                    _userManager.RetrieveUserIDFromEmail(User.Identity.Name));
                if (roles.Contains("Manager"))
                {
                    ViewBag.Title = "Create New Train Schedule";
                    return View();
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }


        [Authorize]
        // POST: TrainSchedule/CreateNewTrainSchedule
        [HttpPost]
        public ActionResult CreateNewTrainSchedule(TrainSchedule trainSchedule)
        {
            trainSchedule.EmployeeID = _userManager.RetrieveUserIDFromEmail(User.Identity.Name);

            trainSchedule.StartDate = DateTime.Parse(DateTime.Now.ToShortDateString());

            DateTime startTime = DateTime.ParseExact(trainSchedule.StartTime, "HH:mm", CultureInfo.InvariantCulture);
            DateTime endTime = DateTime.ParseExact(trainSchedule.EndTime, "HH:mm", CultureInfo.InvariantCulture);

            TimeSpan spanToStart = startTime.Subtract(trainSchedule.StartDate);

            int minutesToStart = Convert.ToInt32(spanToStart.TotalMinutes);
            trainSchedule.StartDate = trainSchedule.StartDate.AddMinutes(minutesToStart);

            try
            {
                if (0 < trainSchedule.EndTime.CompareTo(trainSchedule.StartTime))
                {
                    TrainSchedule newSchedule = _trainScheduleManager.GetNewTrainSchedule(trainSchedule.EmployeeID,
                        trainSchedule.StartDate);
                    newSchedule.StartTime = trainSchedule.StartTime;
                    newSchedule.EndTime = trainSchedule.EndTime;
                    newSchedule.TrainScheduleID = _trainScheduleManager.AddTrainSchedule(newSchedule);
                    TimeSpan span = endTime.Subtract(startTime);
                    int minutes = Convert.ToInt32(span.TotalMinutes);
                    List<TrainScheduleLine> lines = _trainScheduleManager.GenerateTrainSchedule(minutes,
                        newSchedule.TrainScheduleID, newSchedule.StartDate);
                    foreach (var line in lines)
                    {
                        _trainScheduleManager.AddTrainScheduleLine(line);
                    }

                    trainSchedule = newSchedule;

                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: TrainSchedule/StationScheduleDetails/5
        public ActionResult StationScheduleDetails(int id)
        {
            ViewBag.Title = "Station Schedule Detail";
            ViewBag.User = _userManager.GetUserByEmail(User.Identity.Name);
            var lines = _trainScheduleManager.GetTrainScheduleLinesByStationID(id);
            return View(lines);
        }

        // GET: TrainSchedule/StationDetails/5
        public ActionResult StationDetails(int id)
        {
            ViewBag.Title = "Station Detail";
            var station = _stationManager.GetStationByID(id);
            return View(station);
        }

        // GET: TrainSchedule/TrainDetails/5
        public ActionResult TrainDetails(int id, string name, int route)
        {
            ViewBag.Title = "Train Detail";
            ViewBag.User = _userManager.GetUserByEmail(User.Identity.Name);
            TrainVM train = new TrainVM
            {
                TrainID = id,
                TrainName = name,
                RouteID = route,
                TrainCars = _trainManager.GetAllTrainCarVMsByTrainID(id)
            };
            return View(train);
        }

        // GET: TrainSchedule/RouteScheduleDetails/5
        public ActionResult RouteScheduleDetails(int id)
        {
            ViewBag.Title = "Route Schedule Detail";
            var lines = _trainScheduleManager.GetTrainScheduleLinesByRouteID(id);
            return View(lines);
        }

    }
}
