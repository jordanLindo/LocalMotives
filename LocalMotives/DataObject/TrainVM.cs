using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class TrainVM : Train
    {
        public List<TrainCarVM> TrainCars { get; set; }
    }
}
