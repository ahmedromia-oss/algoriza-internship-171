using Core.DTOs.Specialization;
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
    public class SpecializationService : ISpecializationService
    {
        private readonly IUnitOfWork unitOfWork;

        public SpecializationService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<ICollection<GetSpecializationDto>> getSpecialization(PaginationModel paginationModel)
        {
            return await this.unitOfWork.specializations.GetAll(paginationModel);
        }
    }
}
