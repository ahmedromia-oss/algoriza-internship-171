﻿using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Doctor
{
    public class AddTimeDto
    {
        [Required]
        public string time { get; set; }
    }
}
