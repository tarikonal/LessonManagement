namespace Domain.DTOs.Student;

public class CreateStudentDto
    {
        public string Name { get; set; }
        public Guid FamilyId { get; set; }
        public Guid? EkleyenKullaniciId { get; set; }
}
