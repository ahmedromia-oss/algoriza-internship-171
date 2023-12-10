using Core.DTOs.Discount;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IDiscountService
    {
        public Task<bool> addDiscount(addDiscountDto addDiscountDto);
        public Task<bool> useDiscount(string discountCode, string patientEmail, string TimeId);
        public Task<ICollection<getDiscountDto>> getDiscounts(PaginationModel paginationModel);
        public Task<bool> updateDiscount(updateDiscountDto updateDiscountDto,string discountId);

        public Task<bool> deleteDiscount(string discountId);
        public Task<bool> deActivateDiscount(string discountId);

       
    }
}
