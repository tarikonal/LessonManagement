using Domain.DTOs.Student;

namespace Infrastructure.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetStudentsAsync(Guid userId);
        Task<StudentDto> GetStudentByIdAsync(Guid id);
        Task<StudentDto> CreateStudentAsync(CreateStudentDto createStudentDto);
        Task<StudentDto> UpdateStudentAsync(UpdateStudentDto updateStudentDto);
        Task<int> DeleteStudentAsync(Guid id);
    }
}