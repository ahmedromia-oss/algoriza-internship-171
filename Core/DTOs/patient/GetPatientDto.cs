using Core.Domain;
using Core.DTOs.Appointments;
using Core.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.patient
{
    public class GetPatientDto
    {
        public GetUserDto user { get; set; }
        public ICollection<getTimeDtoForUser> bookings { get; set; }
    }

    
}
