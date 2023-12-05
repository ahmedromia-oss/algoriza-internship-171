using Core.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Doctor
{
    public class addDoctorDto
    {
        [Required]
        public string specializationId { get; set; }
      
    }
}
