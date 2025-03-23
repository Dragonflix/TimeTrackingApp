namespace TimeTrackingDAL.Interfaces
{
    public interface IUnitOfWork
    {
        ITimeReportRepository TimeReportRepository { get; }
        IActivityTypeRepository ActivityTypeRepository { get; }
        IProjectRepository ProjectRepository { get; }
        Task SaveAsync();
    }
}
