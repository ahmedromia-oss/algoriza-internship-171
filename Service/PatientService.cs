using Core.Domain;
using Core.DTOs.Appointments;
using Core.DTOs.Auth;
using Core.DTOs.bookers;
using Core.DTOs.patient;
using Core.Interfaces;
using Core.Models;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork unitOfWork;
        public PatientService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<bool> addPatient(addPatientDto addPatientDto , User user)
        {
                await this.unitOfWork.patients.addPatient(addPatientDto , user);
                return await this.unitOfWork.complete();

          
        }

        public async Task<PatientTime> book(string timeId, string Email)
        {
            PatientTime patientTime =  await this.unitOfWork.appointment.bookAppointment(timeId, Email);
            await this.unitOfWork.complete();
            return patientTime;
        }

        public async Task<ICollection<getBookings>> bookings(string email)
        {
           return await this.unitOfWork.appointment.getBookings(email);
        }

        public async Task<PatientTime> cancelBooking(string email, string timeId)
        {
            var result = await this.unitOfWork.appointment.cancelAppointment(email, timeId);
            await this.unitOfWork.complete();
            return result;
        }

        public async Task<int> count()
        {
            return await this.unitOfWork.patients.count();
        }

        public async Task<ICollection<GetPatientDto>> getAll(PaginationModel paginationModel , string searchTerm)
        {
            return await this.unitOfWork.patients.GetAll(paginationModel , e=>e.user.Email.Contains(searchTerm) || e.user.FullName.Contains(searchTerm));
        }

        public async Task<GetPatientDto> getById(string id)
        {
            return await this.unitOfWork.patients.GetByWhere(e=>e.userId == id);
        }

        public async Task<GetPatientDto> loggedInUser(string email)
        {
            return await this.unitOfWork.patients.GetByWhere(e=>e.user.Email == email);
        }
    }
}
