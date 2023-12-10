using Core.DTOs.Appointments;
using Core.DTOs.Discount;
using Core.DTOs.Doctor;
using Core.DTOs.patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.bookers
{
    public class getBookings
    {
        public string Id { get; set; }
        public getDoctorZipped doctor { get; set; }

        public getTimeForBookers time { get; set; }

        public getDiscountDto discount { get; set; }
        public double finalPrice { get; set; }
        public string status { get; set; }
    }
}
