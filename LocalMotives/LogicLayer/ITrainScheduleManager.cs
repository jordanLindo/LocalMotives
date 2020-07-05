using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface ITrainScheduleManager
    {
        TrainSchedule GetTrainScheduleByID(int trainScheduleID);

        TrainSchedule GetTrainSchedule();

        List<TrainScheduleLineVM> GetTrainScheduleLinesByTrainScheduleID(int trainScheduleID);

        List<TrainScheduleLineVM> GetTrainScheduleLinesByRouteID(int routeLineID);

        List<TrainScheduleLineVM> GetTrainScheduleLinesByStationID(int stationID);

        int AddTrainSchedule(TrainSchedule trainSchedule);

        bool AddTrainScheduleLine(TrainScheduleLine line);

        TrainSchedule GetNewTrainSchedule(int employeeID, DateTime startDate);

        List<TrainScheduleLine> GenerateTrainSchedule(int minutes, int trainScheduleID, DateTime startDateTime);
    }
}
