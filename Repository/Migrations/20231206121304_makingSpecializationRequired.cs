using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class makingSpecializationRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "A112730A-E0D5-4A23-959B-0DD25141BD4A",
                column: "NormalizedName",
                value: "DOCTOR");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "A112730A-E0D5-4B22-959B-0DD25141BD4A",
                column: "NormalizedName",
                value: "ADMIN");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "A112730A-E0D5-4C24-959B-0DD25141BD4A",
                column: "NormalizedName",
                value: "PATIENT");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A112730A-E1B5-4C24-959B-0DD25141BD4A",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "92944cf7-bad5-401d-8ac6-6ab870e0b710", "AQAAAAIAAYagAAAAENCykY0GHc4i7zH5xDjUJqzjhG5aG5NbRb86/YkRmAuWQP4YnQUupqo0Fr0gIeufsg==", "40339cb6-20b7-49f7-a968-55d49898fcfc" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "A112730A-E0D5-4A23-959B-0DD25141BD4A",
                column: "NormalizedName",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "A112730A-E0D5-4B22-959B-0DD25141BD4A",
                column: "NormalizedName",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "A112730A-E0D5-4C24-959B-0DD25141BD4A",
                column: "NormalizedName",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A112730A-E1B5-4C24-959B-0DD25141BD4A",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c17bea07-b2d2-4fad-8480-3f9a1dd9a0a4", "AQAAAAIAAYagAAAAEHDK7yWCBNJ77P5c/r7E5Mu5kaLm55U0kETnSMiBZdJOmBv/v4TokftUd00PjwrV+g==", "482a5440-f773-485e-aae7-1bd154db5d42" });
        }
    }
}
