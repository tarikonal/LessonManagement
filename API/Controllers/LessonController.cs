using AutoMapper;
using Domain.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Domain.DTOs.Lesson;
using Microsoft.AspNetCore.Authorization;
using Domain.DTOs.Family;
using System.Security.Claims;
using Domain.DTOs.Session;
using Microsoft.AspNetCore.Identity;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public LessonController(ILessonService lessonService, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _lessonService = lessonService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet("GetAllAsync")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetAllAsync()
        {

            //string userName = "tarikonal";
            //var user = await _userManager.FindByNameAsync(userName);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid? guidUser = Guid.TryParse(userId, out var guidUserId) ? guidUserId : (Guid?)null;

            var lessons = await _lessonService.GetLessonsAsync(guidUser.Value);
            var lessonDtos = _mapper.Map<IEnumerable<LessonDto>>(lessons);
            return Ok(lessonDtos);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("AddAsync")]
        public async Task<ActionResult<LessonDto>> AddAsync(CreateLessonDto createLessonDto)
        {
            var lesson = _mapper.Map<Lesson>(createLessonDto);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            createLessonDto.EkleyenKullaniciId = Guid.TryParse(userId, out var guidUserId) ? guidUserId : (Guid?)null;

            var lessonDto = await _lessonService.CreateLessonAsync(createLessonDto);
            return CreatedAtAction(nameof(GetAllAsync), new { id = lessonDto.Id }, lessonDto);
        }

        [HttpPut("UpdateAsync")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<LessonDto>> UpdateAsync(UpdateLessonDto updateLessonDto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                updateLessonDto.GuncelleyenKullaniciId = Guid.TryParse(userId, out var guidUserId) ? guidUserId : (Guid?)null;

                // Retrieve the existing lesson from the database
                //var existingLesson = await _lessonService.GetLessonByIdAsync(updateLessonDto.Id);
                //if (existingLesson == null)
                //{
                //    return NotFound();
                //}

                //// Preserve the original values of EkleyenKullaniciId and EklemeTarihi
                //updateLessonDto.EkleyenKullaniciId = existingLesson.EkleyenKullaniciId;
                ////updateLessonDto.EklemeTarihi = existingLesson.EklemeTarihi;
                
                var lesson = _mapper.Map<Lesson>(updateLessonDto);

                var lessonDto = await _lessonService.UpdateLessonAsync(updateLessonDto);
                return CreatedAtAction(nameof(GetAllAsync), new { id = lessonDto.Id }, lessonDto);
            }
            catch (Exception ex)
            {
                // Handle the error here
                // Log the error or return a specific error response
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("GetByIdAsync/{id}")]
        public async Task<ActionResult<LessonDto>> GetByIdAsync(Guid id)
        {
            var lesson = await _lessonService.GetLessonByIdAsync(id);
            if (lesson == null) { return NotFound(); }
            return Ok(lesson);
        }

        [HttpDelete("DeleteAsync/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<int>> DeleteAsync(Guid id)
        {
            var result = await _lessonService.DeleteLessonAsync(id);
            if (result == -1)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
