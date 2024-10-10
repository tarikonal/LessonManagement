using Domain.DTOs.Student;

namespace Domain.DTOs.Family
{
    public class FamilyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<StudentDto> Members { get; set; }
        public Guid? EkleyenKullaniciId { get; set; }
        public Guid? GuncelleyenKullaniciId { get; set; }
        public DateTime? EklemeTarihi { get; set; }
    }
}
