using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiCraftApi.Migrations
{
    public partial class AddListofreviewsonreviewstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CraftManModelId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CraftManModelId",
                table: "Reviews",
                column: "CraftManModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_CraftManModelId",
                table: "Reviews",
                column: "CraftManModelId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_CraftManModelId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CraftManModelId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CraftManModelId",
                table: "Reviews");
        }
    }
}
