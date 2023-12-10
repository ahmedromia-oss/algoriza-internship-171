using Core.Domain;
using Core.DTOs.Appointments;
using Core.DTOs.Auth;
using Core.DTOs.bookers;
using Core.DTOs.patient;
using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IPatientService
    {
        public Task<bool> addPatient(addPatientDto addPatientDto , User user);
        public Task<ICollection<GetPatientDto>> getAll(PaginationModel paginationModel , string searchTerm);

        public Task<GetPatientDto> getById(string id);
        public Task<GetPatientDto> loggedInUser(string email);

        public Task<PatientTime> book(string timeId , string Email);

        public Task<ICollection<getBookings>> bookings(string email);

        public Task<PatientTime> cancelBooking(string email , string timeId);
        public Task<int> count();


    }
}
