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
    public class SeatAccessor : ISeatAccessor
    {
        public int DeleteSeat(int seatId)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_delete_seat_by_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SeatID", seatId);

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

        public int InsertSeat(Seat seat)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_seat", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SeatListID", seat.SeatListID);
            cmd.Parameters.AddWithValue("@SeatTypeID", seat.SeatTypeID);

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

        public int InsertSeatType(SeatType seatType)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_seat_type");


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Description", seatType.SeatDescription);
            cmd.Parameters.AddWithValue("@Price", seatType.Price);

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

        public List<SeatVM> SelectAllSeatsActive()
        {
            List<SeatVM> seatVMs = new List<SeatVM>();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_seat", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Decimal deciPrice = reader.GetDecimal(4);
                        seatVMs.Add(new SeatVM()
                        {
                            SeatID = reader.GetInt32(0),
                            SeatTypeID = reader.GetInt32(1),
                            SeatListID = reader.GetInt32(2),
                            SeatDescription = reader.GetString(3),
                            Price = deciPrice.ToString("C"),
                            Available = reader.GetBoolean(5),
                            Active = reader.GetBoolean(6),
                            TrainName = reader.GetString(7),
                            TrainID = reader.GetInt32(8)

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
            return seatVMs;
        }

        public List<SeatVM> SelectAllSeatsByAvailable(bool available = true)
        {
            List<SeatVM> seatVMs = new List<SeatVM>();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_seat_by_available", conn);
            var cmd2 = new SqlCommand("sp_select_seattype_detail_by_seattypeid", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Available", available);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add("@SeatTypeID");

            try
            {
                conn.Open();

                var reader1 = cmd.ExecuteReader();

                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        var seat = new Seat()
                        {
                            SeatID = reader1.GetInt32(0),
                            SeatListID = reader1.GetInt32(1),
                            SeatTypeID = reader1.GetInt32(2),
                            Available = reader1.GetBoolean(3),
                            Active = reader1.GetBoolean(4)
                        };

                        cmd2.Parameters["@SeatTypeID"].Value = seat.SeatTypeID;
                        var reader2 = cmd2.ExecuteReader();

                        if (reader2.Read())
                        {
                            Decimal deciPrice = reader2.GetDecimal(1);
                            seatVMs.Add(new SeatVM()
                            {
                                SeatID = seat.SeatID,
                                SeatListID = seat.SeatListID,
                                SeatTypeID = seat.SeatTypeID,
                                Available = seat.Available,
                                Active = seat.Active,
                                SeatDescription = reader2.GetString(0),
                                Price = deciPrice.ToString("C")
                            });

                        }

                    }
                }

            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return seatVMs;

        }

        public List<SeatType> SelectAllSeatType()
        {
            List<SeatType> seatTypes = new List<SeatType>();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_seat_type", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        seatTypes.Add(new SeatType()
                        {
                            SeatTypeID = reader.GetInt32(0),
                            SeatDescription = reader.GetString(1),
                            Price = reader.GetDecimal(2)
                        });
                    }
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return seatTypes;
        }

        public List<SeatType> SelectAllSeatTypeByActive(bool active = true)
        {
            List<SeatType> seatTypes = new List<SeatType>();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_seat_type_by_active", conn);
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
                        seatTypes.Add(new SeatType()
                        {
                            SeatTypeID = reader.GetInt32(0),
                            SeatDescription = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            Active = active
                        });
                    }
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return seatTypes;
        }

        public List<SeatVM> SelectSeatVMsBySeatListID(int seatListID)
        {
            List<SeatVM> seats = new List<SeatVM>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_seat_by_seatlistid", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SeatListID", seatListID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        seats.Add(new SeatVM()
                        {
                            SeatID = reader.GetInt32(0),
                            SeatTypeID = reader.GetInt32(1),
                            SeatListID = reader.GetInt32(2),
                            SeatDescription = reader.GetString(3),
                            Price = reader.GetDecimal(4).ToString("C"),
                            Available = reader.GetBoolean(5),
                            Active = reader.GetBoolean(6),
                            TrainName = reader.GetString(7),
                            TrainID = reader.GetInt32(8)
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


            return seats;
        }

        public SeatVM SelectSeatVMByID(int seatID)
        {
            SeatVM seat = null;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_seat_view_by_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SeatID", seatID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    seat = new SeatVM()
                    {
                        SeatID = reader.GetInt32(0),
                        SeatTypeID = reader.GetInt32(1),
                        SeatListID = reader.GetInt32(2),
                        SeatDescription = reader.GetString(3),
                        Price = reader.GetDecimal(4).ToString("C"),
                        Available = reader.GetBoolean(5),
                        Active = reader.GetBoolean(6),
                        TrainName = reader.GetString(7),
                        TrainID = reader.GetInt32(8)
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


            return seat;
        }

        public int UpdateSeat(Seat oldSeat, Seat newSeat)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_seat", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SeatID", oldSeat.SeatID);
            cmd.Parameters.AddWithValue("@OldSeatListID", oldSeat.SeatListID);
            cmd.Parameters.AddWithValue("@OldSeatTypeID", oldSeat.SeatTypeID);
            cmd.Parameters.AddWithValue("@OldAvailable", oldSeat.Available);
            cmd.Parameters.AddWithValue("@OldActive", oldSeat.Active);

            cmd.Parameters.AddWithValue("@NewSeatListID", newSeat.SeatListID);
            cmd.Parameters.AddWithValue("@NewSeatTypeID", newSeat.SeatTypeID);
            cmd.Parameters.AddWithValue("@NewAvailable", newSeat.Available);
            cmd.Parameters.AddWithValue("@NewActive", newSeat.Active);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        public int UpdateSeatType(SeatType oldSeatType, SeatType newSeatType)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_seat_type", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SeatTypeID", oldSeatType.SeatTypeID);
            cmd.Parameters.AddWithValue("@OldDescription", oldSeatType.SeatDescription);
            cmd.Parameters.AddWithValue("@OldPrice", oldSeatType.Price);
            cmd.Parameters.AddWithValue("@NewDescription", newSeatType.SeatDescription);
            cmd.Parameters.Add("@NewPrice", SqlDbType.Money).Value = newSeatType.Price;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
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
