﻿using AutoMapper;
using Domain.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Domain.DTOs.Lesson;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;
        private readonly IMapper _mapper;

        public LessonController(ILessonService lessonService, IMapper mapper)
        {
            _lessonService = lessonService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetLessons()
        {
            var lessons = await _lessonService.GetLessonsAsync();
            var lessonDtos = _mapper.Map<IEnumerable<LessonDto>>(lessons);
            return Ok(lessonDtos);
        }

        [HttpPost]
        public async Task<ActionResult<LessonDto>> CreateLesson(CreateLessonDto createLessonDto)
        {
            var lesson = _mapper.Map<Lesson>(createLessonDto);

            var lessonDto = await _lessonService.CreateLessonAsync(createLessonDto);
            return CreatedAtAction(nameof(GetLessons), new { id = lessonDto.Id }, lessonDto);
        }

        [HttpPut]

        public async Task<ActionResult<LessonDto>> UpdateLesson(UpdateLessonDto updateLessonDto)
        {
            var lesson = _mapper.Map<Lesson>(updateLessonDto);

            var lessonDto = await _lessonService.UpdateLessonAsync(updateLessonDto);
            return CreatedAtAction(nameof(GetLessons), new { id = lessonDto.Id }, lessonDto);
        }


        [HttpGet("{id}")]
     
        public async Task<ActionResult<LessonDto>> GetLessonById(Guid id)
        {
            var lesson = await _lessonService.GetLessonByIdAsync(id);
            if (lesson == null) { return NotFound(); }
            return Ok(lesson);
        }
        [HttpDelete]
        public async Task<ActionResult<int>> DeleteLesson(Guid id)
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
