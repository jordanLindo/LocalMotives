using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObject;

namespace LogicLayer
{
    public class TrainManager : ITrainManager
    {
        ITrainAccessor _trainAccessor;

        public TrainManager()
        {
            _trainAccessor = new TrainAccessor();
        }

        public bool AddTrain(Train train)
        {
            bool added = false;
            try
            {
                added = 1 == _trainAccessor.InsertTrain(train);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Train not added.",ex);
            }
            return added;
        }

        public bool AddTrainCar(TrainCar trainCar)
        {
            bool added = false;
            try
            {
                added = 1 == _trainAccessor.InsertTrainCar(trainCar);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Train Car not added.",ex);
            }

            return added;
        }

        public bool DeactivateTrainCar(int trainCarID)
        {
            try
            {
                return 1 == _trainAccessor.DeactivateTrainCar(trainCarID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool EditTrain(Train oldTrain, Train newTrain)
        {
            bool added = false;
            try
            {
                added = 1 == _trainAccessor.UpdateTrain(oldTrain, newTrain);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Train not updated.",ex);
            }

            return added;
        }

        public bool EditTrainCar(TrainCar oldTrainCar, TrainCar newTrainCar)
        {
            bool added = false;
            try
            {
                added = 1 == _trainAccessor.UpdateTrainCar(oldTrainCar, newTrainCar);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Train Car not updated.",ex);
            }


            return added;
        }

        public List<TrainCarVM> GetAllTrainCarVMsByTrainID(int trainID)
        {
            try
            {
                return _trainAccessor.SelectAllTrainCarsByTrainID(trainID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Error retrieving list.",ex);
            }
        }

        public List<Train> GetAllTrainsActive()
        {
            try
            {
                return _trainAccessor.SelectAllTrainsByActive();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Error retrieving list.", ex);
            }
        }

        public List<Train> GetAllTrainsDeactivated()
        {
            try
            {
                return _trainAccessor.SelectAllTrainsByActive(false);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Error retrieving list.", ex);
            }
        }

        public TrainVM GetTrainByID(int trainID)
        {
            try
            {
                return _trainAccessor.SelectTrainsByID(trainID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Validate(string input)
        {
            bool valid = false;

            if (!input.Contains(";") && !input.Contains("'")
                && !input.Contains("--") && !input.Contains("/*")
                    && !input.Contains("*/") && !input.Contains("xp_")
                    && (input.IndexOf("drop", StringComparison.OrdinalIgnoreCase) == -1))
            {
                valid = true;
            }

            return valid;
        }
    }
}
