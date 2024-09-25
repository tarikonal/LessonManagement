using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Lesson
{
    public class UpdateLessonDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

