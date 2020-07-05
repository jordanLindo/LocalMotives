using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface ITrainAccessor
    {
        int InsertTrain(Train train);

        int UpdateTrain(Train oldTrain, Train newTrain);

        List<Train> SelectAllTrainsByActive(bool active = true);

        TrainVM SelectTrainsByID(int trainID);

        List<TrainCarVM> SelectAllTrainCarsByTrainID(int trainID);

        int InsertTrainCar(TrainCar trainCar);

        int UpdateTrainCar(TrainCar oldTrainCar, TrainCar newTrainCar);

        int DeactivateTrainCar(int trainCarID);
    }
}
