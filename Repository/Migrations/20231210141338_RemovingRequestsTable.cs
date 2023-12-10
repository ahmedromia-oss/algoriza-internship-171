using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class RemovingRequestsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientTime",
                table: "PatientTime");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "PatientTime",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientTime",
                table: "PatientTime",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A112730A-E1B5-4C24-959B-0DD25141BD4A",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3c92509a-7940-409a-80b1-64ac2e61ab8d", "AQAAAAIAAYagAAAAEOvI53ePifgYGc7u8nL7F7WBNsBfxKUrXlmIiMSl0LhGw3wAzZPl23TErKRnolpRiw==", "9a51bcab-2c8b-4dda-8b7c-c73ca6a54d35" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientTime_patientId",
                table: "PatientTime",
                column: "patientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientTime",
                table: "PatientTime");

            migrationBuilder.DropIndex(
                name: "IX_PatientTime_patientId",
                table: "PatientTime");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PatientTime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientTime",
                table: "PatientTime",
                columns: new[] { "patientId", "timeId" });

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
    }
}
