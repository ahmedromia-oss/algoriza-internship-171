using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Domain;
using Core.DTOs.Appointments;
using Core.DTOs.Auth;
using Core.DTOs.Doctor;
using Core.DTOs.Specialization;
using Core.DTOs.User;
using Core.Interfaces;
using Core.Models;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DoctorRepository : Repository<Doctor , GetDoctorDto>, IDoctorRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public DoctorRepository(AppDbContext appDbContext  , IMapper mapper):base(appDbContext , mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }

       
        
        

        public async Task<Doctor> addDoctor(addDoctorDto addDoctorDto , User user)
        {
                Doctor doctor = new Doctor();
                Specialization specialization = await this.appDbContext.specializations.Where(e => e.Id.ToString() == addDoctorDto.specializationId).FirstOrDefaultAsync();
            if(specialization == null)
            {
                throw new FileNotFoundException("No specialization with this Id");
            }
                doctor = mapper.Map<Doctor>(addDoctorDto);
                doctor.user = user;
                doctor.specialization = specialization;
            doctor.SpecializationId = specialization.Id;
            
                await this.appDbContext.doctors.AddAsync(doctor);
            
                return (doctor);
            
                     
        }

        public async Task<ICollection<GetDoctorDto>> getAllDoctors(PaginationModel paginationModel , string searchTerm)
        {

            return await this.appDbContext.doctors
                .Where(e => e.user.Email.Contains(searchTerm ?? "") || e.user.FullName.Contains(searchTerm ?? ""))
                .Select(d =>
            new GetDoctorDto
            {
                user = mapper.Map<GetUserDto>(d.user),
                Days = mapper.Map<ICollection<getDayDto>>(d.Days.Select(e => new Day {Id = e.Id , times = e.times.Where(e => e.DocotorId == d.userId).ToList() }).ToList()),
                specialization = mapper.Map<GetSpecializationDto>(d.specialization)

            })
                .Skip(((paginationModel.Page == 0 ? 1 : paginationModel.Page) - 1) * paginationModel.PageSize).Take(paginationModel.PageSize == 0 ? 10 : paginationModel.PageSize).ToListAsync();
        }

        public async Task<GetDoctorDto> updateDoctor(updateDoctorDto updateDoctorDto , string userId)
        {
            Doctor doctor = await this.appDbContext.doctors.Where(e=>e.userId==userId).FirstOrDefaultAsync();
            if (doctor == null)
            {
                throw new FileNotFoundException("No Doctor with this Id");

            }
           
            
            mapper.Map<updateDoctorDto , Doctor>(updateDoctorDto , doctor);
            if (updateDoctorDto.specializationId != null)
            {
                Specialization specialization = await this.appDbContext.specializations.Where(e => e.Id.ToString() == updateDoctorDto.specializationId).FirstOrDefaultAsync();
                if(specialization == null) {
                    throw new FileNotFoundException("No specialization with this Id");
                }
                doctor.specialization = specialization;
            }
           
            this.appDbContext.doctors.Update(doctor);
            this.appDbContext.Users.Update(doctor.user);
            return mapper.Map<GetDoctorDto>(doctor);
        }
        

      

        
    }
}
