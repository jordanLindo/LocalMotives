using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObject;

namespace DataAccessLayer
{
    public class TrainAccessor : ITrainAccessor
    {
        public int DeactivateTrainCar(int trainCarID)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_deactivate_traincar", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@trainCarID", trainCarID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        public int InsertTrain(Train train)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_train", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TrainName", train.TrainName);
            cmd.Parameters.AddWithValue("@RouteID", train.RouteID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        public int InsertTrainCar(TrainCar trainCar)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_traincar", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CarListID", trainCar.CarListID);
            cmd.Parameters.AddWithValue("@CoachTypeID", trainCar.CoachTypeID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }


            return rows;
        }

        public List<TrainCarVM> SelectAllTrainCarsByTrainID(int trainID)
        {
            List<TrainCarVM> trainCars = new List<TrainCarVM>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_traincar_by_trainid",conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TrainID", trainID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        trainCars.Add(new TrainCarVM()
                        {
                            TrainCarID = reader.GetInt32(0),
                            CoachTypeID = reader.GetInt32(1),
                            TrainCarDescription = reader.GetString(2),
                            CarListID = reader.GetInt32(3),
                            SeatListID = reader.GetInt32(4)
                        });
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }


            return trainCars;
        }

        public List<Train> SelectAllTrainsByActive(bool active = true)
        {
            List<Train> trains = new List<Train>();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_train_by_active",conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Active", active);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        trains.Add(new Train()
                        {
                            TrainID = reader.GetInt32(0),
                            TrainName = reader.GetString(1),
                            RouteID = reader.GetInt32(2),
                            Active = active
                        });
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return trains;
        }

        public TrainVM SelectTrainsByID(int trainID)
        {
            TrainVM train = null;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_train_by_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TrainID", trainID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    train = new TrainVM()
                    {
                        TrainID = trainID,
                        TrainName = reader.GetString(0),
                        RouteID = reader.GetInt32(1),
                        Active = reader.GetBoolean(2)
                    };
                    train.TrainCars = SelectAllTrainCarsByTrainID(trainID);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }



            return train;
        }

        public int UpdateTrain(Train oldTrain, Train newTrain)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_train",conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TrainID", oldTrain.TrainID);
            cmd.Parameters.AddWithValue("@OldRouteID", oldTrain.RouteID);
            cmd.Parameters.AddWithValue("@OldTrainName", oldTrain.TrainName);
            cmd.Parameters.AddWithValue("@OldActive", oldTrain.Active);
            cmd.Parameters.AddWithValue("@NewRouteID", newTrain.RouteID);
            cmd.Parameters.AddWithValue("@NewTrainName", newTrain.TrainName);
            cmd.Parameters.AddWithValue("@NewActive", newTrain.Active);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }


            return rows;
        }

        public int UpdateTrainCar(TrainCar oldTrainCar, TrainCar newTrainCar)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_traincar");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TrainCarID", oldTrainCar.TrainCarID);
            cmd.Parameters.AddWithValue("@OldCoachTypeID", oldTrainCar.CoachTypeID);
            cmd.Parameters.AddWithValue("@OldCarListID", oldTrainCar.CarListID);
            cmd.Parameters.AddWithValue("@NewCoachTypeID", newTrainCar.CoachTypeID);
            cmd.Parameters.AddWithValue("@NewCarListID", newTrainCar.CarListID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }
    }
}
