using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeTrackingBLL.Models;

namespace TimeTrackingApp.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TimeReportController : ControllerBase
    {
        private readonly ITimeReportService _timeReportService;

        public TimeReportController(ITimeReportService timeReportService)
        {
            _timeReportService = timeReportService;
        }

        [HttpPost("ReportingByDate")]
        public async Task<IActionResult> GetByUserIdAndDate(DateReportingSearchModel model)
        {
            var timeReports = await _timeReportService.GetByUserIdAndDateAsync(model);
            return Ok(timeReports);
        }

        [HttpPost("ReportingByWeek")]
        public async Task<IActionResult> GetByUserIdAndWeek(WeekReportingSearchModel model)
        {
            var timeReports = await _timeReportService.GetByUserIdAndWeekAsync(model);
            return Ok(timeReports);
        }

        [Authorize]
        [HttpPost("Report")]
        public async Task<IActionResult> Report(TimeReportModel model)
        {
            var token = HttpContext.Request.Headers["Authorization"];
            await _timeReportService.AddAsync(model, token);
            return CreatedAtAction(nameof(Report), new { id = model.ProjectId }, model);
        }
    }
}

