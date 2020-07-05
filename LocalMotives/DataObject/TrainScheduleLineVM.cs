using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class TrainScheduleLineVM : TrainScheduleLine
    {
        public int RouteID { get; set; }
        public int StationStartID { get; set; }
        public string StationName { get; set; }
        public string TrainName { get; set; }
        public int TrainID { get; set; }
    }
}
