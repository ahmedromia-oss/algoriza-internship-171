using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Domain;
using Core.DTOs.Appointments;
using Core.DTOs.bookers;
using Core.DTOs.Discount;
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
        private async Task<bool> markAsBooked(string timeId)
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

       private async Task<bool> markAsUnBooked(string timeId)
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


        public async Task<PatientTime> bookAppointment(string timeId, string Email)
        {
            Patient patient = await this.appDbContext.patients.Where(e => e.user.Email == Email).FirstOrDefaultAsync();
            Time time = await this.appDbContext.Time.FindAsync(timeId);
            if(time != null)
            {
                if (!(time.IsBooked))
                {

                    PatientTime patientTime = new PatientTime { patient = patient, time = time, timeId = time.Id, patientId = patient.userId , finalPrice = time.price };
                    await this.appDbContext.PatientTime.AddAsync(patientTime);
                    await this.markAsBooked(patientTime.timeId);

                    return patientTime;
                }
                throw new Exception("This Appointment Is Booked");
            }
            throw new FileNotFoundException("No Time with this Id");
            
        }

        public async Task<PatientTime> cancelAppointment(string email, string timeId)
        {
            string userId = await this.appDbContext.patients.Where(e => e.user.Email == email).Select(e => e.userId).FirstOrDefaultAsync();
            PatientTime patientTime = await this.appDbContext.PatientTime.Where(e=>e.patientId == userId && e.Id == timeId).FirstOrDefaultAsync();
            if(patientTime != null)
            {
                if (patientTime.status == Convert.ToInt32(Enums.BookingStatus.pending))
                {
                    patientTime.status = Convert.ToInt32(Enums.BookingStatus.cancelled);
                    await this.markAsUnBooked(patientTime.timeId);
                    this.appDbContext.Update(patientTime);
                    return patientTime;
                }
                throw new Exception("Can't cancel book");
            }
            throw new FileNotFoundException("No Booking with this Time");


        }

       
        public async Task<bool> checkIfHaveRequests(string doctorId)
        {
            return await this.appDbContext.PatientTime.AnyAsync(e => e.time.DocotorId == doctorId && e.status == Convert.ToInt32(Enums.BookingStatus.pending));
        }
        

        public async Task<bool> confirmCheckUp(string timeId , string doctorEmail)
        {
            string doctorId = await this.appDbContext.doctors.Where(e => e.user.Email == doctorEmail).Select(e=>e.userId).FirstOrDefaultAsync(); 
            
            PatientTime patientTime = await this.appDbContext.PatientTime.Where(e=>e.Id == timeId && e.time.DocotorId == doctorId).FirstOrDefaultAsync();
            if (patientTime != null)
            {
                if (patientTime.status == Convert.ToInt32(Enums.BookingStatus.pending))
                {
                    patientTime.status = Convert.ToInt32(Enums.BookingStatus.completed);
                    await this.markAsUnBooked(patientTime.timeId);

                    this.appDbContext.Update(patientTime);
                    return true;
                }
                throw new Exception("Can't confirm book");
            }
            throw new FileNotFoundException("No Booking with this Time");
        }

      

        

        public async Task<ICollection<getBookers>> getBookers(string email , int day , PaginationModel paginationModel)
        {
            if (day != 0) {
                ICollection<getBookers> bookers = await this.appDbContext.PatientTime.
                    Where(
                    (e => e.time.Doctor.user.Email == email & day == e.time.DayId)
                    )
                    .ProjectTo<getBookers>(mapper.ConfigurationProvider)
                    .Skip(((paginationModel.Page == 0 ? 1 : paginationModel.Page) - 1) * paginationModel.PageSize)
                    .Take(paginationModel.PageSize == 0 ? 10 : paginationModel.PageSize)
                   
                    .ToListAsync();
                return bookers;
            }
            else
            {
                ICollection<getBookers> bookers = await this.appDbContext.PatientTime.
                    Where(
                    (e => e.time.Doctor.user.Email == email)
                    )
                    .ProjectTo<getBookers>(mapper.ConfigurationProvider)
                     .Skip(((paginationModel.Page == 0 ? 1 : paginationModel.Page) - 1) * paginationModel.PageSize)
                    .Take(paginationModel.PageSize == 0 ? 10 : paginationModel.PageSize)
                    .ToListAsync();
                return bookers;
            }
        }

        public async Task<ICollection<getBookings>> getBookings(string email)
        {
            return await this.appDbContext.PatientTime.Where(e=>e.patient.user.Email == email).Select(e=>new getBookings{
                Id = e.Id,
                doctor =mapper.Map<getDoctorZipped>(e.time.Doctor),
                time = mapper.Map<getTimeForBookers>(e.time),
                status  = ((Enums.BookingStatus)e.status).ToString(),
                finalPrice = e.finalPrice,
                discount = mapper.Map<getDiscountDto>(e.discount)
                

            }).ToListAsync();
        }
    }
}
