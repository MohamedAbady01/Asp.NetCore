using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiCraftApi.Migrations
{
    public partial class AddCustomerpropartyonreviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_CustmerId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "CustmerId",
                table: "Reviews",
                newName: "custmerId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_CustmerId",
                table: "Reviews",
                newName: "IX_Reviews_custmerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_custmerId",
                table: "Reviews",
                column: "custmerId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_custmerId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "custmerId",
                table: "Reviews",
                newName: "CustmerId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_custmerId",
                table: "Reviews",
                newName: "IX_Reviews_CustmerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_CustmerId",
                table: "Reviews",
                column: "CustmerId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
