using Microsoft.AspNetCore.Mvc;
using Infrastructure.Services;
using Domain.DTOs.Teacher;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetTeachers()
        {
            var teachers = await _teacherService.GetTeachersAsync();
            return Ok(teachers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherDto>> GetTeacherById(Guid id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return Ok(teacher);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<TeacherDto>> CreateTeacher(CreateTeacherDto createTeacherDto)
        {
            var teacher = await _teacherService.CreateTeacherAsync(createTeacherDto);
            return CreatedAtAction(nameof(GetTeacherById), new { id = teacher.Id }, teacher);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TeacherDto>> UpdateTeacher(Guid id, UpdateTeacherDto updateTeacherDto)
        {
            if (id != updateTeacherDto.Id)
            {
                return BadRequest();
            }

            var updatedTeacher = await _teacherService.UpdateTeacherAsync(updateTeacherDto);
            if (updatedTeacher == null)
            {
                return NotFound();
            }

            return Ok(updatedTeacher);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeacher(Guid id)
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
