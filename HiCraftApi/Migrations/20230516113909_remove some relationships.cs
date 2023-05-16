using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiCraftApi.Migrations
{
    public partial class removesomerelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_Users_CraftmanId",
                table: "ServiceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_Users_CustomerId",
                table: "ServiceRequests");

            migrationBuilder.DropIndex(
                name: "IX_ServiceRequests_CraftmanId",
                table: "ServiceRequests");

            migrationBuilder.DropIndex(
                name: "IX_ServiceRequests_CustomerId",
                table: "ServiceRequests");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "ServiceRequests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "CraftmanId",
                table: "ServiceRequests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CraftManModelId",
                table: "ServiceRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustmerId",
                table: "ServiceRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_CraftManModelId",
                table: "ServiceRequests",
                column: "CraftManModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_CustmerId",
                table: "ServiceRequests",
                column: "CustmerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequests_Users_CraftManModelId",
                table: "ServiceRequests",
                column: "CraftManModelId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequests_Users_CustmerId",
                table: "ServiceRequests",
                column: "CustmerId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_Users_CraftManModelId",
                table: "ServiceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_Users_CustmerId",
                table: "ServiceRequests");

            migrationBuilder.DropIndex(
                name: "IX_ServiceRequests_CraftManModelId",
                table: "ServiceRequests");

            migrationBuilder.DropIndex(
                name: "IX_ServiceRequests_CustmerId",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "CraftManModelId",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "CustmerId",
                table: "ServiceRequests");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "ServiceRequests",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CraftmanId",
                table: "ServiceRequests",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_CraftmanId",
                table: "ServiceRequests",
                column: "CraftmanId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_CustomerId",
                table: "ServiceRequests",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequests_Users_CraftmanId",
                table: "ServiceRequests",
                column: "CraftmanId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequests_Users_CustomerId",
                table: "ServiceRequests",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
