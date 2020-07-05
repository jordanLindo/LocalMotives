using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface ITrainManager
    {
        bool AddTrain(Train train);

        bool EditTrain(Train oldTrain, Train newTrain);

        bool AddTrainCar(TrainCar trainCar);

        bool EditTrainCar(TrainCar oldTrainCar, TrainCar newTrainCar);

        TrainVM GetTrainByID(int trainID);

        List<Train> GetAllTrainsActive();

        List<Train> GetAllTrainsDeactivated();

        List<TrainCarVM> GetAllTrainCarVMsByTrainID(int trainID);

        bool Validate(string input);

        bool DeactivateTrainCar(int trainCarID);
    }
}
