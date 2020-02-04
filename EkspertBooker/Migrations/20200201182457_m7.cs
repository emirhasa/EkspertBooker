using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EkspertBooker.WebAPI.Migrations
{
    public partial class m7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slika",
                table: "Korisnici");

            migrationBuilder.CreateTable(
                name: "KorisniciSlike",
                columns: table => new
                {
                    KorisnikSlikaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KorisnikId = table.Column<int>(nullable: false),
                    ProfilnaSlika = table.Column<byte[]>(nullable: true),
                    SlikaNaziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisniciSlike", x => x.KorisnikSlikaId);
                    table.ForeignKey(
                        name: "FK_KorisniciSlike_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KorisniciSlike_KorisnikId",
                table: "KorisniciSlike",
                column: "KorisnikId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KorisniciSlike");

            migrationBuilder.AddColumn<byte[]>(
                name: "Slika",
                table: "Korisnici",
                nullable: true);
        }
    }
}
