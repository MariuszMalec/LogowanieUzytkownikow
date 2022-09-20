using Microsoft.EntityFrameworkCore.Migrations;

namespace LoginUser.WebApi.Migrations
{
    public partial class ClientUserIdAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Clients",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CreatedById",
                table: "Clients",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Users_CreatedById",
                table: "Clients",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Users_CreatedById",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_CreatedById",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Clients");
        }
    }
}
