using Domain.DTOs.Teacher;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherDto>> GetTeachersAsync(Guid userId);
        Task<TeacherDto> GetTeacherByIdAsync(Guid id);
        Task<TeacherDto> CreateTeacherAsync(CreateTeacherDto createTeacherDto);
        Task<TeacherDto> UpdateTeacherAsync(UpdateTeacherDto updateTeacherDto);
        Task<int> DeleteTeacherAsync(Guid id);
    }
}
