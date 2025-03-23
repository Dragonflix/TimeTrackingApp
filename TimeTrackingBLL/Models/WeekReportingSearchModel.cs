using System.ComponentModel.DataAnnotations;

namespace TimeTrackingBLL.Models
{
    public class WeekReportingSearchModel
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [Range(1, 52, ErrorMessage = "Week must be between 1 and 52.")]
        public int Week {  get; set; }
    }
}
