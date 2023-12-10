using Core.Domain;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Repository.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public static class modelBuilderExtensions
    {
        public static void seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Day>().HasData(new List<Day>
            {
                new Day {Id = Convert.ToInt32(Enums.Days.saturday) },
                new Day {Id = Convert.ToInt32(Enums.Days.sunday)},
                new Day {Id =Convert.ToInt32(Enums.Days.monday)},
                new Day {Id = Convert.ToInt32(Enums.Days.tuesday)},
                new Day {Id = Convert.ToInt32(Enums.Days.wednesday)},
                new Day {Id =Convert.ToInt32(Enums.Days.thrusday)},
                new Day {Id =Convert.ToInt32(Enums.Days.friday)},

            });


            modelBuilder.Entity<Specialization>().HasData(new List<Specialization>{
                new Specialization {Id =new Guid("6029067F-6F81-411D-9903-22E4BD11A824"),Name = "Eyes"},
                new Specialization
            {
                Id =new Guid("141623C0-95B7-458D-8A05-E73692A358FA"),
                Name = "Brain"
            },
                new Specialization
            {
                Id = new Guid("A112730A-E0D5-4B22-959B-0DD25141BD4A"),
                Name = "Muscles"
            } });

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "A112730A-E0D5-4B22-959B-0DD25141BD4A",
                Name = (Enums.UserTypes.Admin).ToString(),
                NormalizedName = (Enums.UserTypes.Admin).ToString().ToUpper()


            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "A112730A-E0D5-4A23-959B-0DD25141BD4A",
                Name = (Enums.UserTypes.Doctor).ToString(),
                NormalizedName = (Enums.UserTypes.Doctor).ToString().ToUpper()

            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "A112730A-E0D5-4C24-959B-0DD25141BD4A",
                Name = (Enums.UserTypes.Patient).ToString(),
                NormalizedName = (Enums.UserTypes.Patient).ToString().ToUpper()


            });

            User user = new User
            {
                Id = "A112730A-E1B5-4C24-959B-0DD25141BD4A",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Gender = Convert.ToInt32(Enums.Gender.male),
                UserType = Convert.ToInt32(Enums.UserTypes.Admin),
                FirstName = "admin",
                LastName = "admona",
                ImageLink = ""
            };

            PasswordHasher<User> ph = new PasswordHasher<User>();
            user.PasswordHash = ph.HashPassword(user, "@Admin12345");
            modelBuilder.Entity<User>().HasData(user);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "A112730A-E0D5-4B22-959B-0DD25141BD4A",
                UserId = "A112730A-E1B5-4C24-959B-0DD25141BD4A"
            });
        }
        public static void relate(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().HasMany<Discount>(e => e.discounts).WithMany(e => e.Patients).UsingEntity<PatientDiscount>(
           l => l.HasOne<Discount>(e => e.Discount).WithMany().OnDelete(DeleteBehavior.Cascade).HasForeignKey(e => e.discountId),
           r => r.HasOne<Patient>(e => e.Patient).WithMany().OnDelete(DeleteBehavior.NoAction).HasForeignKey(e => e.PatientId));
            modelBuilder.Entity<PatientTime>().HasOne(e => e.discount).WithOne().IsRequired(false).HasForeignKey<PatientTime>(e => e.DiscountId);

            modelBuilder.Entity<Patient>().HasMany<Time>(e => e.bookings).WithMany(e => e.bookers).UsingEntity<PatientTime>(
           l => l.HasOne<Time>(e => e.time).WithMany().OnDelete(DeleteBehavior.Cascade).HasForeignKey(e => e.timeId),
           r => r.HasOne<Patient>(e => e.patient).WithMany().OnDelete(DeleteBehavior.NoAction).HasForeignKey(e => e.patientId),
           j=>j.HasKey(e=>e.Id)
           );
            modelBuilder.Entity<Specialization>().HasMany<Doctor>(e => e.Doctors).WithOne(e => e.specialization).HasForeignKey(e => e.SpecializationId);
            modelBuilder.Entity<Doctor>().HasMany<Day>(e => e.Days).WithMany(e => e.doctors);
            modelBuilder.Entity<Day>().HasMany<Time>(e => e.times).WithOne(e => e.day).HasForeignKey(e => e.DayId);
            modelBuilder.Entity<Doctor>().HasMany<Time>(e => e.Times).WithOne(e => e.Doctor).OnDelete(DeleteBehavior.Cascade).HasForeignKey(e => e.DocotorId);
            modelBuilder.Entity<Patient>().HasOne<User>(e => e.user).WithOne().HasForeignKey<Patient>(e => e.userId);
            modelBuilder.Entity<Doctor>().HasOne<User>(e => e.user).WithOne().HasForeignKey<Doctor>(e => e.userId);
            modelBuilder.Entity<Admin>().HasOne<User>(e => e.user).WithOne().HasForeignKey<Admin>(e => e.userId);
        }
        public static void KeysAndDefault(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().HasKey(e => e.userId);
            modelBuilder.Entity<User>()
               .Property(u => u.FullName)
               .HasComputedColumnSql("[FirstName] + ' ' + [LastName]");
            modelBuilder.Entity<User>().Property(u => u.UserType).HasDefaultValue(Convert.ToInt32(Enums.UserTypes.Patient));

            modelBuilder.Entity<Time>().Property(e => e.IsBooked).HasDefaultValue(false);

            modelBuilder.Entity<Doctor>().HasKey(e => e.userId);
            modelBuilder.Entity<Patient>().HasKey(e => e.userId);

            modelBuilder.Entity<Time>().Property(e => e.time).HasConversion<TimeOnlyConverter, TimeOnlyComparer>();

            modelBuilder.Entity<Discount>().HasIndex(e => e.DiscountCode).IsUnique();
            modelBuilder.Entity<Discount>().Property(e => e.status).HasDefaultValue(Convert.ToInt32(Enums.DiscountStatus.valid));
            modelBuilder.Entity<PatientTime>().Property(e => e.status).HasDefaultValue(1);

            modelBuilder.Entity<Discount>().Property(e => e.NumOfRequests).HasDefaultValue(10);
        }
    }
}
