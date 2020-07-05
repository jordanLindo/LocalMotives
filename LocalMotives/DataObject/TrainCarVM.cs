using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class TrainCarVM : TrainCar
    {
        public string TrainCarDescription { get; set; }
        public int SeatListID { get; set; }
        public List<SeatVM> Seats { get; set; }
    }
}
