using Core.Domain;
using Core.DTOs.Discount;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ReboInterfaces
{
    public interface IDiscountRepository:IRepository<Discount , getDiscountDto>
    {

        public Task<bool> useDiscount(string discountCode, string patientEmail , string timeId);

        public Task<bool> deactivateDiscount(string discountId);

        
    }
}
