using Core.Domain;
using Core.DTOs.Appointments;
using Core.DTOs.bookers;
using Core.DTOs.Doctor;
using Core.DTOs.patient;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ReboInterfaces
{
    public interface IAppointmentRepository
    {
        public Task<bool> addAppointment(AddAppointmentDto addAppointmentDto ,string Email);

        public Task<bool> bookAppointment(string timeId , string Email);
        public Task<ICollection<getBookers>> getBookers(string email);
        public Task<ICollection<getBookings>> getBookings(string email);

        public Task<bool> cancelAppointment(string email ,string timeId);

        public Task<bool> confirmCheckUp(string userId ,string timeId , string doctorEmail);

        public Task<bool> updateTime(string email, string timeId , updateAppointment updateAppointment);
        public Task<bool> deleteTime(string email, string timeId);

        public Task<bool> markAsBooked(string timeId);
        public Task<bool> markAsUnBooked(string timeId);


    }
}
