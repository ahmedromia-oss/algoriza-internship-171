using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Domain;
using Core.DTOs.Appointments;
using Core.DTOs.bookers;
using Core.DTOs.Doctor;
using Core.DTOs.patient;
using Core.DTOs.User;
using Core.Models;
using Core.ReboInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public AppointmentRepository(AppDbContext appDbContext , IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }
        public async Task<bool> addAppointment(AddAppointmentDto addAppointmentDto, string Email)
        {
            
                Doctor doctor = await this.appDbContext.doctors.Where(e => e.user.Email == Email).FirstOrDefaultAsync();
                Day dayToAdd = await this.appDbContext.Day.Where(e => e.Id == Convert.ToInt32(addAppointmentDto.day)).FirstOrDefaultAsync();
                doctor.Days.Add(dayToAdd);
                dayToAdd.doctors.Add(doctor);
                this.appDbContext.doctors.Update(doctor);
                this.appDbContext.Day.Update(dayToAdd);
                await this.appDbContext.Time.AddAsync(new Time { Doctor = doctor, day = dayToAdd, time = TimeOnly.Parse(addAppointmentDto.time) });
                return true;   
        }

        public async Task<bool> bookAppointment(string timeId, string Email)
        {
            Patient patient = await this.appDbContext.patients.Where(e => e.user.Email == Email).FirstOrDefaultAsync();
            Time time = await this.appDbContext.Time.FindAsync(timeId);
            if(time != null)
            {
                if (!(time.IsBooked))
                {


                    await this.appDbContext.PatientTime.AddAsync(new PatientTime { patient = patient, time = time, timeId = time.Id, patientId = patient.userId });
                    await this.markAsBooked(timeId);

                    return true;
                }
                throw new Exception("This Appointment Is Booked");
            }
            throw new FileNotFoundException("No Time with this Id");
            
        }

        public async Task<bool> cancelAppointment(string email, string timeId)
        {
            string userId = await this.appDbContext.patients.Where(e => e.user.Email == email).Select(e => e.userId).FirstOrDefaultAsync();
            PatientTime patientTime = await this.appDbContext.PatientTime.Where(e=>e.patientId == userId && e.time.Id == timeId).FirstOrDefaultAsync();
            if(patientTime != null)
            {
                if (patientTime.status == Convert.ToInt32(Enums.BookingStatus.pending))
                {
                    patientTime.status = Convert.ToInt32(Enums.BookingStatus.cancelled);
                    await this.markAsUnBooked(timeId);
                    this.appDbContext.Update(patientTime);
                    return true;
                }
                throw new Exception("Can't cancel book");
            }
            throw new FileNotFoundException("No Booking with this Time");


        }

        public async Task<bool> confirmCheckUp(string userId, string timeId , string doctorEmail)
        {
            string doctorId = await this.appDbContext.doctors.Where(e => e.user.Email == doctorEmail).Select(e=>e.userId).FirstOrDefaultAsync(); 
            
            PatientTime patientTime = await this.appDbContext.PatientTime.Where(e => e.patientId == userId && e.time.Id == timeId && e.time.DocotorId == doctorId).FirstOrDefaultAsync();
            if (patientTime != null)
            {
                if (patientTime.status == Convert.ToInt32(Enums.BookingStatus.pending))
                {
                    patientTime.status = Convert.ToInt32(Enums.BookingStatus.completed);
                    await this.markAsUnBooked(timeId);

                    this.appDbContext.Update(patientTime);
                    return true;
                }
                throw new Exception("Can't confirm book");
            }
            throw new FileNotFoundException("No Booking with this Time");
        }

        public async Task<bool> deleteTime(string email, string timeId)
        {
            Time timeToEdit = await this.appDbContext.Time.Where(e => e.Id == timeId && e.Doctor.user.Email == email).FirstOrDefaultAsync();
            if (timeToEdit != null)
            {
              
               
                    if (!(await this.appDbContext.Time.Where(e=>e.Id == timeId).Select(e=>e.IsBooked).FirstOrDefaultAsync()))
                    {
                        this.appDbContext.Time.Remove(timeToEdit);
                    return true;
                    }
                    throw new Exception("Time is Booked can't be deleted");

               

            }
            throw new FileNotFoundException("No Time with this Id");
        }

        public async Task<ICollection<getBookers>> getBookers(string email)
        {

            ICollection<getBookers> bookers = await this.appDbContext.PatientTime.Where(e => e.time.Doctor.user.Email == email).ProjectTo<getBookers>(mapper.ConfigurationProvider).ToListAsync();
            return bookers;
        }

        public async Task<ICollection<getBookings>> getBookings(string email)
        {
            return await this.appDbContext.PatientTime.Where(e=>e.patient.user.Email == email).Select(e=>new getBookings{

                doctor =mapper.Map<getDoctorZipped>(e.time.Doctor),
                time = mapper.Map<getTimeForBookers>(e.time),
                status  = ((Enums.BookingStatus)e.status).ToString()

            }).ToListAsync();
        }

        public async Task<bool> updateTime(string email, string timeId , updateAppointment updateAppointment)
        {
            Time timeToEdit = await this.appDbContext.Time.Where(e => e.Id == timeId && e.Doctor.user.Email == email).FirstOrDefaultAsync();
            if(timeToEdit != null)
            {
                timeToEdit.time = TimeOnly.Parse(updateAppointment.time);
                if(!(await this.appDbContext.Time.AnyAsync(e=>e.time == timeToEdit.time && e.DayId == timeToEdit.DayId && e.Doctor.user.Email == email))) {
                    if (!(await this.appDbContext.Time.Where(e => e.Id == timeId).Select(e => e.IsBooked).FirstOrDefaultAsync()))
                    {
                        this.appDbContext.Time.Update(timeToEdit);
                        return true;
                    }
                    throw new Exception("Time is Booked can't Update");

                }
                throw new Exception("Time is Taken");

            }
            throw new FileNotFoundException("No Time with this Id");
        }

        public async Task<bool> markAsBooked(string timeId)
        {
            Time time = await this.appDbContext.Time.FindAsync(timeId);
            if (time != null)
            {
                time.IsBooked = true;
                this.appDbContext.Time.Update(time);
                return true;
            }
            return false;

        }

        public async Task<bool> markAsUnBooked(string timeId)
        {
            Time time = await this.appDbContext.Time.FindAsync(timeId);
            if (time != null)
            {
                time.IsBooked = false;
                this.appDbContext.Time.Update(time);
                return true;
            }
            return false;
        }
    }
}
