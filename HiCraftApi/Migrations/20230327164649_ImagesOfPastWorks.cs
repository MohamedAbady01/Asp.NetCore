using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiCraftApi.Migrations
{
    public partial class ImagesOfPastWorks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImagesOfPastWorksID",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ImageOfPastWork",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Images = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CraftManId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageOfPastWork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageOfPastWork_Users_CraftManId",
                        column: x => x.CraftManId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageOfPastWork_CraftManId",
                table: "ImageOfPastWork",
                column: "CraftManId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageOfPastWork");

            migrationBuilder.DropColumn(
                name: "ImagesOfPastWorksID",
                table: "Users");
        }
    }
}
