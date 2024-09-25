using Domain.Entities;

namespace Domain.DTOs.Student;

public class StudentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid FamilyId { get; set; }
    public Domain.Entities.Family Family { get; set; }
    public ICollection<Domain.Entities.Session> Sessions { get; set; } = new List<Domain.Entities.Session>();
}
