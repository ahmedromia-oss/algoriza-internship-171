using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.DTOs.Auth
{
    public class signUpDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]

        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [JsonConverter(typeof(int))]
        public int Gender { get; set; }
        
        [Required]
        [Compare("Password")]
        public string confirmPassword { get; set; }

        public IFormFile? Image { get; set; }
        
        public int? UserType { get; set; }
    }
}
