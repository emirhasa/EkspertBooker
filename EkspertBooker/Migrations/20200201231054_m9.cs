using Microsoft.EntityFrameworkCore.Migrations;

namespace EkspertBooker.WebAPI.Migrations
{
    public partial class m9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KorisnikId",
                table: "Poslodavci",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Poslodavci_KorisnikId",
                table: "Poslodavci",
                column: "KorisnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Poslodavci_Korisnici_KorisnikId",
                table: "Poslodavci",
                column: "KorisnikId",
                principalTable: "Korisnici",
                principalColumn: "KorisnikId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Poslodavci_Korisnici_KorisnikId",
                table: "Poslodavci");

            migrationBuilder.DropIndex(
                name: "IX_Poslodavci_KorisnikId",
                table: "Poslodavci");

            migrationBuilder.DropColumn(
                name: "KorisnikId",
                table: "Poslodavci");
        }
    }
}
