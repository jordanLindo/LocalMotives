using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface ISeatManager
    {
        List<SeatVM> GetAllSeatsByAvailable(bool available = true);

        List<SeatType> GetAllSeatTypesByActive(bool active = true);

        bool EditSeat(Seat oldSeat, Seat newSeat);

        bool DeacivateSeat( Seat seat);

        bool AddSeat(Seat seat);

        bool DeleteSeat(int seatID);

        List<SeatType> GetAllSeatTypes();

        bool EditSeatType(SeatType oldSeatType, SeatType newSeatType);

        bool AddSeatType(SeatType seatType);

        List<SeatVM> GetAllSeats();

        List<SeatVM> GetAllSeatVMsBySeatListID(int seatListID);

        SeatVM GetSeatVMByID(int seatID);

        bool Validate(string input);
    }
}
