using Azure;
using Core.Domain;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.relate();
            modelBuilder.seed();
            modelBuilder.KeysAndDefault();
            base.OnModelCreating(modelBuilder);

        }
      public DbSet<User> users { get; set; }
        public DbSet<Discount> discounts { get; set; }

        public DbSet<Patient> patients { get; set; }
        public DbSet<Doctor> doctors { get; set; }
        public DbSet<Specialization> specializations { get; set; }
        public DbSet<Day> Day { get; set; }
        public DbSet<Time> Time { get; set; }

        public DbSet<PatientTime> PatientTime { get; set; }
        public DbSet<PatientDiscount> patientDiscounts { get; set; }

       
        
    }
    
}
