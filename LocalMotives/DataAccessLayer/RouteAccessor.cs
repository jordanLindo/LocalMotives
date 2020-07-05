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
    public class RouteAccessor : IRouteAccessor
    {
        public int InsertRoute(Route route)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_route", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TotalTime", route.TotalTimeMin);

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

        public int InsertRouteLine(RouteLine routeLine)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_routeline", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RouteID", routeLine.RouteID);
            cmd.Parameters.AddWithValue("@TimeToNext", routeLine.TimeToNext);
            cmd.Parameters.AddWithValue("@RoutePosition", routeLine.RoutePosition);

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

        public List<Route> SelectAllRoutesByActive(bool active = true)
        {
            List<Route> routes = new List<Route>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_route_by_active", conn);
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
                        routes.Add(new Route()
                        {
                            RouteID = reader.GetInt32(0),
                            TotalTimeMin = reader.GetInt32(1),
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
            return routes;
        }

        public List<RouteLineVM> SelectRouteLineByRouteID(int routeID)
        {
            List<RouteLineVM> routeLines = new List<RouteLineVM>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_routeline_by_routeid", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RouteID", routeID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        routeLines.Add(new RouteLineVM()
                        {
                            RouteLineID = reader.GetInt32(0),
                            StartStation = reader.GetInt32(1),
                            StationStartName = reader.GetString(2),
                            TimeToNext = reader.GetInt32(3),
                            RoutePosition = reader.GetInt32(4),
                            RouteID = routeID
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



            return routeLines;
        }

        public int UpdateRoute(Route oldRoute, Route newRoute)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_route", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RouteID", oldRoute.RouteID);
            cmd.Parameters.AddWithValue("@OldTotalTimeMin", oldRoute.TotalTimeMin);
            cmd.Parameters.AddWithValue("@OldActive", oldRoute.Active);
            cmd.Parameters.AddWithValue("@NewTotalTimeMin", newRoute.TotalTimeMin);
            cmd.Parameters.AddWithValue("@NewActive", newRoute.Active);

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

        public int UpdateRouteLine(RouteLine oldRouteLine, RouteLine newRouteLine)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_routeline",conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RouteLineID", oldRouteLine.RouteLineID);
            cmd.Parameters.AddWithValue("@OldRouteID", oldRouteLine.RouteID);
            cmd.Parameters.AddWithValue("@OldStartStation", oldRouteLine.StartStation);
            cmd.Parameters.AddWithValue("@OldTimeToNext", oldRouteLine.TimeToNext);
            cmd.Parameters.AddWithValue("@OldRoutePosition", oldRouteLine.RoutePosition);
            cmd.Parameters.AddWithValue("@NewRouteID", newRouteLine.RouteID);
            cmd.Parameters.AddWithValue("@NewStartStation", newRouteLine.StartStation);
            cmd.Parameters.AddWithValue("@NewTimeToNext", newRouteLine.TimeToNext);
            cmd.Parameters.AddWithValue("@NewRoutePosition", newRouteLine.RoutePosition);

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
