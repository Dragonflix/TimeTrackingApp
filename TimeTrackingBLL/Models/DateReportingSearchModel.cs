using System.ComponentModel.DataAnnotations;

namespace TimeTrackingBLL.Models
{
    public class DateReportingSearchModel
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
