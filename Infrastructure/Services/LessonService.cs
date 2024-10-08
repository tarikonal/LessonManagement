﻿//using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.DTOs.Lesson;

namespace Infrastructure.Services
{
    public class LessonService : ILessonService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public LessonService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LessonDto>> GetLessonsAsync(Guid userId)
        {
            var lessons = await _context.Lessons.Where(x => x.EkleyenKullaniciId == userId).ToListAsync();
            return _mapper.Map<IEnumerable<LessonDto>>(lessons);
        }

        public async Task<LessonDto> CreateLessonAsync(CreateLessonDto createLessonDto)
        {
            var lesson = _mapper.Map<Lesson>(createLessonDto);
            lesson.EklemeTarihi = DateTime.Now;

            lesson.EkleyenKullaniciId = createLessonDto.EkleyenKullaniciId;
            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();
            return _mapper.Map<LessonDto>(lesson);
        }

        public async Task<LessonDto> GetLessonByIdAsync(Guid id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            
            return _mapper.Map<LessonDto>(lesson);
        }


        public async Task<Lesson> GetLessonByIdForUpdatesAsync(Guid id)
        {
            var lesson = await _context.Lessons.FindAsync(id);

            return (lesson);
        }

        public async Task<int> DeleteLessonAsync(Guid id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
            {
                return -1;
            }
            _context.Lessons.Remove(lesson);

            return await _context.SaveChangesAsync(); ;
        }

        public async Task<LessonDto> UpdateLessonAsync(UpdateLessonDto updateLessonDto)
        {
            var lesson = _mapper.Map<Lesson>(updateLessonDto);
            lesson.GuncellemeTarihi = DateTime.Now;
            lesson.GuncelleyenKullaniciId = updateLessonDto.GuncelleyenKullaniciId;
            //var oldRecord = GetLessonByIdAsync(lesson.Id);
            //lesson.EkleyenKullaniciId = oldRecord.ekl

            _context.Lessons.Update(lesson);
            await _context.SaveChangesAsync();
            return _mapper.Map<LessonDto>(lesson);

        }

    }
}
