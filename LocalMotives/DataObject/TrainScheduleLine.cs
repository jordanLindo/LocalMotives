using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class TrainScheduleLine
    {
        public int TrainScheduleLineID { get; set; }
        public int RouteLineID { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int TrainScheduleID { get; set; }
    }
}
