using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EkspertBooker.WebAPI.Migrations
{
    public partial class m1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategorije",
                columns: table => new
                {
                    KategorijaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorije", x => x.KategorijaId);
                });

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    KorisnikId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Telefon = table.Column<string>(nullable: true),
                    KorisnickoIme = table.Column<string>(nullable: true),
                    LozinkaHash = table.Column<string>(nullable: true),
                    LozinkaSalt = table.Column<string>(nullable: true),
                    Slika = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.KorisnikId);
                });

            migrationBuilder.CreateTable(
                name: "Stanja",
                columns: table => new
                {
                    StanjeId = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stanja", x => x.StanjeId);
                });

            migrationBuilder.CreateTable(
                name: "Uloge",
                columns: table => new
                {
                    UlogaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uloge", x => x.UlogaId);
                });

            migrationBuilder.CreateTable(
                name: "KorisniciUloge",
                columns: table => new
                {
                    KorisnikUlogaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KorisnikId = table.Column<int>(nullable: false),
                    UlogaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisniciUloge", x => x.KorisnikUlogaId);
                    table.ForeignKey(
                        name: "FK_Korisnici_KorisniciUloge",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Uloge_KorisniciUloge",
                        column: x => x.UlogaId,
                        principalTable: "Uloge",
                        principalColumn: "UlogaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Eksperti",
                columns: table => new
                {
                    EkspertId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KorisnikUlogaId = table.Column<int>(nullable: false),
                    ProsjecnaOcjena = table.Column<float>(nullable: false),
                    BrojZavrsenihProjekata = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eksperti", x => x.EkspertId);
                    table.ForeignKey(
                        name: "FK_Eksperti_KorisniciUloge_KorisnikUlogaId",
                        column: x => x.KorisnikUlogaId,
                        principalTable: "KorisniciUloge",
                        principalColumn: "KorisnikUlogaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Poslodavci",
                columns: table => new
                {
                    PoslodavacId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KorisnikUlogaId = table.Column<int>(nullable: false),
                    ProsjecnaOcjena = table.Column<float>(nullable: false),
                    BrojZavrsenihProjekata = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poslodavci", x => x.PoslodavacId);
                    table.ForeignKey(
                        name: "FK_Poslodavci_KorisniciUloge_KorisnikUlogaId",
                        column: x => x.KorisnikUlogaId,
                        principalTable: "KorisniciUloge",
                        principalColumn: "KorisnikUlogaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projekti",
                columns: table => new
                {
                    ProjektId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PoslodavacId = table.Column<int>(nullable: false),
                    EkspertId = table.Column<int>(nullable: true),
                    Naziv = table.Column<string>(maxLength: 100, nullable: false),
                    KratkiOpis = table.Column<string>(maxLength: 100, nullable: false),
                    DetaljniOpis = table.Column<string>(nullable: true),
                    DatumObjave = table.Column<DateTime>(nullable: true),
                    DatumPocetka = table.Column<DateTime>(nullable: true),
                    DatumZavrsetka = table.Column<DateTime>(nullable: true),
                    TrajanjeDana = table.Column<int>(nullable: true),
                    StanjeId = table.Column<string>(maxLength: 50, nullable: false),
                    KategorijaId = table.Column<int>(nullable: false),
                    Budzet = table.Column<int>(nullable: true),
                    Hitan = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projekti", x => x.ProjektId);
                    table.ForeignKey(
                        name: "FK_Projekti_Eksperti_EkspertId",
                        column: x => x.EkspertId,
                        principalTable: "Eksperti",
                        principalColumn: "EkspertId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projekti_Kategorije",
                        column: x => x.KategorijaId,
                        principalTable: "Kategorije",
                        principalColumn: "KategorijaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projekti_Poslodavci_PoslodavacId",
                        column: x => x.PoslodavacId,
                        principalTable: "Poslodavci",
                        principalColumn: "PoslodavacId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projekti_Stanja",
                        column: x => x.StanjeId,
                        principalTable: "Stanja",
                        principalColumn: "StanjeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjektDetalji",
                columns: table => new
                {
                    ProjektDetaljiId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProjektId = table.Column<int>(nullable: false),
                    AktivanDetaljanOpis = table.Column<string>(nullable: true),
                    Napomena = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjektDetalji", x => x.ProjektDetaljiId);
                    table.ForeignKey(
                        name: "FK_ProjektDetalji_Projekti_ProjektId",
                        column: x => x.ProjektId,
                        principalTable: "Projekti",
                        principalColumn: "ProjektId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Eksperti_KorisnikUlogaId",
                table: "Eksperti",
                column: "KorisnikUlogaId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisniciUloge_KorisnikId",
                table: "KorisniciUloge",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisniciUloge_UlogaId",
                table: "KorisniciUloge",
                column: "UlogaId");

            migrationBuilder.CreateIndex(
                name: "IX_Poslodavci_KorisnikUlogaId",
                table: "Poslodavci",
                column: "KorisnikUlogaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjektDetalji_ProjektId",
                table: "ProjektDetalji",
                column: "ProjektId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projekti_EkspertId",
                table: "Projekti",
                column: "EkspertId");

            migrationBuilder.CreateIndex(
                name: "IX_Projekti_KategorijaId",
                table: "Projekti",
                column: "KategorijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Projekti_PoslodavacId",
                table: "Projekti",
                column: "PoslodavacId");

            migrationBuilder.CreateIndex(
                name: "IX_Projekti_StanjeId",
                table: "Projekti",
                column: "StanjeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjektDetalji");

            migrationBuilder.DropTable(
                name: "Projekti");

            migrationBuilder.DropTable(
                name: "Eksperti");

            migrationBuilder.DropTable(
                name: "Kategorije");

            migrationBuilder.DropTable(
                name: "Poslodavci");

            migrationBuilder.DropTable(
                name: "Stanja");

            migrationBuilder.DropTable(
                name: "KorisniciUloge");

            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "Uloge");
        }
    }
}
