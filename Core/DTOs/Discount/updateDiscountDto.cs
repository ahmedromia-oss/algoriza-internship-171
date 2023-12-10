using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.DTOs.Discount
{
    public class updateDiscountDto
    {

        [JsonConverter(typeof(int))]
        [Required]
        public Enums.DiscountType Type { get; set; }
        [Required]

        public string DiscountCode { get; set; }
        [Required]

        public double value { get; set; }
        public int NumOfRequests { get; set; }
    }
}
