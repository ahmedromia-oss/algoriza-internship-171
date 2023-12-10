using Core.DTOs.Appointments;
using Core.DTOs.Discount;
using Core.DTOs.patient;
using Core.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.bookers
{
    public class getBookers
    {
        public string Id { get;set; }
        public getPatientZipped patient { get; set; }

      public getTimeForBookers time { get; set; }
        public double finalPrice { get; set; }

        public string status { get; set; }
    }
}
