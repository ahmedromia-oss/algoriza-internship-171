using Core.Domain;
using Core.DTOs.Appointments;
using Core.DTOs.bookers;
using Core.DTOs.Doctor;
using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IDoctorService
    {
        public Task<bool> addAppointment(AddApointments addAppointmentDto , string Email);
        public Task<Doctor> addDoctor(addDoctorDto addDoctorDto , User user);
        public Task<string> updateDoctor(updateDoctorDto updateDoctorDto , string userId);
       
        public Task<GetDoctorDto> getDoctorById(Expression<Func<Doctor, bool>> where);
        public Task<ICollection<GetDoctorDto>> getAll(PaginationModel paginationModel , string searchTerm);

        public Task<ICollection<getBookers>> getBookers(string email , int day  , PaginationModel paginationModel);
        public Task<bool> confirmBook(string timeId , string doctorEmail);
        public Task<bool> UpdateTime(string email, string timeId, updateAppointment updateAppointment);
        public Task<bool> deleteTime(string email, string timeId);

        public Task<int> count();



    }
}
