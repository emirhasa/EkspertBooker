using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EkspertBooker.WebAPI.Migrations
{
    public partial class m8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KorisniciKategorije");

            migrationBuilder.CreateTable(
                name: "EkspertiKategorijePretplate",
                columns: table => new
                {
                    EkspertKategorijaPretplateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EkspertId = table.Column<int>(nullable: false),
                    KategorijaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EkspertiKategorijePretplate", x => x.EkspertKategorijaPretplateId);
                    table.ForeignKey(
                        name: "FK_EkspertiKategorijePretplate_Eksperti_EkspertId",
                        column: x => x.EkspertId,
                        principalTable: "Eksperti",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EkspertiKategorijePretplate_Kategorije_KategorijaId",
                        column: x => x.KategorijaId,
                        principalTable: "Kategorije",
                        principalColumn: "KategorijaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EkspertiKategorijePretplate_EkspertId",
                table: "EkspertiKategorijePretplate",
                column: "EkspertId");

            migrationBuilder.CreateIndex(
                name: "IX_EkspertiKategorijePretplate_KategorijaId",
                table: "EkspertiKategorijePretplate",
                column: "KategorijaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EkspertiKategorijePretplate");

            migrationBuilder.CreateTable(
                name: "KorisniciKategorije",
                columns: table => new
                {
                    KorisnikKategorijaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KategorijaId = table.Column<int>(nullable: false),
                    KorisnikId = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_KorisniciKategorije_KategorijaId",
                table: "KorisniciKategorije",
                column: "KategorijaId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisniciKategorije_KorisnikId",
                table: "KorisniciKategorije",
                column: "KorisnikId");
        }
    }
}
