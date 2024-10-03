using AutoMapper;
using Domain.DTOs.Teacher;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TeacherService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TeacherDto>> GetTeachersAsync()
        {
            var teachers = await _context.Teachers.ToListAsync();
            return _mapper.Map<IEnumerable<TeacherDto>>(teachers);
        }

        public async Task<TeacherDto> GetTeacherByIdAsync(Guid id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            return _mapper.Map<TeacherDto>(teacher);
        }

        public async Task<TeacherDto> CreateTeacherAsync(CreateTeacherDto createTeacherDto)
        {
            var teacher = _mapper.Map<Teacher>(createTeacherDto);
            teacher.EklemeTarihi = DateTime.Now;
            teacher.EkleyenKullaniciId = createTeacherDto.EkleyenKullaniciId;
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
            return _mapper.Map<TeacherDto>(teacher);
        }

        public async Task<TeacherDto> UpdateTeacherAsync(UpdateTeacherDto updateTeacherDto)
        {
            var teacher = _mapper.Map<Teacher>(updateTeacherDto);
            teacher.GuncellemeTarihi = DateTime.Now;
            teacher.GuncelleyenKullaniciId = updateTeacherDto.GuncelleyenKullaniciId;
            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync();
            return _mapper.Map<TeacherDto>(teacher);
        }

        public async Task<int> DeleteTeacherAsync(Guid id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return -1;
            }
            _context.Teachers.Remove(teacher);
            return await _context.SaveChangesAsync();
        }
    }
}
