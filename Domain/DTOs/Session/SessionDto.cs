﻿namespace Domain.DTOs.Session
{
    public class SessionDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid TeacherId { get; set; }
        public Guid LessonId { get; set; }
        public string studentName { get; set; }
        public string teacherName { get; set; }
        public string lessonName { get; set; }
        public DateTime Date { get; set; }
        public int DurationInHours { get; set; }
        public decimal HourlyPrice { get; set; }
        public Guid? EkleyenKullaniciId { get; set; }
        public Guid? GuncelleyenKullaniciId { get; set; }
        public DateTime? EklemeTarihi { get; set; }
    }
}
