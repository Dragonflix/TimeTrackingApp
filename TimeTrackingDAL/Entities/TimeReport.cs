namespace TimeTrackingDAL.Entities
{
    public class TimeReport
    {
        public Guid TimeReportId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string ActivityDescription { get; set; }

        public Guid UserId { get; set; }

        public Guid ProjectId { get; set; }

        public Project Project { get; set; }

        public Guid ActivityTypeId { get; set; }

        public ActivityType ActivityType { get; set; }
    }
}
