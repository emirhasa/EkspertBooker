using Microsoft.EntityFrameworkCore.Migrations;

namespace EkspertBooker.WebAPI.Migrations
{
    public partial class m4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_Email",
                table: "Korisnici",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_KorisnickoIme",
                table: "Korisnici",
                column: "KorisnickoIme",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Korisnici_Email",
                table: "Korisnici");

            migrationBuilder.DropIndex(
                name: "IX_Korisnici_KorisnickoIme",
                table: "Korisnici");
        }
    }
}
