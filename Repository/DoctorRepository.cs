using AutoMapper;
using Core.Domain;
using Core.DTOs.Auth;
using Core.DTOs.Doctor;
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

        public async Task<bool> addAppointment(AddAppointmentDto addAppointmentDto ,  string Email)
        {
           

            Doctor doctor = await this.appDbContext.doctors.Where(e=>e.user.Email == Email).FirstOrDefaultAsync();
            Day dayToAdd = await this.appDbContext.Day.Where(e => e.Id == addAppointmentDto.day).FirstOrDefaultAsync();
            doctor.Days.Add(dayToAdd);
            dayToAdd.doctors.Add(doctor);
            this.appDbContext.doctors.Update(doctor);
            this.appDbContext.Day.Update(dayToAdd);

            await this.appDbContext.Time.AddAsync(new Time { Doctor = doctor, day = dayToAdd, time = TimeOnly.Parse(addAppointmentDto.time) });           
                return true;
        
        }

        public async Task<GetDoctorDto> addDoctor(addDoctorDto addDoctorDto , User user)
        {
                Doctor doctor = new Doctor();
                Specialization specialization = await this.appDbContext.specializations.Where(e => e.Id.ToString() == addDoctorDto.specializationId).FirstOrDefaultAsync();
                doctor = mapper.Map<Doctor>(addDoctorDto);
                doctor.user = user;
                doctor.specialization = specialization;
                await this.appDbContext.doctors.AddAsync(doctor);
                return mapper.Map<GetDoctorDto>(doctor);
            
                     
        }

        public async Task<GetDoctorDto> updateDoctor(updateDoctorDto updateDoctorDto , string userId)
        {
            Doctor doctor = await this.appDbContext.doctors.Where(e=>e.userId==userId).FirstOrDefaultAsync();
            if (doctor == null)
            {
                throw new Exception("Not Found");

            }
           
            
            mapper.Map<updateDoctorDto , Doctor>(updateDoctorDto , doctor);
            if (updateDoctorDto.specializationId != null)
            {
                Specialization specialization = await this.appDbContext.specializations.Where(e => e.Id.ToString() == updateDoctorDto.specializationId).FirstOrDefaultAsync();
                doctor.specialization = specialization;
            }
           
            this.appDbContext.doctors.Update(doctor);
            this.appDbContext.Users.Update(doctor.user);
            return mapper.Map<GetDoctorDto>(doctor);
        }
        

      

        
    }
}
