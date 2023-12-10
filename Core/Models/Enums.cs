using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Enums
    {
        public enum Gender
        {
            male = 1,
            female = 2,
        }
        public enum StatusCode
        {
            Success = 200,
            NotFound = 404,
            BadRequest = 400,
            created = 201,
           
            UnAuthorized = 401,
            Forbidden = 403

        }
        public enum Days
        {
            saturday = 1,
            sunday,
            monday,
            tuesday,
            wednesday,
            thrusday,
            friday
        }
        public enum BookingStatus 
        {
            pending = 1,
            completed = 2,
            cancelled = 3
        }

        public enum UserTypes
        {
            Admin = 1,
            Patient,
            Doctor
        }

        public enum DiscountType
        {
            value = 1,
            percentage = 2
        }

        public enum DiscountStatus
        {
            valid = 1,
            deactivated
        }
    }
}
