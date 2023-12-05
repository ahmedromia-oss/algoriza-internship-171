using Core.Domain;
using Core.DTOs.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IDoctorRepository:IRepository<Doctor , GetDoctorDto>
    {
        public Task<GetDoctorDto> addDoctor(addDoctorDto addDoctorDto , User user);
        public Task<GetDoctorDto> updateDoctor(updateDoctorDto updateDoctorDto , string userId);

        

      
    }
}
