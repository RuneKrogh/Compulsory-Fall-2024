using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.DTOs.Read;
using Service.DTOs.Create;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyDto>>> GetAllProperties()
        {
            var properties = await _propertyService.GetAllProperties();
            return Ok(properties);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyDto>> GetPropertyById(int id)
        {
            var property = await _propertyService.GetPropertyById(id);
            if (property == null) return NotFound();
            return Ok(property);
        }

        [HttpPost]
        public async Task<ActionResult<PropertyDto>> CreateProperty(CreatePropertyDto createPropertyDto)
        {
            var createdProperty = await _propertyService.CreateProperty(createPropertyDto);
            return CreatedAtAction(nameof(GetPropertyById), new { id = createdProperty.Id }, createdProperty);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProperty(int id, PropertyDto propertyDto)
        {
            if (id != propertyDto.Id) return BadRequest();
            await _propertyService.UpdateProperty(propertyDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            await _propertyService.DeleteProperty(id);
            return NoContent();
        }
    }
}