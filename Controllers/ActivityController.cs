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
        public IActionResult CreateActivity([FromForm] CreateAndUpdateActivityDto dto, IFormFile image)
        {
            try
            {
                if (image != null && image.Length > 0)
                {
                    string imagePath = GuardarImagen(image); // Guardar la imagen
                    dto.ImgUrl = imagePath; // Asignar la URL de la imagen al DTO
                }

                _activityService.CreateActivity(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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

        // Método para guardar la imagen en el servidor
        private static string GuardarImagen(IFormFile image)
        {
            // Genera un nombre de archivo único para evitar conflictos.
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

            // Obtiene la ruta de la carpeta donde se guardarán las imágenes (wwwroot/Images).
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");

            // Si la carpeta no existe, la crea.
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Combina la ruta de la carpeta con el nombre del archivo para obtener la ruta completa del archivo.
            string filePath = Path.Combine(uploadsFolder, fileName);

            // Guarda la imagen en el sistema de archivos.
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }

            // Devuelve la ruta relativa de la imagen.
            return "/Images/" + fileName;
        }


    }

}
