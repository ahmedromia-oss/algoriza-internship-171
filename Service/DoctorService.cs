using Core.Domain;
using Core.DTOs.Appointments;
using Core.DTOs.Auth;
using Core.DTOs.bookers;
using Core.DTOs.Doctor;
using Core.Interfaces;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class DoctorService : IDoctorService
    {
        
        private readonly IUnitOfWork unitOfWork;
        

        public DoctorService(IUnitOfWork unitOfWork)
        {
           
            this.unitOfWork = unitOfWork;
        }


        public async Task<Doctor> addDoctor(addDoctorDto addDoctorDto , User user)
        {
            
            
                var doctor = await this.unitOfWork.doctors.addDoctor(addDoctorDto, user);

                await this.unitOfWork.complete();
            
            
            
            return doctor;
        }

       

        public async Task<ICollection<GetDoctorDto>> getAll(PaginationModel paginationModel, string searchTerm)
        {
            return await this.unitOfWork.doctors.getAllDoctors(paginationModel , searchTerm);
        }

        public async Task<GetDoctorDto> getDoctorById(Expression<Func<Doctor , bool>> where)
        {
            return await this.unitOfWork.doctors.GetByWhere(where);
        }

        public async Task<string> updateDoctor(updateDoctorDto updateDoctorDto, string  Email)
        {
            
            await this.unitOfWork.doctors.updateDoctor(updateDoctorDto , Email);
            var save = await this.unitOfWork.complete();
            if (save) {

                return "Doctor Updated";
                    } ;
            throw new Exception("Something went Wrong");
        }
       
        public async Task<bool> addAppointment(AddApointments addAppointmentDto, string Email)
        {
             
                await this.unitOfWork.time.addAppointment(addAppointmentDto , Email);
                return await this.unitOfWork.complete();
        }

        public async Task<ICollection<getBookers>> getBookers(string email, int day , PaginationModel paginationModel)
        {
           return await this.unitOfWork.appointment.getBookers(email , day , paginationModel );
        }

        public async Task<bool> confirmBook( string timeId, string doctorEmail)
        {
            await this.unitOfWork.appointment.confirmCheckUp(timeId, doctorEmail);
            return await this.unitOfWork.complete();
        }

        public async Task<bool> UpdateTime(string email, string timeId, updateAppointment updateAppointment)
        {
           await this.unitOfWork.time.updateTime(email, timeId, updateAppointment);
            return await this.unitOfWork.complete();
        }

        public async Task<bool> deleteTime(string email, string timeId)
        {
            await this.unitOfWork.time.deleteTime(email, timeId);
            return await this.unitOfWork.complete();
        }

        public async Task<int> count()
        {
            return await this.unitOfWork.doctors.count();
        }
    }
}
