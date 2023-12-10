using AutoMapper;
using Core.Domain;
using Core.DTOs.patient;
using Core.DTOs.Specialization;
using Core.DTOs.User;
using Core.Interfaces;
using Core.ReboInterfaces;
using Core.Services;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork 
    {
        public IDashBoardRepository requests { get; set; }
        public IUserRepository users { get; set; }
        public ITimeRepository time { get; set; }
        public IAppointmentRepository appointment { get; set; }
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;
        public IPatientRepository patients { get; set; } 
        public IDoctorRepository doctors { get; set; }
       public IRepository<Specialization , GetSpecializationDto> specializations { get; set; }
        public IDiscountRepository discount { get; set; }

        public UnitOfWork(AppDbContext appDbContext , IMapper mapper , IFileOperation fileOperation)
        {
            time = new TimeRepository(appDbContext , mapper);
            requests = new DashBoardRepository(appDbContext , mapper);
            discount = new DiscountRebository(mapper, appDbContext);
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
            

                await this.appDbContext.SaveChangesAsync();

                return true;
            
          
            
        }

        public async Task<bool> startTransaction()
        {
            await this.appDbContext.Database.BeginTransactionAsync();
            return true;
        }

        
        public async Task<bool> RollBack()
        {
            await this.appDbContext.Database.RollbackTransactionAsync();
            return true;
        }
        public async Task<bool> commitTransaction()
        {
            await this.appDbContext.Database.CommitTransactionAsync();
            return true;
        }
    }
}
