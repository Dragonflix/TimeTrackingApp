using TimeTrackingDAL.Entities;

namespace TimeTrackingDAL.Interfaces
{
    public interface IActivityTypeRepository
    {
        Task<IEnumerable<ActivityType>> GetAllAsync();

        Task<ActivityType> GetByIdAsync(Guid id);

        Task AddAsync(ActivityType entity);

        Task UpdateAsync(ActivityType entity);

        Task DeleteAsync(Guid id);
    }
}
