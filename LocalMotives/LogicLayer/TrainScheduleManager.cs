using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObject;

namespace LogicLayer
{
    public class TrainScheduleManager : ITrainScheduleManager
    {
        ITrainScheduleAccessor _trainScheduleAccessor;
        IRouteAccessor _routeAccessor;

        public TrainScheduleManager()
        {
            _trainScheduleAccessor = new TrainScheduleAccessor();
            _routeAccessor = new RouteAccessor();
        }

        public int AddTrainSchedule(TrainSchedule trainSchedule)
        {
            int result = 0;

            try
            {
                result = _trainScheduleAccessor.InsertTrainSchedule(trainSchedule);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to add Train Schedule. ", ex);
            }

            return result;
        }

        public bool AddTrainScheduleLine(TrainScheduleLine line)
        {
            bool result = false;

            try
            {
                result = (1 == _trainScheduleAccessor.InsertTrainScheduleLine(line));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed add Train Schedule line. ", ex);
            }


            return result;
        }

        public TrainSchedule GetTrainScheduleByID(int trainScheduleID)
        {
            try
            {
                return _trainScheduleAccessor.SelectTrainScheduleByID(trainScheduleID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to find a matching Train Schedule. ",ex);
            }
        }
        public TrainSchedule GetTrainSchedule()
        {
            try
            {
                return _trainScheduleAccessor.SelectTrainSchedule();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to find a Train Schedule. ", ex);
            }
        }

        public List<TrainScheduleLineVM> GetTrainScheduleLinesByRouteID(int routeLineID)
        {
            try
            {
                return _trainScheduleAccessor.SelectTrainScheduleLinesByRouteLineID(routeLineID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to find a matching Train Schedule. ", ex);
            }
        }

        public List<TrainScheduleLineVM> GetTrainScheduleLinesByTrainScheduleID(int trainScheduleID)
        {
            try
            {
                return _trainScheduleAccessor.SelectTrainScheduleLinesByTrainScheduleID(trainScheduleID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to find a matching Train Schedule. ", ex);
            }
        }

        public TrainSchedule GetNewTrainSchedule(int employeeID, DateTime startDate)
        {
            TrainSchedule trainSchedule = new TrainSchedule()
            {
                EmployeeID = employeeID,
                StartDate = startDate,
                CreationDate = DateTime.Now
            };
            return trainSchedule;
        }

        public List<TrainScheduleLine> GenerateTrainSchedule(int minutes,int trainScheduleID,DateTime startDateTime)
        {
            List<TrainScheduleLine> lines = new List<TrainScheduleLine>();

            List<Route> routes = new List<Route>();

            List<RouteLineVM> routeLines = new List<RouteLineVM>();

            routes = _routeAccessor.SelectAllRoutesByActive();
            

            foreach (Route route in routes)
            {
                routeLines = _routeAccessor.SelectRouteLineByRouteID(route.RouteID);

                DateTime time = startDateTime;

                do
                {
                    for (int i = 0; i < routeLines.Count; i++)
                    {
                        lines.Add(new TrainScheduleLine()
                        {
                            RouteLineID = routeLines.ElementAt(i).RouteLineID,
                            ArrivalTime = time,
                            TrainScheduleID = trainScheduleID
                        });

                        time = time.AddMinutes(routeLines.ElementAt(i).TimeToNext);
                    }

                    minutes -= route.TotalTimeMin;

                } while (minutes > route.TotalTimeMin);
            }
            return lines;
        }

        public List<TrainScheduleLineVM> GetTrainScheduleLinesByStationID(int stationID)
        {
            try
            {
                return _trainScheduleAccessor.SelectTrainScheduleLinesByStationID(stationID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to retrieve times.",ex);
            }
        }
    }
}
