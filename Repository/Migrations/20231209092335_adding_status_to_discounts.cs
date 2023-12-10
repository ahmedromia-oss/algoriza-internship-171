using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class addingstatustodiscounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DiscountCode",
                table: "discounts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "discounts",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A112730A-E1B5-4C24-959B-0DD25141BD4A",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1dec570-80d7-4a87-b8ac-8a65b9eef1f0", "AQAAAAIAAYagAAAAEHZGdmBR7NzmjeS/IllXT99bNlofX2cAoziP5gT8tuIjxEpo1qesPNmJe7abS/bvuA==", "21a0eea9-79f7-46f7-98f6-5eb3cde04dbd" });

            migrationBuilder.CreateIndex(
                name: "IX_discounts_DiscountCode",
                table: "discounts",
                column: "DiscountCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_discounts_DiscountCode",
                table: "discounts");

            migrationBuilder.DropColumn(
                name: "status",
                table: "discounts");

            migrationBuilder.AlterColumn<string>(
                name: "DiscountCode",
                table: "discounts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A112730A-E1B5-4C24-959B-0DD25141BD4A",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c5f75698-b15e-4c82-a5c1-dc8da883860e", "AQAAAAIAAYagAAAAEDPIm+RdIw7F2nLKOtZRzc0CKjajJ7iBPbS+DmNnhX/HrXr3EkKTLIVFZ3T37Rhn3A==", "4aebc175-d342-4abe-aa39-13f7c588b4a5" });
        }
    }
}
