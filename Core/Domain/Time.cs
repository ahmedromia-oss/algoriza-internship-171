using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Time
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public int DayId { get; set; }

        public virtual Doctor Doctor { get; set; }
        public string DocotorId { get; set; }
        public virtual Day day { get; set; }

        public TimeOnly time { get; set; }

        public virtual ICollection<Patient> bookers { get; set; }

        public bool IsBooked { get; set; }

    }
}
