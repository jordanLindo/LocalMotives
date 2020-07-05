using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface ITrainScheduleAccessor
    {
        TrainSchedule SelectTrainSchedule();

        TrainSchedule SelectTrainScheduleByID(int trainScheduleID);

        List<TrainScheduleLineVM> SelectTrainScheduleLinesByTrainScheduleID(int trainScheduleID);

        List<TrainScheduleLineVM> SelectTrainScheduleLinesByRouteLineID(int routeLineID);

        List<TrainScheduleLineVM> SelectTrainScheduleLinesByStationID(int stationID);

        int InsertTrainSchedule(TrainSchedule trainSchedule);

        int InsertTrainScheduleLine(TrainScheduleLine line);
    }
}
