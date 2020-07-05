using System;
using System.ComponentModel.DataAnnotations;

namespace DataObject
{
    public class TrainSchedule
    {
        public int TrainScheduleID { get; set; }
        public DateTime CreationDate { get; set; }
        public int EmployeeID { get; set; }
        [Required(ErrorMessage = "Please enter a date to start.")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Please enter a time to start.")]
        public string StartTime { get; set; }
        [Required(ErrorMessage = "Please enter a time to end.")]
        public string EndTime { get; set; }
        public bool Active { get; set; }
    }
}
