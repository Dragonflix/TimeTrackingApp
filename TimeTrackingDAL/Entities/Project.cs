namespace TimeTrackingDAL.Entities
{
    public class Project
    {
        public Guid ProjectId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ProjectName { get; set; }
    }
}
