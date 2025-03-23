using TimeTrackingDAL.Entities;

namespace TimeTrackingDAL.Interfaces
{
    public interface ITimeReportRepository
    {
        Task<IEnumerable<TimeReport>> GetByUserIdAsync(Guid id);
        Task AddAsync(TimeReport entity);
    }
}
