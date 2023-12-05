using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddingRolesAndAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                defaultValue: 2);

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.userId);
                    table.ForeignKey(
                        name: "FK_Admin_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "A112730A-E0D5-4A23-959B-0DD25141BD4A", null, "Doctor", null },
                    { "A112730A-E0D5-4B22-959B-0DD25141BD4A", null, "Admin", null },
                    { "A112730A-E0D5-4C24-959B-0DD25141BD4A", null, "Patient", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Discriminator", "Email", "EmailConfirmed", "FirstName", "Gender", "ImageLink", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "A112730A-E1B5-4C24-959B-0DD25141BD4A", 0, "c17bea07-b2d2-4fad-8480-3f9a1dd9a0a4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User", "admin@gmail.com", false, "admin", 1, "", "admona", false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEHDK7yWCBNJ77P5c/r7E5Mu5kaLm55U0kETnSMiBZdJOmBv/v4TokftUd00PjwrV+g==", null, false, "482a5440-f773-485e-aae7-1bd154db5d42", false, "admin@gmail.com", 1 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "A112730A-E0D5-4B22-959B-0DD25141BD4A", "A112730A-E1B5-4C24-959B-0DD25141BD4A" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "A112730A-E0D5-4A23-959B-0DD25141BD4A");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "A112730A-E0D5-4C24-959B-0DD25141BD4A");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "A112730A-E0D5-4B22-959B-0DD25141BD4A", "A112730A-E1B5-4C24-959B-0DD25141BD4A" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "A112730A-E0D5-4B22-959B-0DD25141BD4A");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A112730A-E1B5-4C24-959B-0DD25141BD4A");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "AspNetUsers");
        }
    }
}
