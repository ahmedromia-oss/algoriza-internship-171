using Core.DTOs.Doctor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Appointments
{
    public class AddDayTimeDto
    {
        [Range(1, 7)]
        public int day { get; set; }
        public ICollection<AddTimeDto> times { get; set; }
        
    }
}
