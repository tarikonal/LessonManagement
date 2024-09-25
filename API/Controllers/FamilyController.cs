using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Services;
using Domain.DTOs.Family;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FamilyController : ControllerBase
    {
        private readonly IFamilyService _familyService;

        public FamilyController(IFamilyService familyService)
        {
            _familyService = familyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FamilyDto>>> GetFamilies()
        {
            var families = await _familyService.GetFamiliesAsync();
            return Ok(families);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FamilyDto>> GetFamily(Guid id)
        {
            var family = await _familyService.GetFamilyByIdAsync(id);
            if (family == null)
            {
                return NotFound();
            }
            return Ok(family);
        }

        [HttpPost]
        public async Task<ActionResult<FamilyDto>> CreateFamily(CreateFamilyDto createFamilyDto)
        {
            var family = await _familyService.CreateFamilyAsync(createFamilyDto);
            return CreatedAtAction(nameof(GetFamily), new { id = family.Id }, family);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFamily(Guid id, UpdateFamilyDto updateFamilyDto)
        {
            if (id != updateFamilyDto.Id)
            {
                return BadRequest();
            }

            var family = await _familyService.UpdateFamilyAsync(updateFamilyDto);
            if (family == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFamily(Guid id)
        {
            var result = await _familyService.DeleteFamilyAsync(id);
            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
