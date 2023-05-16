using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiCraftApi.Migrations
{
    public partial class addcolumnbio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Bio",
                table: "Users",
                newName: "Bios");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Bios",
                table: "Users",
                newName: "Bio");
        }
    }
}
