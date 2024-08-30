using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.LeaveManagmant.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addedrefreshtokentable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: new Guid("20596ef3-2e25-4322-855c-63337fa54868"));

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JwtId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsed = table.Column<bool>(type: "bit", nullable: false),
                    IdRevoked = table.Column<bool>(type: "bit", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "LeaveTypes",
                columns: new[] { "Id", "CreatedDate", "DefaultDays", "ModifiedDate", "Name" },
                values: new object[] { new Guid("d521425d-ec13-4c2c-aca2-cac12b2bc435"), new DateTime(2024, 8, 20, 13, 13, 8, 219, DateTimeKind.Local).AddTicks(9075), 10, new DateTime(2024, 8, 20, 13, 13, 8, 219, DateTimeKind.Local).AddTicks(9093), "Vacation" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: new Guid("d521425d-ec13-4c2c-aca2-cac12b2bc435"));

            migrationBuilder.InsertData(
                table: "LeaveTypes",
                columns: new[] { "Id", "CreatedDate", "DefaultDays", "ModifiedDate", "Name" },
                values: new object[] { new Guid("20596ef3-2e25-4322-855c-63337fa54868"), new DateTime(2024, 8, 18, 11, 23, 35, 170, DateTimeKind.Local).AddTicks(5437), 10, new DateTime(2024, 8, 18, 11, 23, 35, 170, DateTimeKind.Local).AddTicks(5455), "Vacation" });
        }
    }
}
