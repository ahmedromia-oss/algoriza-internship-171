using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.bookers
{
    public class AddBookDto
    {
        [Required]
        public string timeId { get; set; }
        
        public string? DiscountCode { get; set; }
    }
}
