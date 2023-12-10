using Core.Domain;
using Core.DTOs.Appointments;
using Core.DTOs.bookers;
using Core.DTOs.Doctor;
using Core.DTOs.patient;
using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ReboInterfaces
{
    public interface IAppointmentRepository
    {

        public Task<bool> checkIfHaveRequests(string doctorId);
        public Task<PatientTime> bookAppointment(string timeId , string Email);
        public Task<ICollection<getBookers>> getBookers(string email , int day , PaginationModel paginationModel);
        public Task<ICollection<getBookings>> getBookings(string email);

        public Task<PatientTime> cancelAppointment(string email ,string timeId);

        public Task<bool> confirmCheckUp(string timeId , string doctorEmail);

        


    }
}
