using Domain.DTOs.Student;

namespace Domain.DTOs.Family
{
    public class FamilyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<StudentDto> Members { get; set; }
    }
}
