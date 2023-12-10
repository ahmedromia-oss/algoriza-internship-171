using AutoMapper;
using Core.Domain;
using Core.DTOs.Discount;
using Core.DTOs.Requests;
using Core.Models;
using Core.ReboInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DiscountRebository : Repository<Discount, getDiscountDto>, IDiscountRepository
    {
        private readonly IMapper mapper;
        private readonly AppDbContext appDbContext;

        public DiscountRebository(IMapper mapper , AppDbContext appDbContext):base(appDbContext , mapper)
        {
            this.mapper = mapper;
            this.appDbContext = appDbContext;
        }
        private double calculateDiscount(int DiscountType , double price , double value)
        {
            if(Convert.ToInt32(Enums.DiscountType.percentage) == DiscountType)
            {
                return price - ((price * value) / 100);
            }
            else
            {
                return price - value;
            }
        }

        private bool checkIfDeactivated(Discount discount)
        {
            return discount.status == Convert.ToInt32(Enums.DiscountStatus.deactivated);
        }

        private async Task<bool> checkCodePatient(string discountId , string patientId)
        {
            return !(await this.appDbContext.patientDiscounts.AnyAsync(e=>e.discountId == discountId && e.PatientId == patientId));
        }
        private async Task<bool> addDiscountToPatient(Patient patient , Discount discount)
        {
            await this.appDbContext.patientDiscounts.AddAsync(new PatientDiscount { discountId = discount.Id , PatientId = patient.userId , Discount = discount , Patient = patient});
            return true;
        }
        public async Task<bool> useDiscount(string discountCode, string patientEmail , string TimeId)
        {
            Discount discount = await this.appDbContext.discounts.Where(e => e.DiscountCode == discountCode).FirstOrDefaultAsync();
            if(discount != null ) {
                Patient patient = await this.appDbContext.patients.Where(e=>e.user.Email== patientEmail).FirstOrDefaultAsync();
                PatientTime patientTime = await this.appDbContext.PatientTime.Where(e=>e.timeId == TimeId && e.patientId == patient.userId).FirstOrDefaultAsync();
                if(patientTime != null && await checkCodePatient(discount.Id , patient.userId))
                {
                    if (!(checkIfDeactivated(discount)))
                    {
                        if (checkNumberOfRequests(discount.NumOfRequests))
                        {

                            patientTime.finalPrice = calculateDiscount(discount.Type, patientTime.time.price, discount.value);
                            patientTime.discount = discount;
                            patientTime.DiscountId = discount.Id;
                            this.appDbContext.PatientTime.Update(patientTime);
                            await this.addDiscountToPatient(patient, discount);
                            this.decrementDiscount(discount);
                            return true;
                        }
                        throw new Exception("Out Of Order");
                    }
                    throw new Exception("Code is Deactivated");
                    
                }
                throw new Exception("you Used this Discount Code already");
            }
            throw new FileNotFoundException("Discount Code is Wrong");
        }

        public async Task<bool> deactivateDiscount(string discountId)
        {
            Discount discount = await this.appDbContext.discounts.FindAsync(discountId);

            if(discount == null) {

                throw new FileNotFoundException("Not Found");
            }
            discount.status = Convert.ToInt32(Enums.DiscountStatus.deactivated);
            this.appDbContext.discounts.Update(discount);
            return true;
        }

        private bool decrementDiscount(Discount discount)
        {
            
            discount.NumOfRequests -= 1;
            this.appDbContext.Update(discount);
            return true;
        }
        private bool checkNumberOfRequests(int numberOfRequests) 
        {
            return numberOfRequests > 0;
        }
    }
}
