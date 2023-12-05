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

        public async Task<bool> book(string timeId, string Email)
        {
            await this.unitOfWork.appointment.bookAppointment(timeId, Email);
            return await this.unitOfWork.complete();
        }

        public async Task<ICollection<getBookings>> bookings(string email)
        {
           return await this.unitOfWork.appointment.getBookings(email);
        }

        public async Task<bool> cancelBooking(string email, string timeId)
        {
            await this.unitOfWork.appointment.cancelAppointment(email, timeId);
            return await this.unitOfWork.complete();
        }

        public async Task<ICollection<GetPatientDto>> getAll(PaginationModel paginationModel)
        {
            return await this.unitOfWork.patients.GetAll(paginationModel);
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
