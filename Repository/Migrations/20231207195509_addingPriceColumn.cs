using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class addingPriceColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "price",
                table: "Time",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A112730A-E1B5-4C24-959B-0DD25141BD4A",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7b864740-2839-4be5-9761-d41bd5c14a97", "AQAAAAIAAYagAAAAEKDu5UOSQlsUFzNHjoXw2x49sLNKb/tSCFV0T1gTqQfi2Ad+U3c6tW2J+S1ngcboUw==", "6160dc99-f166-48ac-83ef-7fa48218c0b0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                table: "Time");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A112730A-E1B5-4C24-959B-0DD25141BD4A",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "99347563-0f13-4ef3-af37-ee4b05cae45f", "AQAAAAIAAYagAAAAEFQG6/7ipjceY1boCMcf03RJcvohW65Yq5cMo452mTcehWrje86KxxaU/3Y4ujBwzg==", "42847be0-7c0d-44ee-a574-9d017a3eb167" });
        }
    }
}
