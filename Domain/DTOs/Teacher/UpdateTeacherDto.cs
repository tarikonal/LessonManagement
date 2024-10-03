namespace Domain.DTOs.Teacher
{
    public class UpdateTeacherDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? GuncelleyenKullaniciId { get; set; }
    }
}
