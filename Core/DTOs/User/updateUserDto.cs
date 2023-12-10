using Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.DTOs.User
{
    public class updateUserDto
    {
        public string? Email { get; set; }


        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        
        public DateTime? DateOfBirth { get; set; }

        [JsonConverter(typeof(int))]
        public Enums.Gender? Gender { get; set; }

        public IFormFile? Image { get; set; }
    }
}
