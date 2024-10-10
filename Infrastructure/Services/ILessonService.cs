using Domain.DTOs.Lesson;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface ILessonService
    {
        Task<IEnumerable<LessonDto>> GetLessonsAsync(Guid userId);
        Task<LessonDto> CreateLessonAsync(CreateLessonDto createLessonDto);
        Task<LessonDto> GetLessonByIdAsync(Guid id);
        Task<int> DeleteLessonAsync(Guid id);
        Task<LessonDto> UpdateLessonAsync(UpdateLessonDto updateLessonDto);
    
    }
}
