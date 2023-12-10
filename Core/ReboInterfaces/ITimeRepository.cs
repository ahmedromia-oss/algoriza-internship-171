using Core.Domain;
using Core.DTOs.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ReboInterfaces
{
    public interface ITimeRepository
    {
        public Task<bool> addAppointment(AddApointments addApointments, string Email);
        public Task<bool> updateTime(string email, string timeId, updateAppointment updateAppointment);
        public Task<bool> deleteTime(string email, string timeId);

        
    }
}
