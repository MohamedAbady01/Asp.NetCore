using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiCraftApi.Migrations
{
    public partial class ForignKeyReviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_CraftManModelId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "CommentID",
                table: "Users",
                newName: "RevuewId");

            migrationBuilder.RenameColumn(
                name: "CraftManModelId",
                table: "Reviews",
                newName: "ReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_CraftManModelId",
                table: "Reviews",
                newName: "IX_Reviews_ReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_ReviewId",
                table: "Reviews",
                column: "ReviewId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_ReviewId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "RevuewId",
                table: "Users",
                newName: "CommentID");

            migrationBuilder.RenameColumn(
                name: "ReviewId",
                table: "Reviews",
                newName: "CraftManModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ReviewId",
                table: "Reviews",
                newName: "IX_Reviews_CraftManModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_CraftManModelId",
                table: "Reviews",
                column: "CraftManModelId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
