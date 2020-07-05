using DataObject;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPresentationLayer.Controllers
{
    [HandleError(ExceptionType = typeof(ApplicationException),
        View = "Error")]
    public class TrainScheduleController : Controller
    {

        private ITrainScheduleManager _trainScheduleManager;
        private IStationManager _stationManager;
        private ITrainManager _trainManager;


        public TrainScheduleController()
        {
            _trainScheduleManager = new TrainScheduleManager();
            _stationManager = new StationManager();
            _trainManager = new TrainManager();
        }

        // GET: TrainSchedule
        public ActionResult Index()
        {
            ViewBag.Title = "Schedule";
            var schedule = _trainScheduleManager.GetTrainScheduleLinesByTrainScheduleID(
                _trainScheduleManager.GetTrainSchedule().TrainScheduleID);
            if(schedule != null)
            {
                return View(schedule);
            }
            else
            {
                return View();
            }

        }


        // GET: TrainSchedule/StationScheduleDetails/5
        public ActionResult StationScheduleDetails(int id)
        {
            ViewBag.Title = "Station Schedule Detail";
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
