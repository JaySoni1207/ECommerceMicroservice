using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Migrations
{
    /// <inheritdoc />
    public partial class AddedMobileNumberColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MobileNumber",
                table: "Orders",
                type: "bigint",
                maxLength: 10,
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MobileNumber",
                table: "Orders",
                column: "MobileNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderReference",
                table: "Orders",
                column: "OrderReference",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_MobileNumber",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderReference",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "Orders");
        }
    }
}
