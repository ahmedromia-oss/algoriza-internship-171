using AutoMapper.QueryableExtensions;
using AutoMapper;
using Core.Domain;
using Core.DTOs.Appointments;
using Core.DTOs.bookers;
using Core.DTOs.Discount;
using Core.DTOs.Doctor;
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
    public class TimeRepository:ITimeRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public TimeRepository(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }
        public async Task<bool> addAppointment(AddApointments addApointments, string Email)
        {

            Doctor doctor = await this.appDbContext.doctors.Where(e => e.user.Email == Email).FirstOrDefaultAsync();
            foreach (AddDayTimeDto d in addApointments.Days)
            {
                Day dayToAdd = await this.appDbContext.Day.Where(e => e.Id == Convert.ToInt32(d.day)).FirstOrDefaultAsync();

                doctor.Days.Add(dayToAdd);
                dayToAdd.doctors.Add(doctor);
                this.appDbContext.doctors.Update(doctor);
                this.appDbContext.Day.Update(dayToAdd);
                foreach (AddTimeDto time in d.times)
                {

                    if (!(await this.appDbContext.Time.AnyAsync(e => e.DayId == Convert.ToInt32(d.day) && e.DocotorId == doctor.userId && e.time == TimeOnly.Parse(time.time))))
                    {
                        await this.appDbContext.Time.AddAsync(new Time { Doctor = doctor, day = dayToAdd, time = TimeOnly.Parse(time.time), price = addApointments.price });
                    }
                    else
                    {
                        throw new Exception("Time at " + ((Enums.Days)d.day).ToString() + " at " + time.time + " clock is Taken");
                    }
                }
            }
            return true;

        }

       

        public async Task<bool> deleteTime(string email, string timeId)
        {
            Time timeToEdit = await this.appDbContext.Time.Where(e => e.Id == timeId && e.Doctor.user.Email == email).FirstOrDefaultAsync();
            if (timeToEdit != null)
            {


                if (!(await this.appDbContext.Time.Where(e => e.Id == timeId).Select(e => e.IsBooked).FirstOrDefaultAsync()))
                {
                    this.appDbContext.Time.Remove(timeToEdit);
                    return true;
                }
                throw new Exception("Time is Booked can't be deleted");



            }
            throw new FileNotFoundException("No Time with this Id");
        }
        public async Task<bool> updateTime(string email, string timeId, updateAppointment updateAppointment)
        {
            Time timeToEdit = await this.appDbContext.Time.Where(e => e.Id == timeId && e.Doctor.user.Email == email).FirstOrDefaultAsync();
            if (timeToEdit != null)
            {
                timeToEdit.time = TimeOnly.Parse(updateAppointment.time);
                if (!(await this.appDbContext.Time.AnyAsync(e => e.time == timeToEdit.time && e.DayId == timeToEdit.DayId && e.Doctor.user.Email == email)))
                {
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

       
    }
}

