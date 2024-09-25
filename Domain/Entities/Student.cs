using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Domain.Entities
{
    public class Student : BaseEntity
    {
      
        public string Name { get; set; }
        public Guid FamilyId { get; set; }
        public Family Family { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}

