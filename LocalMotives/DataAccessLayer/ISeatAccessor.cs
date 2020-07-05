using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface ISeatAccessor
    {
        List<SeatVM> SelectAllSeatsByAvailable(bool available = true);

        int UpdateSeat(Seat oldSeat, Seat newSeat);

        int InsertSeat(Seat seat);

        int DeleteSeat(int seatId);

        List<SeatType> SelectAllSeatType();

        List<SeatType> SelectAllSeatTypeByActive(bool active = true);

        int UpdateSeatType(SeatType oldSeatType, SeatType newSeatType);

        int InsertSeatType(SeatType seatType);

        List<SeatVM> SelectAllSeatsActive();

        SeatVM SelectSeatVMByID(int seatID);

        List<SeatVM> SelectSeatVMsBySeatListID(int seatListID);
    }
}
