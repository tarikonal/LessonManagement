//using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.DTOs.Student;

namespace Infrastructure.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public StudentService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentDto>> GetStudentsAsync()
        {
            var students = await _context.Students.ToListAsync();
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }

        public async Task<StudentDto> GetStudentByIdAsync(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            return _mapper.Map<StudentDto>(student);
        }

        public async Task<StudentDto> CreateStudentAsync(CreateStudentDto createStudentDto)
        {
            var student = _mapper.Map<Student>(createStudentDto);
            student.EklemeTarihi = DateTime.Now;
            student.EkleyenKullaniciId = createStudentDto.EkleyenKullaniciId;
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return _mapper.Map<StudentDto>(student);
        }

        public async Task<StudentDto> UpdateStudentAsync(UpdateStudentDto updateStudentDto)
        {
            var student = await _context.Students.FindAsync(updateStudentDto.Id);
            if (student == null)
            {
                return null;
            }

            _mapper.Map(updateStudentDto, student);
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            return _mapper.Map<StudentDto>(student);
        }

        public async Task<int> DeleteStudentAsync(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return -1;
            }

            _context.Students.Remove(student);
            return await _context.SaveChangesAsync();
        }
    }
}
