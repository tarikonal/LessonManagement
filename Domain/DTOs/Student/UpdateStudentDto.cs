using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Student;

public class UpdateStudentDto
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public Guid FamilyId { get; set; }
    public Guid? GuncelleyenKullaniciId { get; set; }
    public Guid? EkleyenKullaniciId { get; set; }
    public DateTime? EklemeTarihi { get; set; }
}
