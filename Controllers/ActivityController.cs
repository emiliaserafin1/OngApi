using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ongApi.Entities;
using ongApi.Models.Dtos;
using ongApi.Services.Implementations;
using ongApi.Services.Interfaces;

namespace ongApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;
        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }
        [HttpGet]
        public IActionResult GetAllActivities()
        {
            return Ok(_activityService.GetAllActivities());
        }
        [HttpGet("{activityId}")]   
        public IActionResult GetActivityById(int activityId)
        {
            if (activityId == 0)
            {
                return BadRequest();
            }
            Activity? activity = _activityService.GetActivityById(activityId);
            if (activity is null)
            {
                return NotFound();
            }
            return Ok(activity);
        }
        [HttpPost]
        public IActionResult CreateActivity(CreateAndUpdateActivityDto dto)
        {
            try
            {
                _activityService.CreateActivity(dto);
            }
            catch (Exception)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
            }

            return Created("Created", dto);
        }
        [HttpPut("{activityId}")]
        public IActionResult UpdateActivity(CreateAndUpdateActivityDto dto, int activityId)
        {
            try
            {
                _activityService.UpdateActivity(dto, activityId);
            }
            catch (Exception)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
            }
            return Ok();
        }
        [HttpDelete("{activityId}")]
        public IActionResult DeleteActivity(int activityId)
        {
            try
            {
                _activityService.DeleteActivity(activityId);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return NoContent();
        }
        [HttpGet("{activityId}/volunteers")]
        public IActionResult GetActivityVolunteers(int activityId)
        {
            if (activityId == 0)
            {
                return BadRequest();
            }
            var volunteers = _activityService.GetActivityVolunteers(activityId);
            if (volunteers is null)
            {
                return NotFound();
            }
            return Ok(volunteers);
        }
        [HttpGet("{activityId}/materials")]
        public IActionResult GetActivityMaterials(int activityId)
        {
            if (activityId == 0)
            {
                return BadRequest();
            }
            var materials = _activityService.GetActivityMaterials(activityId);
            if (materials is null)
            {
                return NotFound();
            }
            return Ok(materials);
        }

    }

}
