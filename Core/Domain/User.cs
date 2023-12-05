using Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int Gender { get; set; }

        public int UserType { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string ImageLink { get; set; }


        
    }
}
