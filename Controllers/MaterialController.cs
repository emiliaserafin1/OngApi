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
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _materialService;
        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }
        
        [HttpGet]
        public IActionResult GetAllMaterials()
        {
            return Ok(_materialService.GetAllMaterials());
        }

        [HttpGet("{materialId}")] 
        public IActionResult GetMaterialById(int materialId)
        {
            if (materialId == 0)
            {
                return BadRequest();
            }
            Material? material = _materialService.GetMaterialById(materialId);
            if (material is null)
            {
                return NotFound();
            }
            return Ok(material);
        }

        [HttpPost]
        public IActionResult CreateMaterial([FromBody] CreateAndUpdateMaterialDto material)
        {
            try
            {
                _materialService.CreateMaterial(material);
            }
            catch (Exception)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
            }

            return Created("Created", material);
        }

        [HttpPut("{materialId}")]
        public IActionResult UpdateMaterial([FromBody] CreateAndUpdateMaterialDto material, int materialId)
        {
            if (materialId == 0)
            {
                return BadRequest();
            }
            try
            {
                _materialService.UpdateMaterial(material, materialId);
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

        [HttpDelete("{materialId}")]
        public IActionResult DeleteMaterial(int materialId)
        {
            if (materialId == 0)
            {
                return BadRequest();
            }
            _materialService.DeleteMaterial(materialId);
            return NoContent();
        }

    }
}
