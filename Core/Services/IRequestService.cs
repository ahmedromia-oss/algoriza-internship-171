using Core.Domain;
using Core.DTOs.Doctor;
using Core.DTOs.Requests;
using Core.DTOs.Specialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IRequestService
    {
       
        public Task<getNumberOfRequests> getNumberOfRequests();
        public Task<bool> checkIfHaveRequests(string doctorId);
      
        public Task<Object> Top10Doctors();
        public Task<Object> Top5Specialization();
    }
}
