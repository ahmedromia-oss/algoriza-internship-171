using Core.Domain;
using Core.DTOs.Doctor;
using Core.DTOs.Requests;
using Core.DTOs.Specialization;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ReboInterfaces
{
    public interface IDashBoardRepository
    {
        public Task<getNumberOfRequests> numberOfRequests();
        public Task<Object> Top10Doctors();
        public Task<Object> Top5Specialization();

    }
}
