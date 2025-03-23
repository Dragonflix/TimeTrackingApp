using AutoMapper;
using TimeTrackingDAL.Interfaces;
using TimeTrackingBLL.Models;
using TimeTrackingDAL.Entities;
using Microsoft.Extensions.Logging;

public class ActivityTypeService : IActivityTypeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<IActivityTypeService> _logger;

    public ActivityTypeService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<IActivityTypeService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<ActivityTypeModel>> GetAllAsync()
    {
        _logger.LogInformation($"Fetching all ActivityTypes");
        var activityTypes = await _unitOfWork.ActivityTypeRepository.GetAllAsync();
        return activityTypes.Select(_mapper.Map<ActivityTypeModel>);
    }

    public async Task<ActivityTypeModel> GetByIdAsync(Guid id)
    {
        _logger.LogInformation($"Fetching ActivityType {id}");
        var activityType = await _unitOfWork.ActivityTypeRepository.GetByIdAsync(id);
        return _mapper.Map<ActivityTypeModel>(activityType);
    }

    public async Task AddAsync(ActivityTypeModel model)
    {
        _logger.LogInformation($"Adding ActivityType");
        var activityType = _mapper.Map<ActivityType>(model);
        await _unitOfWork.ActivityTypeRepository.AddAsync(activityType);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateAsync(ActivityTypeModel model)
    {
        _logger.LogInformation($"Updating ActivityType {model.ActivityTypeId}");
        var activityType = _mapper.Map<ActivityType>(model);
        await _unitOfWork.ActivityTypeRepository.UpdateAsync(activityType);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        _logger.LogInformation($"Deleting ActivityType {id}");
        await _unitOfWork.ActivityTypeRepository.DeleteAsync(id);
        await _unitOfWork.SaveAsync();
    }
}