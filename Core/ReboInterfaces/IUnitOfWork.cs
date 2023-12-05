using Core.Domain;
using Core.DTOs.patient;
using Core.DTOs.Specialization;
using Core.DTOs.User;
using Core.ReboInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        public IDoctorRepository doctors { get; set; }
        public IAppointmentRepository appointment { get; set; }

        public IRepository<Specialization , GetSpecializationDto> specializations { get; set; }
        public IUserRepository users { get; set; }

        public IPatientRepository patients { get; set; }

        


        public Task<bool> complete();

        
    }
}
