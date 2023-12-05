using Core.Domain;
using Core.DTOs.Specialization;
using Core.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Doctor
{
    public class GetDoctorDto
    {
       
        public GetUserDto user { get; set; }
        public GetSpecializationDto specialization { get; set; }
        public ICollection<getDayDto> Days { get; set; }
    }
}
