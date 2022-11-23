using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiLibros2.Migrations
{
    public partial class UsuarioValidacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Editorial",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Editorial_UsuarioId",
                table: "Editorial",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Editorial_AspNetUsers_UsuarioId",
                table: "Editorial",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Editorial_AspNetUsers_UsuarioId",
                table: "Editorial");

            migrationBuilder.DropIndex(
                name: "IX_Editorial_UsuarioId",
                table: "Editorial");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Editorial");
        }
    }
}
