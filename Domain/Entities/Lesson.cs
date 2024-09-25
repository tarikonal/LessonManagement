using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Lesson : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}
