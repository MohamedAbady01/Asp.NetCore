using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiCraftApi.Migrations
{
    public partial class las : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageOfPastWork_Users_CraftManId",
                table: "ImageOfPastWork");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageOfPastWork",
                table: "ImageOfPastWork");

            migrationBuilder.RenameTable(
                name: "ImageOfPastWork",
                newName: "ImageOfPastWorks");

            migrationBuilder.RenameIndex(
                name: "IX_ImageOfPastWork_CraftManId",
                table: "ImageOfPastWorks",
                newName: "IX_ImageOfPastWorks_CraftManId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageOfPastWorks",
                table: "ImageOfPastWorks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageOfPastWorks_Users_CraftManId",
                table: "ImageOfPastWorks",
                column: "CraftManId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageOfPastWorks_Users_CraftManId",
                table: "ImageOfPastWorks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageOfPastWorks",
                table: "ImageOfPastWorks");

            migrationBuilder.RenameTable(
                name: "ImageOfPastWorks",
                newName: "ImageOfPastWork");

            migrationBuilder.RenameIndex(
                name: "IX_ImageOfPastWorks_CraftManId",
                table: "ImageOfPastWork",
                newName: "IX_ImageOfPastWork_CraftManId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageOfPastWork",
                table: "ImageOfPastWork",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageOfPastWork_Users_CraftManId",
                table: "ImageOfPastWork",
                column: "CraftManId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
