using TimeTrackingBLL.Models;

public interface IActivityTypeService
{
    Task<IEnumerable<ActivityTypeModel>> GetAllAsync();
    Task<ActivityTypeModel> GetByIdAsync(Guid id);
    Task AddAsync(ActivityTypeModel model);
    Task UpdateAsync(ActivityTypeModel model);
    Task DeleteAsync(Guid id);
}