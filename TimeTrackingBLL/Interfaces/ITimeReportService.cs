using TimeTrackingBLL.Models;

public interface ITimeReportService
{
    Task<IEnumerable<TimeReportModel>> GetByUserIdAndDateAsync(DateReportingSearchModel model);
    Task<IEnumerable<TimeReportModel>> GetByUserIdAndWeekAsync(WeekReportingSearchModel model);
    Task AddAsync(TimeReportModel model, string token);
}
