using DataObject;
using LogicLayer;
using LogicLayerUtilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace MVCPresentationLayer.Controllers
{
    [Authorize(Roles = "Manager")]

    public class TrainManagementController : Controller
    {
        private ITrainScheduleManager _trainScheduleManager;
        private IStationManager _stationManager;
        private ITrainManager _trainManager;
        private ISeatManager _seatManager;

        public TrainManagementController()
        {
            _trainScheduleManager = new TrainScheduleManager();
            _stationManager = new StationManager();
            _trainManager = new TrainManager();
            _seatManager = new SeatManager();
        }

        // GET: TrainManagement
        public ActionResult Index()
        {
            ViewBag.Title = "Schedule Admin";
            var schedule = _trainScheduleManager.GetTrainScheduleLinesByTrainScheduleID(
                _trainScheduleManager.GetTrainSchedule().TrainScheduleID);

            return View(schedule);
        }

        // GET: TrainManagement/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Generate New Schedule";
            ViewBag.Today = DateTime.Now.ToShortDateString();
            ViewBag.Hours = ListManager.GetTimes();
            return View();
        }


        // POST: TrainManagement/Create
        [HttpPost]
        public ActionResult Create(TrainSchedule schedule)
        {
            DateTime startTime = DateTime.Parse(schedule.StartTime);
            DateTime endTime = DateTime.Parse(schedule.EndTime);
            
            if (ModelState.IsValid && endTime.CompareTo(startTime) > 0)
            {
                try
                {
                    var userID = User.Identity.GetUserId();
                    var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var user = userManager.Users.First(u => u.Id == userID);
                    int employeeID = (int)user.EmployeeID;
                    TrainSchedule newSchedule = 
                        _trainScheduleManager.GetNewTrainSchedule(employeeID, schedule.StartDate);
                    newSchedule.TrainScheduleID = _trainScheduleManager.AddTrainSchedule(newSchedule);
                    int mins = (int)endTime.Subtract(startTime).TotalMinutes;
                    List<TrainScheduleLine> lines = _trainScheduleManager.GenerateTrainSchedule(mins, newSchedule.TrainScheduleID, startTime);
                    foreach (TrainScheduleLine line in lines)
                    {
                        _trainScheduleManager.AddTrainScheduleLine(line);
                    }
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        // GET: TrainManagement/TrainEdit/5
        public ActionResult TrainEdit(int id)
        {
            TrainVM train = _trainManager.GetTrainByID(id);
            ViewBag.Title = "Edit Train";
            return View(train);
        }

        // POST: TrainManagement/TrainEdit/5
        [HttpPost]
        public ActionResult TrainEdit(int id, TrainVM trainVM)
        {
            if (ModelState.IsValid)
                try
                {
                    TrainVM old = _trainManager.GetTrainByID(id);
                    _trainManager.EditTrain(old, trainVM);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            return View();
        }

        // GET: TrainManagement/StationEdit
        public ActionResult StationEdit(int stationID)
        {
            Station station = _stationManager.GetStationByID(stationID);
            ViewBag.Title = "Edit Station";
            ViewBag.Types = _stationManager.GetStationTypes();
            return View(station);
        }

        // POST: TrainManagement/StationEdit
        [HttpPost]
        public ActionResult StationEdit(int stationID, Station station)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Station old = _stationManager.GetStationByID(stationID);
                    _stationManager.EditStation(old, station);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        // GET: TrainManagement/TrainCarsList
        public ActionResult TrainCarsList(int trainID)
        {
            List<TrainCarVM> cars = _trainManager.GetAllTrainCarVMsByTrainID(trainID);
            ViewBag.Title = "Train Cars";
            ViewBag.TrainID = trainID;
            ViewBag.SeatManager = _seatManager;
            return View(cars);
        }

        // GET: TrainManagement/TrainCarDetails
        public ActionResult TrainCarDetails(int trainCarID, int trainID)
        {
            List<TrainCarVM> cars = _trainManager.GetAllTrainCarVMsByTrainID(trainID);
            bool found = false;
            TrainCarVM trainCar = null;
            for (int i = 0; i < cars.Count && !found; i++)
            {
                if(cars[i].TrainCarID == trainCarID)
                {
                    trainCar = cars[i];
                    found = true;
                }
            }
            
            ViewBag.Title = "Train Car Details";
            ViewBag.TrainID = trainID;
            return View(trainCar);

        }

        // GET: TrainManagement/DeleteTrainCar
        public ActionResult DeleteTrainCar(int trainCarID, int trainID)
        {
            List<TrainCarVM> cars = _trainManager.GetAllTrainCarVMsByTrainID(trainID);
            bool found = false;
            TrainCarVM trainCar = null;
            for (int i = 0; i < cars.Count && !found; i++)
            {
                if (cars[i].TrainCarID == trainCarID)
                {
                    trainCar = cars[i];
                    found = true;
                }
            }
            ViewBag.Title = "Delete Train Car";
            return View(trainCar);
        }

        // POST: TrainManagement/DeleteTrainCar
        [HttpPost]
        public ActionResult DeleteTrainCar(int trainCarID, int trainID,
            FormCollection collection)
        {
            try
            {
                _trainManager.DeactivateTrainCar(trainCarID);
                return RedirectToAction("TrainCarsList" , new { trainID = trainID });
            }
            catch
            {
                return View();
            }
        }
    }
}