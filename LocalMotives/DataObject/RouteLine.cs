using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class RouteLine
    {
        public int RouteLineID { get; set; }
        public int RouteID { get; set; }
        public int StartStation { get; set; }
        public int TimeToNext { get; set; }
        public int RoutePosition { get; set; }
    }
}
