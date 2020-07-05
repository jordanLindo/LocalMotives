using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class Station
    {
        public int StationID { get; set; }
        [Required(ErrorMessage = "Please enter a name.")]
        [MaxLength(150, ErrorMessage = "This can't be more than 150 characters.")]
        public string StationName { get; set; }
        public string StationType { get; set; }
        public bool Active { get; set; }
    }
}
