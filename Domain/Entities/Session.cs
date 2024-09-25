using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Session : BaseEntity
    {
       
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public Guid LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public DateTime Date { get; set; }
        public int DurationInHours { get; set; }
        public decimal HourlyPrice { get; set; }
    }
}

