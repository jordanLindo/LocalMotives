using System.ComponentModel.DataAnnotations;

namespace DataObject
{
    public class Train
    {
        public int TrainID { get; set; }
        [Required(ErrorMessage = "Please enter a Name.")]
        [MaxLength(100,ErrorMessage = "This can't be more than 100 characters.")]
        public string TrainName { get; set; }
        public int RouteID { get; set; }
        public bool Active { get; set; }
    }
}
