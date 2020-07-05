using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class SeatVM : Seat
    {
        public string SeatDescription { get; set; }
        public string Price { get; set; }
        public string TrainName { get; set; }
        public int TrainID { get; set; }
    }

}