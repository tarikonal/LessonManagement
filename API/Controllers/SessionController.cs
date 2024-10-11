using Microsoft.AspNetCore.Mvc;
using Infrastructure.Services;
using Domain.DTOs.Session;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.DTOs.Lesson;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;
        private readonly ILessonService _lessonService;
        private readonly UserManager<IdentityUser> _userManager;

        public SessionController(ISessionService sessionService, 
                                 UserManager<IdentityUser> userManager,
                                  ITeacherService teacherService,
                                  IStudentService studentService,
                                  ILessonService lessonService
                                )
        {
            _sessionService = sessionService;
            _userManager = userManager;
            _teacherService = teacherService;
            _studentService = studentService;
            _lessonService = lessonService;
        }

        [HttpGet("GetAllAsync")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<SessionDto>>> GetAllAsync()
        {
            //string userName = "tarikonal";
            //var user = await _userManager.FindByNameAsync(userName);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid? guidUser = Guid.TryParse(userId, out var guidUserId) ? guidUserId : (Guid?)null;

            var sessions = await _sessionService.GetSessionsAsync(guidUser.Value);

            
            foreach (var session in sessions)
            {
                var student = await _studentService.GetStudentByIdAsync(session.StudentId);
                session.studentName = student.Name;
            }
            foreach (var session in sessions)
            {
                var teacher = await _teacherService.GetTeacherByIdAsync(session.TeacherId);
                session.teacherName = teacher.Name;
            }
            foreach (var session in sessions)
            {
                var lesson = await _lessonService.GetLessonByIdAsync(session.LessonId);
                session.lessonName = lesson.Name;
            }


            return Ok(sessions);
        }

        [HttpGet("GetByIdAsync/{id}")]
        public async Task<ActionResult<SessionDto>> GetByIdAsync(Guid id)
        {
            var session = await _sessionService.GetSessionByIdAsync(id);
            if (session == null)
            {
                return NotFound();
            }
            return Ok(session);
        }

        [HttpPost("AddAsync")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SessionDto>> AddAsync(CreateSessionDto createSessionDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            createSessionDto.EkleyenKullaniciId = Guid.TryParse(userId, out var guidUserId) ? guidUserId : (Guid?)null;

            var session = await _sessionService.CreateSessionAsync(createSessionDto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = session.Id }, session);
        }
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SessionDto>> UpdateAsync(UpdateSessionDto updateSessionDto)
        {
            //if (id != updateSessionDto.Id)
            //{
            //    return BadRequest();
            //}
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            updateSessionDto.GuncelleyenKullaniciId = Guid.TryParse(userId, out var guidUserId) ? guidUserId : (Guid?)null;

            var updatedSession = await _sessionService.UpdateSessionAsync(updateSessionDto);
            if (updatedSession == null)
            {
                return NotFound();
            }

            return Ok(updatedSession);
        }

        [HttpDelete("DeleteAsync/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var result = await _sessionService.DeleteSessionAsync(id);
            if (result == -1)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
