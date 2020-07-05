using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class Seat
    {
        public int SeatID { get; set; }
        public int SeatListID { get; set; }
        public int SeatTypeID { get; set; }
        public bool Active { get; set; }
        public bool Available { get; set; }
    }
}
