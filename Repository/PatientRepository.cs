using AutoMapper;
using Core.Domain;
using Core.DTOs.Auth;
using Core.DTOs.Doctor;
using Core.DTOs.patient;
using Core.Models;
using Core.ReboInterfaces;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PatientRepository : Repository<Patient, GetPatientDto>, IPatientRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
      
        public PatientRepository(AppDbContext context , IMapper mapper):base(context , mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }
        public async Task<bool> addPatient(addPatientDto addPatientDto , User user)
        {
                Patient patient = mapper.Map<Patient>(addPatientDto);
                patient.user = user;
                await this.context.patients.AddAsync(patient);
                return true;  
        }
    }
}
