using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace Core.DTOs.Appointments
{
    public class AddApointments
    {
        public double price { get; set; }

        public ICollection<AddDayTimeDto> Days { get; set; }
    }
}
