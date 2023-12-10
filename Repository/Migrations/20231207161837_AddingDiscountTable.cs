using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddingDiscountTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    DiscountCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    value = table.Column<double>(type: "float", nullable: false),
                    NumOfRequests = table.Column<int>(type: "int", nullable: false, defaultValue: 10)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A112730A-E1B5-4C24-959B-0DD25141BD4A",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "99347563-0f13-4ef3-af37-ee4b05cae45f", "AQAAAAIAAYagAAAAEFQG6/7ipjceY1boCMcf03RJcvohW65Yq5cMo452mTcehWrje86KxxaU/3Y4ujBwzg==", "42847be0-7c0d-44ee-a574-9d017a3eb167" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A112730A-E1B5-4C24-959B-0DD25141BD4A",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "92944cf7-bad5-401d-8ac6-6ab870e0b710", "AQAAAAIAAYagAAAAENCykY0GHc4i7zH5xDjUJqzjhG5aG5NbRb86/YkRmAuWQP4YnQUupqo0Fr0gIeufsg==", "40339cb6-20b7-49f7-a968-55d49898fcfc" });
        }
    }
}
