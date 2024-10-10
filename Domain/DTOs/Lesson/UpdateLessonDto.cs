using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Lesson
{
    public class UpdateLessonDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Guid? GuncelleyenKullaniciId { get; set; }
        public Guid? EkleyenKullaniciId { get; set; }
        public DateTime? EklemeTarihi { get; set; } // Kaydın eklenme tarihi.
    }
}

