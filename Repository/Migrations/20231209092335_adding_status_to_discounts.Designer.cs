﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository;

#nullable disable

namespace Repository.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231209092335_adding_status_to_discounts")]
    partial class addingstatustodiscounts
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Core.Domain.Admin", b =>
                {
                    b.Property<string>("userId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("userId");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("Core.Domain.Day", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Day");

                    b.HasData(
                        new
                        {
                            Id = 1
                        },
                        new
                        {
                            Id = 2
                        },
                        new
                        {
                            Id = 3
                        },
                        new
                        {
                            Id = 4
                        },
                        new
                        {
                            Id = 5
                        },
                        new
                        {
                            Id = 6
                        },
                        new
                        {
                            Id = 7
                        });
                });

            modelBuilder.Entity("Core.Domain.Discount", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DiscountCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("NumOfRequests")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(10);

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<double>("value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("DiscountCode")
                        .IsUnique();

                    b.ToTable("discounts");
                });

            modelBuilder.Entity("Core.Domain.Doctor", b =>
                {
                    b.Property<string>("userId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("SpecializationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("userId");

                    b.HasIndex("SpecializationId");

                    b.ToTable("doctors");
                });

            modelBuilder.Entity("Core.Domain.Patient", b =>
                {
                    b.Property<string>("userId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("userId");

                    b.ToTable("patients");
                });

            modelBuilder.Entity("Core.Domain.PatientDiscount", b =>
                {
                    b.Property<string>("PatientId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("discountId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PatientId", "discountId");

                    b.HasIndex("discountId");

                    b.ToTable("patientDiscounts");
                });

            modelBuilder.Entity("Core.Domain.PatientTime", b =>
                {
                    b.Property<string>("patientId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("timeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DiscountId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("finalPrice")
                        .HasColumnType("float");

                    b.Property<int>("status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("patientId", "timeId");

                    b.HasIndex("DiscountId")
                        .IsUnique()
                        .HasFilter("[DiscountId] IS NOT NULL");

                    b.HasIndex("timeId");

                    b.ToTable("PatientTime");
                });

            modelBuilder.Entity("Core.Domain.Specialization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("specializations");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6029067f-6f81-411d-9903-22e4bd11a824"),
                            Name = "Eyes"
                        },
                        new
                        {
                            Id = new Guid("141623c0-95b7-458d-8a05-e73692a358fa"),
                            Name = "Brain"
                        },
                        new
                        {
                            Id = new Guid("a112730a-e0d5-4b22-959b-0dd25141bd4a"),
                            Name = "Muscles"
                        });
                });

            modelBuilder.Entity("Core.Domain.Time", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("DayId")
                        .HasColumnType("int");

                    b.Property<string>("DocotorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsBooked")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.Property<TimeSpan>("time")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("DayId");

                    b.HasIndex("DocotorId");

                    b.ToTable("Time");
                });

            modelBuilder.Entity("DayDoctor", b =>
                {
                    b.Property<int>("DaysId")
                        .HasColumnType("int");

                    b.Property<string>("doctorsuserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("DaysId", "doctorsuserId");

                    b.HasIndex("doctorsuserId");

                    b.ToTable("DayDoctor");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "A112730A-E0D5-4B22-959B-0DD25141BD4A",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "A112730A-E0D5-4A23-959B-0DD25141BD4A",
                            Name = "Doctor",
                            NormalizedName = "DOCTOR"
                        },
                        new
                        {
                            Id = "A112730A-E0D5-4C24-959B-0DD25141BD4A",
                            Name = "Patient",
                            NormalizedName = "PATIENT"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "A112730A-E1B5-4C24-959B-0DD25141BD4A",
                            RoleId = "A112730A-E0D5-4B22-959B-0DD25141BD4A"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Core.Domain.User", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("nvarchar(max)")
                        .HasComputedColumnSql("[FirstName] + ' ' + [LastName]");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("ImageLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserType")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(2);

                    b.HasDiscriminator().HasValue("User");

                    b.HasData(
                        new
                        {
                            Id = "A112730A-E1B5-4C24-959B-0DD25141BD4A",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "f1dec570-80d7-4a87-b8ac-8a65b9eef1f0",
                            Email = "admin@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@GMAIL.COM",
                            NormalizedUserName = "ADMIN@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEHZGdmBR7NzmjeS/IllXT99bNlofX2cAoziP5gT8tuIjxEpo1qesPNmJe7abS/bvuA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "21a0eea9-79f7-46f7-98f6-5eb3cde04dbd",
                            TwoFactorEnabled = false,
                            UserName = "admin@gmail.com",
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "admin",
                            Gender = 1,
                            ImageLink = "",
                            LastName = "admona",
                            UserType = 1
                        });
                });

            modelBuilder.Entity("Core.Domain.Admin", b =>
                {
                    b.HasOne("Core.Domain.User", "user")
                        .WithOne()
                        .HasForeignKey("Core.Domain.Admin", "userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("Core.Domain.Doctor", b =>
                {
                    b.HasOne("Core.Domain.Specialization", "specialization")
                        .WithMany("Doctors")
                        .HasForeignKey("SpecializationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.User", "user")
                        .WithOne()
                        .HasForeignKey("Core.Domain.Doctor", "userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("specialization");

                    b.Navigation("user");
                });

            modelBuilder.Entity("Core.Domain.Patient", b =>
                {
                    b.HasOne("Core.Domain.User", "user")
                        .WithOne()
                        .HasForeignKey("Core.Domain.Patient", "userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("Core.Domain.PatientDiscount", b =>
                {
                    b.HasOne("Core.Domain.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Core.Domain.Discount", "Discount")
                        .WithMany()
                        .HasForeignKey("discountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Discount");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Core.Domain.PatientTime", b =>
                {
                    b.HasOne("Core.Domain.Discount", "discount")
                        .WithOne()
                        .HasForeignKey("Core.Domain.PatientTime", "DiscountId");

                    b.HasOne("Core.Domain.Patient", "patient")
                        .WithMany()
                        .HasForeignKey("patientId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Core.Domain.Time", "time")
                        .WithMany()
                        .HasForeignKey("timeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("discount");

                    b.Navigation("patient");

                    b.Navigation("time");
                });

            modelBuilder.Entity("Core.Domain.Time", b =>
                {
                    b.HasOne("Core.Domain.Day", "day")
                        .WithMany("times")
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Doctor", "Doctor")
                        .WithMany("Times")
                        .HasForeignKey("DocotorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("day");
                });

            modelBuilder.Entity("DayDoctor", b =>
                {
                    b.HasOne("Core.Domain.Day", null)
                        .WithMany()
                        .HasForeignKey("DaysId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Doctor", null)
                        .WithMany()
                        .HasForeignKey("doctorsuserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Domain.Day", b =>
                {
                    b.Navigation("times");
                });

            modelBuilder.Entity("Core.Domain.Doctor", b =>
                {
                    b.Navigation("Times");
                });

            modelBuilder.Entity("Core.Domain.Specialization", b =>
                {
                    b.Navigation("Doctors");
                });
#pragma warning restore 612, 618
        }
    }
}
