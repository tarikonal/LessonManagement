using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Services;
using Domain.DTOs.Family;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FamilyController : ControllerBase
    {
        private readonly IFamilyService _familyService;
      
        private readonly UserManager<IdentityUser> _userManager;

        public FamilyController(IFamilyService familyService, UserManager<IdentityUser> userManager)
        {
            _familyService = familyService;
            _userManager = userManager;
        }

        [HttpGet("GetAllAsync")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<FamilyDto>>> GetAllAsync()
        {
            //string userName = "tarikonal";
            //var user = await _userManager.FindByNameAsync(userName);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid? guidUser = Guid.TryParse(userId, out var guidUserId) ? guidUserId : (Guid?)null;

            var families = await _familyService.GetFamiliesAsync(guidUser.Value);
            return Ok(families);
        }

        //[HttpGet("{id}")]
        [HttpGet("GetByIdAsync/{id}")]
        public async Task<ActionResult<FamilyDto>> GetByIdAsync(Guid id)
        {
            var family = await _familyService.GetFamilyByIdAsync(id);
            if (family == null)
            {
                return NotFound();
            }
            return Ok(family);
        }
       
        [Authorize(Roles = "Admin")]
        [HttpPost("AddAsync")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<FamilyDto>> AddAsync(CreateFamilyDto createFamilyDto)
        {
            //    // Access user claims
            //    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //    var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            //    //how to read 
            //    // Optionally, you can add custom logic based on user claims or roles
            //    if (userRole != "Admin")
            //    {
            //        return Forbid(); // Return 403 Forbidden if the user is not an admin
            //    }
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                createFamilyDto.EkleyenKullaniciId = Guid.TryParse(userId, out var guidUserId) ? guidUserId : (Guid?)null;

                var family = await _familyService.CreateFamilyAsync(createFamilyDto);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = family.Id }, family);
            }
            catch (Exception ex)
            {
                // Handle the error here
                // Log the error or return a specific error response
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("UpdateAsync")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(UpdateFamilyDto updateFamilyDto)
        {
            //if (id != updateFamilyDto.Id)
            //{
            //    return BadRequest();
            //}
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            updateFamilyDto.GuncelleyenKullaniciId = Guid.TryParse(userId, out var guidUserId) ? guidUserId : (Guid?)null;

            var family = await _familyService.UpdateFamilyAsync(updateFamilyDto);
            if (family == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("DeleteAsync/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(Guid id)
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
