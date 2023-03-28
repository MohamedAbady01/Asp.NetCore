using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiCraftApi.Migrations
{
    public partial class droprealltionbetweenthecraftmananditsspecialization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CraftMenId",
                table: "Specializations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CraftMenId",
                table: "Specializations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
