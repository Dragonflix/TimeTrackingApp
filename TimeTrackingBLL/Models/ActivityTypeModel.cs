using System.ComponentModel.DataAnnotations;

namespace TimeTrackingBLL.Models
{
    public class ActivityTypeModel
    {
        [Required]
        public Guid ActivityTypeId { get; set; }

        [Required]
        public string ActivityName { get; set; }
    }
}
