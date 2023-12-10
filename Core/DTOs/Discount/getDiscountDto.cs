using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Discount
{
    public class getDiscountDto
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string DiscountCode { get; set; }

        public double value { get; set; }
      
        public string status { get; set; }
    }
}
