namespace Domain.DTOs.Session
{
    public class CreateSessionDto
    {
        public Guid StudentId { get; set; }
        public Guid TeacherId { get; set; }
        public Guid LessonId { get; set; }
        public DateTime Date { get; set; }
        public int DurationInHours { get; set; }
        public decimal HourlyPrice { get; set; }
    }
}
