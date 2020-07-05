using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObject;
using System.Data;

namespace DataAccessLayer
{
    public class StationAccessor : IStationAccessor
    {
        public Station SelectStationByName(string name)
        {
            Station station = null;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_station_by_name", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@StationName", name);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    station = new Station()
                    {
                        StationID = reader.GetInt32(0),
                        StationName = reader.GetString(1),
                        StationType = reader.IsDBNull(2) ? "" : reader.GetString(1)
                    };
                }
                else
                {
                    throw new ApplicationException("Station not found.");
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

            return station;
        }

        public int InsertStation(Station station)
        {
            int stationID = 0;

            var conn = DBConnection.GetConnection();

            var cmd = new SqlCommand("sp_insert_station",conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@StationName", station.StationName);
            cmd.Parameters.AddWithValue("@StationType", station.StationType);

            try
            {
                conn.Open();
                stationID = Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return stationID;
        }

        public List<Station> SelectAllStationsByActive(bool active)
        {
            List<Station> stations = new List<Station>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_station_by_active", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Active", active);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        stations.Add(new Station()
                        {
                            StationID = reader.GetInt32(0),
                            StationName = reader.GetString(1),
                            StationType = reader.GetString(2),
                            Active = active
                        });
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return stations;
        }

        public int SetStationActive(int stationID, bool active)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_active_station", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@StationID", stationID);
            cmd.Parameters.AddWithValue("@Active", active);

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

        public int UpdateStation(Station oldStation, Station updateStation)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_station", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StationID", oldStation.StationID);
            cmd.Parameters.AddWithValue("@OldStationName", oldStation.StationName);
            cmd.Parameters.AddWithValue("@OldStationType", oldStation.StationType);
            cmd.Parameters.AddWithValue("@NewStationName", updateStation.StationName);
            cmd.Parameters.AddWithValue("@NewStationType", updateStation.StationType);

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

        public Station SelectStationByID(int stationID)
        {
            Station station = null;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_station_by_id",conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StationID", stationID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    station = new Station()
                    {
                        StationID = reader.GetInt32(0),
                        StationName = reader.GetString(1),
                        StationType = reader.GetString(2)
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


            return station;
        }

        public List<string> SelectDistinctStationType()
        {
            List<string> stationTypes = new List<string>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_distinct_stationtypes", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        stationTypes.Add(reader.GetString(0));
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

            return stationTypes;
        }
    }
}
