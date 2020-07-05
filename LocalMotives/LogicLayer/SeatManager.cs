using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObject;

namespace LogicLayer
{
    public class SeatManager : ISeatManager
    {
        private ISeatAccessor _seatAccessor;

        public SeatManager()
        {
            _seatAccessor = new SeatAccessor();
        }

        public bool AddSeat(Seat seat)
        {
            bool result = false;

            try
            {
                result = (1 == _seatAccessor.InsertSeat(seat));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Seat not added.", ex);
            }
            return result;
        }

        public bool AddSeatType(SeatType seatType)
        {
            bool result = false;

            try
            {
                result = (1 == _seatAccessor.InsertSeatType(seatType));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Seat type not added.", ex);
            }
            return result;
        }

        public bool DeacivateSeat(Seat seat)
        {
            Seat newSeat = new Seat()
            {
                SeatID = seat.SeatID,
                SeatListID = seat.SeatListID,
                SeatTypeID = seat.SeatTypeID,
                Available = seat.Available,
                Active = false
            };
            bool result = false;

            try
            {
                result = (1 == _seatAccessor.UpdateSeat(seat, newSeat));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Seat not deacitivated.",ex);
            }

            return result;
        }

        public bool DeleteSeat(int seatID)
        {
            bool result = false;

            try
            {
                result = (1 == _seatAccessor.DeleteSeat(seatID));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Seat not removed.",ex);
            }
            return result;
        }

        public bool EditSeat(Seat oldSeat, Seat newSeat)
        {
            bool result = false;

            try
            {
                result = (1 == _seatAccessor.UpdateSeat(oldSeat, newSeat));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update failed.", ex);
            }


            return result;
        }

        public bool EditSeatType(SeatType oldSeatType, SeatType newSeatType)
        {
            bool result = false;

            try
            {
                result = (1 == _seatAccessor.UpdateSeatType(oldSeatType, newSeatType));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update failed.", ex);
            }


            return result;
        }

        public List<SeatVM> GetAllSeats()
        {
            try
            {
                return _seatAccessor.SelectAllSeatsActive();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to retrieve list.",ex);
            }
        }

        public List<SeatVM> GetAllSeatsByAvailable(bool available = true)
        {
            try
            {
                return _seatAccessor.SelectAllSeatsByAvailable(available);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to retrieve list.", ex);
            }
        }

        public List<SeatVM> GetAllSeatVMsBySeatListID(int seatListID)
        {
            try
            {
                return _seatAccessor.SelectSeatVMsBySeatListID(seatListID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to retrieve list.", ex);
            }
        }

        public List<SeatType> GetAllSeatTypes()
        {
            try
            {
                return _seatAccessor.SelectAllSeatType();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to retrieve list.",ex);
            }
        }

        public SeatVM GetSeatVMByID(int seatID)
        {
            try
            {
                return _seatAccessor.SelectSeatVMByID(seatID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Validate(string input)
        {
            bool valid = false;

            if(!input.Contains(";") && !input.Contains("'")
                && !input.Contains("--") && !input.Contains("/*")
                    && !input.Contains("*/") && !input.Contains("xp_")
                    &&(input.IndexOf("drop",StringComparison.OrdinalIgnoreCase)==-1))
            {
                valid = true;
            }

            return valid;
        }

        public List<SeatType> GetAllSeatTypesByActive(bool active = true)
        {
            try
            {
                return _seatAccessor.SelectAllSeatTypeByActive(active);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to retrieve List.", ex);
            }
        }
    }
}
