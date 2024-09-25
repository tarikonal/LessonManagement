using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Domain.Entities
{
    public class Teacher : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}

