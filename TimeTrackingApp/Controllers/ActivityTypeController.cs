using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeTrackingBLL.Models;

namespace TimeTrackingApp.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ActivityTypeController : ControllerBase
    {
        private readonly IActivityTypeService _activityTypeService;

        public ActivityTypeController(IActivityTypeService activityTypeService)
        {
            _activityTypeService = activityTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var activityTypes = await _activityTypeService.GetAllAsync();
            return Ok(activityTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var activityType = await _activityTypeService.GetByIdAsync(id);
            if (activityType == null)
            {
                return NotFound();
            }
            return Ok(activityType);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ActivityTypeModel model)
        {
            await _activityTypeService.AddAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = model.ActivityTypeId }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ActivityTypeModel model)
        {
            if (id != model.ActivityTypeId)
            {
                return BadRequest();
            }
            await _activityTypeService.UpdateAsync(model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _activityTypeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
