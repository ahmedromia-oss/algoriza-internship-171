using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Day
    {
       
        public int Id { get; set; }
        public virtual ICollection<Doctor> doctors { get; set; }
        public virtual ICollection<Time> times { get; set; }
    }
}
