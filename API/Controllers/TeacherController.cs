using Microsoft.AspNetCore.Mvc;
using Infrastructure.Services;
using Domain.DTOs.Teacher;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Domain.DTOs.Lesson;
using System.Security.Claims;
using Domain.DTOs.Session;
using Microsoft.AspNetCore.Identity;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly UserManager<IdentityUser> _userManager;

        public TeacherController(ITeacherService teacherService, UserManager<IdentityUser> userManager)
        {
            _teacherService = teacherService;
            _userManager = userManager;
        }

        [HttpGet("GetAllAsync")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetAllAsync()
        {
            //string userName = "tarikonal";
            //var user = await _userManager.FindByNameAsync(userName);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid? guidUser = Guid.TryParse(userId, out var guidUserId) ? guidUserId : (Guid?)null;

            var teachers = await _teacherService.GetTeachersAsync(guidUser.Value);
            return Ok(teachers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherDto>> GetByIdAsync(Guid id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return Ok(teacher);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("AddAsync")]
        public async Task<ActionResult<TeacherDto>> AddAsync(CreateTeacherDto createTeacherDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            createTeacherDto.EkleyenKullaniciId = Guid.TryParse(userId, out var guidUserId) ? guidUserId : (Guid?)null;

            var teacher = await _teacherService.CreateTeacherAsync(createTeacherDto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = teacher.Id }, teacher);
        }

        [HttpPut("UpdateAsync/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TeacherDto>> UpdateAsync(UpdateTeacherDto updateTeacherDto)
        {
            //if (id != updateTeacherDto.Id)
            //{
            //    return BadRequest();
            //}
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            updateTeacherDto.GuncelleyenKullaniciId = Guid.TryParse(userId, out var guidUserId) ? guidUserId : (Guid?)null;

            var updatedTeacher = await _teacherService.UpdateTeacherAsync(updateTeacherDto);
            if (updatedTeacher == null)
            {
                return NotFound();
            }

            return Ok(updatedTeacher);
        }

        [HttpDelete("DeleteAsync/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var result = await _teacherService.DeleteTeacherAsync(id);
            if (result == -1)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
