using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EkspertBooker.WebAPI.Migrations
{
    public partial class m2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Komentar = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecenzijeOEksperti", x => x.RecenzijaOEkspertId);
                    table.ForeignKey(
                        name: "FK_RecenzijeOEksperti_Eksperti_EkspertId",
                        column: x => x.EkspertId,
                        principalTable: "Eksperti",
                        principalColumn: "EkspertId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecenzijeOEksperti_Poslodavci_PoslodavacId",
                        column: x => x.PoslodavacId,
                        principalTable: "Poslodavci",
                        principalColumn: "PoslodavacId",
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
                    Komentar = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecenzijeOPoslodavci", x => x.RecenzijaOPoslodavacId);
                    table.ForeignKey(
                        name: "FK_RecenzijeOPoslodavci_Eksperti_EkspertId",
                        column: x => x.EkspertId,
                        principalTable: "Eksperti",
                        principalColumn: "EkspertId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecenzijeOPoslodavci_Poslodavci_PoslodavacId",
                        column: x => x.PoslodavacId,
                        principalTable: "Poslodavci",
                        principalColumn: "PoslodavacId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecenzijeOPoslodavci_Projekti_ProjektId",
                        column: x => x.ProjektId,
                        principalTable: "Projekti",
                        principalColumn: "ProjektId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjektDetaljiPrilozi_ProjektDetaljiId",
                table: "ProjektDetaljiPrilozi",
                column: "ProjektDetaljiId",
                unique: true);

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
                name: "ProjektDetaljiPrilozi");

            migrationBuilder.DropTable(
                name: "RecenzijeOEksperti");

            migrationBuilder.DropTable(
                name: "RecenzijeOPoslodavci");
        }
    }
}
