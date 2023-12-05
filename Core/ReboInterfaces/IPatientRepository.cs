using Core.Domain;
using Core.DTOs.patient;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ReboInterfaces
{
    public interface IPatientRepository:IRepository<Patient , GetPatientDto>
    {
        public Task<bool> addPatient(addPatientDto addPatientDto , User user);
    }
}
