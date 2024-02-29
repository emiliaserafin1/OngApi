using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ongApi.Models.Dtos;
using ongApi.Models.Enum;
using ongApi.Services.Interfaces;
using System.Security.Claims;

namespace ongApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (role == UserRole.Admin.ToString())
            {
                return Ok(_userService.GetAllUsers());
            }
            return Forbid();
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserById(int userId)
        {
            if (userId == 0)
            {
                return BadRequest();
            }

            GetUserDto? user = _userService.GetUserById(userId);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateUser(CreateAndUpdateUserDto dto)
        {
            try
            {
                _userService.CreateUser(dto);
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

        [HttpPut("{userId}")]
        public IActionResult UpdateUser(CreateAndUpdateUserDto dto, int userId)
        {
            if (!_userService.CheckIfUserExists(userId))
            {
                return NotFound();
            }
            try
            {
                _userService.UpdateUser(dto, userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return NoContent();
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            try
            {
                string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                string userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (role != "Admin")
                    return Forbid();

                if (userIdClaim == userId.ToString())
                    return BadRequest("No se puede eliminar a sí mismo");

                var user = _userService.GetUserById(userId);
                if (user is null)
                    return BadRequest("El cliente que intenta eliminar no existe");

                if (user.Role == UserRole.Admin)
                    return BadRequest("No se puede eliminar a un Administrador");

                _userService.DeleteUser(userId);
                return NoContent();
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpPost("registerForActivity/{activityId}")]
        public IActionResult RegisterForActivity( int activityId)
        {
            try
            {
                int userId = int.Parse((User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value));
                _userService.RegisterForActivity(userId, activityId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }

        [HttpGet("getUserActivities/{userId}")]
        public IActionResult GetUserActivities(int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user is null)
                return NotFound("El usuario no existe");

            return Ok(_userService.GetUserActivities(userId));
        }   

    }
}
