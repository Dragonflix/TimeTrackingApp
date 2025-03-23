using System.ComponentModel.DataAnnotations;

namespace TimeTrackingBLL.Models
{
    public class TimeReportModel
    {
        [Required]
        public Guid TimeReportId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DateGreaterThan("StartTime", ErrorMessage = "End Date must be later than Start Date.")]
        public DateTime EndTime { get; set; }

        public string ActivityDescription { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid ActivityTypeId { get; set; }

        [Required]
        public Guid ProjectId { get; set; }
    }
}
