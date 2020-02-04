using Microsoft.EntityFrameworkCore.Migrations;

namespace EkspertBooker.WebAPI.Migrations
{
    public partial class m8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KorisnikId",
                table: "Eksperti",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Eksperti_KorisnikId",
                table: "Eksperti",
                column: "KorisnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Eksperti_Korisnici_KorisnikId",
                table: "Eksperti",
                column: "KorisnikId",
                principalTable: "Korisnici",
                principalColumn: "KorisnikId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Eksperti_Korisnici_KorisnikId",
                table: "Eksperti");

            migrationBuilder.DropIndex(
                name: "IX_Eksperti_KorisnikId",
                table: "Eksperti");

            migrationBuilder.DropColumn(
                name: "KorisnikId",
                table: "Eksperti");
        }
    }
}
