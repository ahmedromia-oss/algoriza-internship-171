using AutoMapper;
using Core.Domain;
using Core.DTOs.patient;
using Core.DTOs.Specialization;
using Core.DTOs.User;
using Core.Interfaces;
using Core.ReboInterfaces;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository users { get; set; }
        public IAppointmentRepository appointment { get; set; }
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;
        public IPatientRepository patients { get; set; } 
        public IDoctorRepository doctors { get; set; }
       public IRepository<Specialization , GetSpecializationDto> specializations { get; set; }

        public UnitOfWork(AppDbContext appDbContext , IMapper mapper , IFileOperation fileOperation)
        {
            appointment = new AppointmentRepository(appDbContext , mapper);
            users = new UserRepository(appDbContext , mapper , fileOperation);
            doctors = new  DoctorRepository(appDbContext , mapper);
            specializations = new Repository<Specialization , GetSpecializationDto>(appDbContext , mapper);
            patients= new PatientRepository(appDbContext, mapper);
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }
       

        public async Task<bool> complete()
        {
            try
            {
                await this.appDbContext.SaveChangesAsync();
                return true;
            } catch
            {
                return false;
            }
        }

       
    }
}
