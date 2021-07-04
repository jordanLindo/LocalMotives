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
    public class TrainScheduleAccessor : ITrainScheduleAccessor
    {
        public int InsertTrainSchedule(TrainSchedule trainSchedule)
        {
            int trainScheduleID = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_trainschedule", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CreationDate", trainSchedule.CreationDate);
            cmd.Parameters.AddWithValue("@EmployeeID", trainSchedule.EmployeeID);
            cmd.Parameters.AddWithValue("@StartDate", trainSchedule.StartDate);
            cmd.Parameters.Add("@NewScheduleID", SqlDbType.Int).Direction = ParameterDirection.Output;


            try
            {
                conn.Open();
                cmd.ExecuteScalar();
                trainScheduleID =(int)cmd.Parameters["@NewScheduleID"].Value;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return trainScheduleID;
        }

        public int InsertTrainScheduleLine(TrainScheduleLine line)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_trainscheduleline",conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RouteLineID", line.RouteLineID);
            cmd.Parameters.AddWithValue("@ArrivalTime", line.ArrivalTime);
            cmd.Parameters.AddWithValue("@TrainScheduleID", line.TrainScheduleID);

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

        public TrainSchedule SelectTrainScheduleByID(int trainScheduleID)
        {
            TrainSchedule trainSchedule = null;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_trainschedule_by_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TrainScheduleID", trainScheduleID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    trainSchedule = new TrainSchedule()
                    {
                        CreationDate = reader.GetDateTime(0),
                        EmployeeID = reader.GetInt32(1),
                        StartDate = reader.GetDateTime(2),
                        Active = reader.GetBoolean(3),
                        TrainScheduleID = trainScheduleID
                    };
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

            return trainSchedule;
        }

        public TrainSchedule SelectTrainSchedule()
        {
            TrainSchedule trainSchedule = null;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_trainschedule", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    trainSchedule = new TrainSchedule()
                    {
                        TrainScheduleID = reader.GetInt32(0),
                        CreationDate = reader.GetDateTime(1),
                        EmployeeID = reader.GetInt32(2),
                        StartDate = reader.GetDateTime(3),
                        Active = true
                        
                    };
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

            return trainSchedule;
        }

        public List<TrainScheduleLineVM> SelectTrainScheduleLinesByRouteLineID(int routeLineID)
        {
            List<TrainScheduleLineVM> lines = new List<TrainScheduleLineVM>();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_trainscheduleline_by_routeLineID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RouteLineID", routeLineID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lines.Add(new TrainScheduleLineVM()
                        {
                            TrainScheduleLineID = reader.GetInt32(0),
                            ArrivalTime = reader.GetDateTime(1).ToShortTimeString(),
                            TrainScheduleID = reader.GetInt32(2),
                            RouteID = reader.GetInt32(3),
                            StationStartID = reader.GetInt32(4),
                            StationName = reader.GetString(5),
                            TrainName = reader.GetString(6),
                            TrainID = reader.GetInt32(7),
                            RouteLineID = routeLineID
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
            return lines;
        }

        public List<TrainScheduleLineVM> SelectTrainScheduleLinesByStationID(int stationID)
        {
            List<TrainScheduleLineVM> lines = new List<TrainScheduleLineVM>();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_scheduleline_by_stationid", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StationID", stationID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lines.Add(new TrainScheduleLineVM()
                        {
                            TrainScheduleLineID = reader.GetInt32(0),
                            ArrivalTime = reader.GetDateTime(1).ToShortTimeString(),
                            RouteLineID = reader.GetInt32(2),
                            RouteID = reader.GetInt32(3),
                            TrainScheduleID = reader.GetInt32(4),
                            StationName = reader.GetString(5),
                            TrainName = reader.GetString(6),
                            TrainID = reader.GetInt32(7),
                            StationStartID = stationID
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

            return lines;
        }

        public List<TrainScheduleLineVM> SelectTrainScheduleLinesByTrainScheduleID(int trainScheduleID)
        {
            List<TrainScheduleLineVM> lines = new List<TrainScheduleLineVM>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_trainscheduleline_by_trainscheduleid", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TrainScheduleID", trainScheduleID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lines.Add(new TrainScheduleLineVM()
                        {
                            TrainScheduleLineID = reader.GetInt32(0),
                            ArrivalTime = reader.GetDateTime(1).ToShortTimeString(),
                            RouteLineID = reader.GetInt32(2),
                            RouteID = reader.GetInt32(3),
                            StationStartID = reader.GetInt32(4),
                            StationName = reader.GetString(5),
                            TrainName = reader.GetString(6),
                            TrainID = reader.GetInt32(7),
                            TrainScheduleID = trainScheduleID
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
            return lines;
        }
    }
}
