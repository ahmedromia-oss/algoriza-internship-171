using Core.Domain;
using Core.DTOs.Doctor;
using Core.DTOs.Requests;
using Core.DTOs.Specialization;
using Core.Interfaces;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class RequestService : IRequestService
    {
        private readonly IUnitOfWork unitOfWork;

        public RequestService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        

        public async Task<bool> checkIfHaveRequests(string doctorId)
        {
            return await this.unitOfWork.appointment.checkIfHaveRequests(doctorId);
        }

        public async Task<getNumberOfRequests> getNumberOfRequests()
        {
            return await this.unitOfWork.requests.numberOfRequests();
        }

        public async Task<Object> Top10Doctors()
        {
            return await this.unitOfWork.requests.Top10Doctors();
        }

        public async Task<Object> Top5Specialization()
        {
            return await this.unitOfWork.requests.Top5Specialization();
        }
    }
}
