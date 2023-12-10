using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Doctor
    {
        
        public string userId { get; set; }
        [Required]
        public Guid SpecializationId { get; set; }
        [Required]
        public virtual Specialization specialization { get; set; } 
        public virtual User user { get; set; }

        public virtual ICollection<Day> Days{ get; set;}
        public virtual ICollection<Time> Times { get; set; }

    }
}
