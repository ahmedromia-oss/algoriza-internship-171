using Core.Domain;
using Core.DTOs.patient;
using Core.DTOs.Specialization;
using Core.DTOs.User;
using Core.ReboInterfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        public ITimeRepository time { get;set; }
        public IDashBoardRepository requests { get; set; }
        public IDoctorRepository doctors { get; set; }
        public IAppointmentRepository appointment { get; set; }

        public IRepository<Specialization , GetSpecializationDto> specializations { get; set; }

        public IDiscountRepository discount { get; set; }
        public IUserRepository users { get; set; }

        public IPatientRepository patients { get; set; }

        


        public Task<bool> complete();
        public Task<bool> RollBack();
       
        public Task<bool> startTransaction();
      
        public Task<bool> commitTransaction();
       

        
    }
}
