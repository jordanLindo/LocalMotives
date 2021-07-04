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


    public class TrainManagementController : Controller
    {
        private ITrainScheduleManager _trainScheduleManager;
        private IStationManager _stationManager;
        private ITrainManager _trainManager;
        private ISeatManager _seatManager;
        private IUserManager _userManager;

        public TrainManagementController()
        {
            _trainScheduleManager = new TrainScheduleManager();
            _stationManager = new StationManager();
            _trainManager = new TrainManager();
            _seatManager = new SeatManager();
            _userManager = new UserManager();
        }

        // GET: TrainManagement
        [Authorize]
        public ActionResult Index()
        {
            User user = _userManager.GetUserByEmail(User.Identity.Name);
            if (user.Roles.Contains("Manager"))
            {
                ViewBag.Title = "Schedule Management";
                var schedule = _trainScheduleManager.GetTrainScheduleLinesByTrainScheduleID(
                    _trainScheduleManager.GetTrainSchedule().TrainScheduleID);

                return View(schedule);
            }

            return RedirectToAction("Index", "Home");


        }

        // GET: TrainManagement/TrainEdit/5
        [Authorize]
        public ActionResult TrainEdit(int id)
        {
            User user = _userManager.GetUserByEmail(User.Identity.Name);
            if (user.Roles.Contains("Manager"))
            {
                TrainVM train = _trainManager.GetTrainByID(id);
                ViewBag.Title = "Edit Train";
                return View(train);
            }

            return RedirectToAction("Index", "Home");

        }

        // POST: TrainManagement/TrainEdit/5
        [Authorize]
        [HttpPost]
        public ActionResult TrainEdit(int id, TrainVM trainVM)
        {
            User user = _userManager.GetUserByEmail(User.Identity.Name);
            if (user.Roles.Contains("Manager"))
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

            return RedirectToAction("Index", "Home");

        }

        // GET: TrainManagement/StationEdit
        [Authorize]
        public ActionResult StationEdit(int stationID)
        {
            User user = _userManager.GetUserByEmail(User.Identity.Name);
            if (user.Roles.Contains("Manager"))
            {
                Station station = _stationManager.GetStationByID(stationID);
                ViewBag.Title = "Edit Station";
                ViewBag.Types = _stationManager.GetStationTypes();
                return View(station);
            }

            return RedirectToAction("Index", "Home");

        }

        // POST: TrainManagement/StationEdit
        [Authorize]
        [HttpPost]
        public ActionResult StationEdit(int stationID, Station station)
        {
            User user = _userManager.GetUserByEmail(User.Identity.Name);
            if (user.Roles.Contains("Manager"))
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

            return RedirectToAction("Index", "Home");

        }

        // GET: TrainManagement/TrainCarsList
        [Authorize]
        public ActionResult TrainCarsList(int trainID)
        {
            User user = _userManager.GetUserByEmail(User.Identity.Name);
            if (user.Roles.Contains("Manager"))
            {
                List<TrainCarVM> cars = _trainManager.GetAllTrainCarVMsByTrainID(trainID);
                ViewBag.Title = "Train Cars";
                ViewBag.TrainID = trainID;
                ViewBag.SeatManager = _seatManager;
                return View(cars);
            }

            return RedirectToAction("Index", "Home");

        }

        // GET: TrainManagement/TrainCarDetails
        [Authorize]
        public ActionResult TrainCarDetails(int trainCarID, int trainID)
        {
            User user = _userManager.GetUserByEmail(User.Identity.Name);
            if (user.Roles.Contains("Manager"))
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

                ViewBag.Title = "Train Car Details";
                ViewBag.TrainID = trainID;
                return View(trainCar);
            }

            return RedirectToAction("Index", "Home");


        }

        // GET: TrainManagement/DeleteTrainCar
        [Authorize]
        public ActionResult DeleteTrainCar(int trainCarID, int trainID)
        {
            User user = _userManager.GetUserByEmail(User.Identity.Name);
            if (user.Roles.Contains("Manager"))
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

            return RedirectToAction("Index", "Home");

        }

        // POST: TrainManagement/DeleteTrainCar
        [Authorize]
        [HttpPost]
        public ActionResult DeleteTrainCar(int trainCarID, int trainID,
            FormCollection collection)
        {
            try
            {
                _trainManager.DeactivateTrainCar(trainCarID);
                return RedirectToAction("TrainCarsList", new { trainID = trainID });
            }
            catch
            {
                return View();
            }
        }
    }
}