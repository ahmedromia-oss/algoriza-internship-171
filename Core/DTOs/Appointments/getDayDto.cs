using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs.Appointments;

namespace Core.DTOs.Doctor
{
    public class getDayDto
    {
        public string day { get; set; }
        public ICollection<getTimeDto> times{get; set; }
    }
}
