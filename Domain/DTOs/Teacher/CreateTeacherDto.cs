namespace Domain.DTOs.Teacher
{
    public class CreateTeacherDto
    {
        public string Name { get; set; }
        public Guid? EkleyenKullaniciId { get; set; }
    }
}
