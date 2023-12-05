using Core.DTOs.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Appointments
{
    public class getTimeForBookers
    {
        public string Id { get; set; }
        public TimeOnly Time { get; set; }
        public getOnlyDayDTO Day { get; set; }
    }
}
