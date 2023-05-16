using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiCraftApi.Migrations
{
    public partial class updatereview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CraftsmanId",
                table: "Reviews",
                newName: "CraftmanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CraftmanId",
                table: "Reviews",
                newName: "CraftsmanId");
        }
    }
}
