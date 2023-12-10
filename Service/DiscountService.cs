using Core.DTOs.Discount;
using Core.Interfaces;
using Core.Models;
using Core.ReboInterfaces;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class DiscountService : IDiscountService
    {
        private readonly IUnitOfWork unitOfWork;

        public DiscountService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<bool> addDiscount(addDiscountDto addDiscountDto)
        {
            await this.unitOfWork.discount.Add(addDiscountDto);
            return await this.unitOfWork.complete();
          
        }

        public async Task<bool> deActivateDiscount(string discountId)
        {
            await this.unitOfWork.discount.deactivateDiscount(discountId);
            return await this.unitOfWork.complete();
        }

        public async Task<bool> deleteDiscount(string discountId)
        {
            await this.unitOfWork.discount.deleteById(discountId);
            return await this.unitOfWork.complete();
        }

        public async Task<ICollection<getDiscountDto>> getDiscounts(PaginationModel paginationModel)
        {
            return await this.unitOfWork.discount.GetAll(paginationModel);
        }

        public async Task<bool> updateDiscount(updateDiscountDto updateDiscount, string discountId)
        {
            await this.unitOfWork.discount.update(updateDiscount,  discountId);
            return await this.unitOfWork.complete();
        }

       

        public async Task<bool> useDiscount(string discountCode, string patientEmail, string TimeId)
        {
            await this.unitOfWork.discount.useDiscount(discountCode, patientEmail, TimeId);
            return await this.unitOfWork.complete();
        }
    }
}
