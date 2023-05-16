using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiCraftApi.Migrations
{
    public partial class changesonreviewtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_ClientID",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_CraftsmanId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ClientID",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CraftsmanId",
                table: "Reviews");

            migrationBuilder.AlterColumn<string>(
                name: "CraftsmanId",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ClientID",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CraftManModelId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustmerId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CraftManModelId",
                table: "Reviews",
                column: "CraftManModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CustmerId",
                table: "Reviews",
                column: "CustmerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_CraftManModelId",
                table: "Reviews",
                column: "CraftManModelId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_CustmerId",
                table: "Reviews",
                column: "CustmerId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_CraftManModelId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_CustmerId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CraftManModelId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CustmerId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CraftManModelId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CustmerId",
                table: "Reviews");

            migrationBuilder.AlterColumn<string>(
                name: "CraftsmanId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ClientID",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ClientID",
                table: "Reviews",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CraftsmanId",
                table: "Reviews",
                column: "CraftsmanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_ClientID",
                table: "Reviews",
                column: "ClientID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_CraftsmanId",
                table: "Reviews",
                column: "CraftsmanId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
