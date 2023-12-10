using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddingRequestsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "requests",
                columns: table => new
                {
                    DoctorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestsCancelled = table.Column<int>(type: "int", nullable: false),
                    RequestsCompleted = table.Column<int>(type: "int", nullable: false),
                    RequestsPending = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requests", x => x.DoctorId);
                    table.ForeignKey(
                        name: "FK_requests_doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "doctors",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A112730A-E1B5-4C24-959B-0DD25141BD4A",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "09c13eaa-11eb-47d2-bd17-993d6907033b", "AQAAAAIAAYagAAAAENrhtAeFBBARZmG1MEKlbidbwfc6rS0ZoHIwSIdREMtMZOUyx+VsBntmu8NUNOxI9w==", "6e113ac6-683a-4fa8-990b-5ea6258c3d45" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "requests");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A112730A-E1B5-4C24-959B-0DD25141BD4A",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1dec570-80d7-4a87-b8ac-8a65b9eef1f0", "AQAAAAIAAYagAAAAEHZGdmBR7NzmjeS/IllXT99bNlofX2cAoziP5gT8tuIjxEpo1qesPNmJe7abS/bvuA==", "21a0eea9-79f7-46f7-98f6-5eb3cde04dbd" });
        }
    }
}
