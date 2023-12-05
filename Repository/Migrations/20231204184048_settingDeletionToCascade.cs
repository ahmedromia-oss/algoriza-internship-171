using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class settingDeletionToCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientTime_Time_timeId",
                table: "PatientTime");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientTime_Time_timeId",
                table: "PatientTime",
                column: "timeId",
                principalTable: "Time",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientTime_Time_timeId",
                table: "PatientTime");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientTime_Time_timeId",
                table: "PatientTime",
                column: "timeId",
                principalTable: "Time",
                principalColumn: "Id");
        }
    }
}
