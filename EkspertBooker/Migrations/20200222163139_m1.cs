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
                    Naziv = table.Column<string>(nullable: true)
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
                    Ime = table.Column<string>(maxLength: 50, nullable: false),
                    Prezime = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Telefon = table.Column<string>(maxLength: 50, nullable: true),
                    KorisnickoIme = table.Column<string>(maxLength: 50, nullable: false),
                    LozinkaHash = table.Column<string>(nullable: false),
                    LozinkaSalt = table.Column<string>(nullable: false),
                    DatumRegistracije = table.Column<DateTime>(nullable: true)
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
                name: "KorisniciKategorije",
                columns: table => new
                {
                    KorisnikKategorijaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KorisnikId = table.Column<int>(nullable: false),
                    KategorijaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisniciKategorije", x => x.KorisnikKategorijaId);
                    table.ForeignKey(
                        name: "FK_KorisniciKategorije_Kategorije_KategorijaId",
                        column: x => x.KategorijaId,
                        principalTable: "Kategorije",
                        principalColumn: "KategorijaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KorisniciKategorije_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    KorisnikId = table.Column<int>(nullable: false),
                    KorisnikUlogaId = table.Column<int>(nullable: true),
                    ProsjecnaOcjena = table.Column<float>(nullable: true),
                    BrojZavrsenihProjekata = table.Column<int>(nullable: false, defaultValue: 0),
                    BrojRecenzija = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eksperti", x => x.KorisnikId);
                    table.ForeignKey(
                        name: "FK_Eksperti_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Eksperti_KorisniciUloge_KorisnikUlogaId",
                        column: x => x.KorisnikUlogaId,
                        principalTable: "KorisniciUloge",
                        principalColumn: "KorisnikUlogaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Poslodavci",
                columns: table => new
                {
                    KorisnikId = table.Column<int>(nullable: false),
                    PoslodavacId = table.Column<int>(nullable: false),
                    KorisnikUlogaId = table.Column<int>(nullable: true),
                    ProsjecnaOcjena = table.Column<float>(nullable: true),
                    BrojZavrsenihProjekata = table.Column<int>(nullable: false, defaultValue: 0),
                    BrojRecenzija = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poslodavci", x => x.KorisnikId);
                    table.ForeignKey(
                        name: "FK_Poslodavci_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Poslodavci_KorisniciUloge_KorisnikUlogaId",
                        column: x => x.KorisnikUlogaId,
                        principalTable: "KorisniciUloge",
                        principalColumn: "KorisnikUlogaId",
                        onDelete: ReferentialAction.Restrict);
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
                        principalColumn: "KorisnikId",
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
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projekti_Stanja",
                        column: x => x.StanjeId,
                        principalTable: "Stanja",
                        principalColumn: "StanjeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ponude",
                columns: table => new
                {
                    PonudaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EkspertId = table.Column<int>(nullable: false),
                    ProjektId = table.Column<int>(nullable: false),
                    OpisPonude = table.Column<string>(nullable: true),
                    Cijena = table.Column<int>(nullable: true),
                    VrijemePonude = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ponude", x => x.PonudaId);
                    table.ForeignKey(
                        name: "FK_Ponude_Eksperti_EkspertId",
                        column: x => x.EkspertId,
                        principalTable: "Eksperti",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ponude_Projekti_ProjektId",
                        column: x => x.ProjektId,
                        principalTable: "Projekti",
                        principalColumn: "ProjektId",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "RecenzijeOEksperti",
                columns: table => new
                {
                    RecenzijaOEkspertId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProjektId = table.Column<int>(nullable: false),
                    EkspertId = table.Column<int>(nullable: false),
                    PoslodavacId = table.Column<int>(nullable: false),
                    Ocjena = table.Column<int>(nullable: false),
                    Komentar = table.Column<string>(nullable: true),
                    DatumRecenzije = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecenzijeOEksperti", x => x.RecenzijaOEkspertId);
                    table.ForeignKey(
                        name: "FK_RecenzijeOEksperti_Eksperti_EkspertId",
                        column: x => x.EkspertId,
                        principalTable: "Eksperti",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecenzijeOEksperti_Poslodavci_PoslodavacId",
                        column: x => x.PoslodavacId,
                        principalTable: "Poslodavci",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecenzijeOEksperti_Projekti_ProjektId",
                        column: x => x.ProjektId,
                        principalTable: "Projekti",
                        principalColumn: "ProjektId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecenzijeOPoslodavci",
                columns: table => new
                {
                    RecenzijaOPoslodavacId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProjektId = table.Column<int>(nullable: false),
                    PoslodavacId = table.Column<int>(nullable: false),
                    EkspertId = table.Column<int>(nullable: false),
                    Ocjena = table.Column<int>(nullable: false),
                    Komentar = table.Column<string>(nullable: true),
                    DatumRecenzije = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecenzijeOPoslodavci", x => x.RecenzijaOPoslodavacId);
                    table.ForeignKey(
                        name: "FK_RecenzijeOPoslodavci_Eksperti_EkspertId",
                        column: x => x.EkspertId,
                        principalTable: "Eksperti",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecenzijeOPoslodavci_Poslodavci_PoslodavacId",
                        column: x => x.PoslodavacId,
                        principalTable: "Poslodavci",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecenzijeOPoslodavci_Projekti_ProjektId",
                        column: x => x.ProjektId,
                        principalTable: "Projekti",
                        principalColumn: "ProjektId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjektDetaljiPrilozi",
                columns: table => new
                {
                    ProjektDetaljiPrilogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProjektDetaljiId = table.Column<int>(nullable: false),
                    PrilogNaziv = table.Column<string>(nullable: true),
                    Prilog = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjektDetaljiPrilozi", x => x.ProjektDetaljiPrilogId);
                    table.ForeignKey(
                        name: "FK_ProjektDetaljiPrilozi_ProjektDetalji_ProjektDetaljiId",
                        column: x => x.ProjektDetaljiId,
                        principalTable: "ProjektDetalji",
                        principalColumn: "ProjektDetaljiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Eksperti_KorisnikUlogaId",
                table: "Eksperti",
                column: "KorisnikUlogaId",
                unique: true,
                filter: "[KorisnikUlogaId] IS NOT NULL");

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

            migrationBuilder.CreateIndex(
                name: "IX_KorisniciKategorije_KategorijaId",
                table: "KorisniciKategorije",
                column: "KategorijaId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisniciKategorije_KorisnikId",
                table: "KorisniciKategorije",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisniciSlike_KorisnikId",
                table: "KorisniciSlike",
                column: "KorisnikId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KorisniciUloge_KorisnikId",
                table: "KorisniciUloge",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisniciUloge_UlogaId",
                table: "KorisniciUloge",
                column: "UlogaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ponude_EkspertId",
                table: "Ponude",
                column: "EkspertId");

            migrationBuilder.CreateIndex(
                name: "IX_Ponude_ProjektId",
                table: "Ponude",
                column: "ProjektId");

            migrationBuilder.CreateIndex(
                name: "IX_Poslodavci_KorisnikUlogaId",
                table: "Poslodavci",
                column: "KorisnikUlogaId",
                unique: true,
                filter: "[KorisnikUlogaId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProjektDetalji_ProjektId",
                table: "ProjektDetalji",
                column: "ProjektId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjektDetaljiPrilozi_ProjektDetaljiId",
                table: "ProjektDetaljiPrilozi",
                column: "ProjektDetaljiId",
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

            migrationBuilder.CreateIndex(
                name: "IX_RecenzijeOEksperti_EkspertId",
                table: "RecenzijeOEksperti",
                column: "EkspertId");

            migrationBuilder.CreateIndex(
                name: "IX_RecenzijeOEksperti_PoslodavacId",
                table: "RecenzijeOEksperti",
                column: "PoslodavacId");

            migrationBuilder.CreateIndex(
                name: "IX_RecenzijeOEksperti_ProjektId",
                table: "RecenzijeOEksperti",
                column: "ProjektId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecenzijeOPoslodavci_EkspertId",
                table: "RecenzijeOPoslodavci",
                column: "EkspertId");

            migrationBuilder.CreateIndex(
                name: "IX_RecenzijeOPoslodavci_PoslodavacId",
                table: "RecenzijeOPoslodavci",
                column: "PoslodavacId");

            migrationBuilder.CreateIndex(
                name: "IX_RecenzijeOPoslodavci_ProjektId",
                table: "RecenzijeOPoslodavci",
                column: "ProjektId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KorisniciKategorije");

            migrationBuilder.DropTable(
                name: "KorisniciSlike");

            migrationBuilder.DropTable(
                name: "Ponude");

            migrationBuilder.DropTable(
                name: "ProjektDetaljiPrilozi");

            migrationBuilder.DropTable(
                name: "RecenzijeOEksperti");

            migrationBuilder.DropTable(
                name: "RecenzijeOPoslodavci");

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
