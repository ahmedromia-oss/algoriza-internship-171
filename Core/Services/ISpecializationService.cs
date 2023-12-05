using Core.Domain;
using Core.DTOs.Specialization;
using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ISpecializationService
    {
        public Task<ICollection<GetSpecializationDto>> getSpecialization(PaginationModel paginationModel);
    }
}
