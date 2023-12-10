using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientDiscounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Discount",
                table: "Discount");

            migrationBuilder.RenameTable(
                name: "Discount",
                newName: "discounts");

            migrationBuilder.AddColumn<string>(
                name: "DiscountId",
                table: "PatientTime",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "finalPrice",
                table: "PatientTime",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_discounts",
                table: "discounts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "patientDiscounts",
                columns: table => new
                {
                    discountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PatientId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patientDiscounts", x => new { x.PatientId, x.discountId });
                    table.ForeignKey(
                        name: "FK_patientDiscounts_discounts_discountId",
                        column: x => x.discountId,
                        principalTable: "discounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_patientDiscounts_patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "patients",
                        principalColumn: "userId");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A112730A-E1B5-4C24-959B-0DD25141BD4A",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c5f75698-b15e-4c82-a5c1-dc8da883860e", "AQAAAAIAAYagAAAAEDPIm+RdIw7F2nLKOtZRzc0CKjajJ7iBPbS+DmNnhX/HrXr3EkKTLIVFZ3T37Rhn3A==", "4aebc175-d342-4abe-aa39-13f7c588b4a5" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientTime_DiscountId",
                table: "PatientTime",
                column: "DiscountId",
                unique: true,
                filter: "[DiscountId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_patientDiscounts_discountId",
                table: "patientDiscounts",
                column: "discountId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientTime_discounts_DiscountId",
                table: "PatientTime",
                column: "DiscountId",
                principalTable: "discounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientTime_discounts_DiscountId",
                table: "PatientTime");

            migrationBuilder.DropTable(
                name: "patientDiscounts");

            migrationBuilder.DropIndex(
                name: "IX_PatientTime_DiscountId",
                table: "PatientTime");

            migrationBuilder.DropPrimaryKey(
                name: "PK_discounts",
                table: "discounts");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "PatientTime");

            migrationBuilder.DropColumn(
                name: "finalPrice",
                table: "PatientTime");

            migrationBuilder.RenameTable(
                name: "discounts",
                newName: "Discount");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discount",
                table: "Discount",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A112730A-E1B5-4C24-959B-0DD25141BD4A",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7b864740-2839-4be5-9761-d41bd5c14a97", "AQAAAAIAAYagAAAAEKDu5UOSQlsUFzNHjoXw2x49sLNKb/tSCFV0T1gTqQfi2Ad+U3c6tW2J+S1ngcboUw==", "6160dc99-f166-48ac-83ef-7fa48218c0b0" });
        }
    }
}
