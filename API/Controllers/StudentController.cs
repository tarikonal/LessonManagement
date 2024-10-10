using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.DTOs.Student;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Domain.DTOs.Lesson;
using System.Security.Claims;
using Domain.DTOs.Session;
using Microsoft.AspNetCore.Identity;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly UserManager<IdentityUser> _userManager;

        public StudentController(IStudentService studentService, UserManager<IdentityUser> userManager)
        {
            _studentService = studentService;
            _userManager = userManager;
        }

        [HttpGet("GetAllAsync")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAllAsync()
        {
            //string userName = "tarikonal";
            //var user = await _userManager.FindByNameAsync(userName);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid? guidUser = Guid.TryParse(userId, out var guidUserId) ? guidUserId : (Guid?)null;

            var students = await _studentService.GetStudentsAsync(guidUser.Value);
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetByIdAsync(Guid id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<StudentDto>> CreateStudent(CreateStudentDto createStudentDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            createStudentDto.EkleyenKullaniciId = Guid.TryParse(userId, out var guidUserId) ? guidUserId : (Guid?)null;

            var student = await _studentService.CreateStudentAsync(createStudentDto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = student.Id }, student);
        }

        [HttpPut("UpdateAsync")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<StudentDto>> UpdateAsync(UpdateStudentDto updateStudentDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            updateStudentDto.GuncelleyenKullaniciId = Guid.TryParse(userId, out var guidUserId) ? guidUserId : (Guid?)null;

            var updatedStudent = await _studentService.UpdateStudentAsync(updateStudentDto);
            if (updatedStudent == null)
            {
                return NotFound();
            }

            return Ok(updatedStudent);
        }

        [HttpDelete("DeleteAsync/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var result = await _studentService.DeleteStudentAsync(id);
            if (result == -1)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
