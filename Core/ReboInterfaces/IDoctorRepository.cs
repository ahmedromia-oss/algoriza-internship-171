using Core.Domain;
using Core.DTOs.Doctor;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IDoctorRepository:IRepository<Doctor , GetDoctorDto>
    {
        public Task<Doctor> addDoctor(addDoctorDto addDoctorDto , User user);

        public Task<ICollection<GetDoctorDto>> getAllDoctors(PaginationModel paginationModel , string searchTerm);
        public Task<GetDoctorDto> updateDoctor(updateDoctorDto updateDoctorDto , string userId);

        

      
    }
}
