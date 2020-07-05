using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class SeatType
    {
        public int SeatTypeID { get; set; }
        public string SeatDescription { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
    }
}
