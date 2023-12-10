using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Specialization
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
