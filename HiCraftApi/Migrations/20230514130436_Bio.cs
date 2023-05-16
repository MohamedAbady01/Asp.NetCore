using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiCraftApi.Migrations
{
    public partial class Bio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_ClientNameId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_CraftManModelId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ClientNameId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CraftManModelId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ClientNameId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CraftManModelId",
                table: "Reviews");

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ClientID",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CraftsmanId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ServiceRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CraftmanId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestDetails = table.Column<string>(name: "Request Details", type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRequests_Users_CraftmanId",
                        column: x => x.CraftmanId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceRequests_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ClientID",
                table: "Reviews",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CraftsmanId",
                table: "Reviews",
                column: "CraftsmanId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_CraftmanId",
                table: "ServiceRequests",
                column: "CraftmanId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_CustomerId",
                table: "ServiceRequests",
                column: "CustomerId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_ClientID",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_CraftsmanId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "ServiceRequests");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ClientID",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CraftsmanId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CraftsmanId",
                table: "Reviews");

            migrationBuilder.AlterColumn<string>(
                name: "ClientID",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ClientNameId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CraftManModelId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ClientNameId",
                table: "Reviews",
                column: "ClientNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CraftManModelId",
                table: "Reviews",
                column: "CraftManModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_ClientNameId",
                table: "Reviews",
                column: "ClientNameId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_CraftManModelId",
                table: "Reviews",
                column: "CraftManModelId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
