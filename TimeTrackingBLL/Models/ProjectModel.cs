using System.ComponentModel.DataAnnotations;

namespace TimeTrackingBLL.Models
{
    public class ProjectModel
    {
        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DateGreaterThan("StartDate", ErrorMessage = "End Date must be later than Start Date.")]
        public DateTime EndDate { get; set; }

        [Required]
        public string ProjectName { get; set; }
    }
}
