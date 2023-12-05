using Core.DTOs.Appointments;
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
        public getDoctorZipped doctor { get; set; }

        public getTimeForBookers time { get; set; }

        public string status { get; set; }
    }
}
