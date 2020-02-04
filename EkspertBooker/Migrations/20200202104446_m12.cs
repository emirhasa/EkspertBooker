using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EkspertBooker.WebAPI.Migrations
{
    public partial class m12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ponude",
                columns: table => new
                {
                    PonudaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EkspertId = table.Column<int>(nullable: false),
                    ProjekatId = table.Column<int>(nullable: false),
                    ProjektId = table.Column<int>(nullable: true),
                    Cijena = table.Column<float>(nullable: true),
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
                        principalColumn: "EkspertId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ponude_Projekti_ProjektId",
                        column: x => x.ProjektId,
                        principalTable: "Projekti",
                        principalColumn: "ProjektId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ponude_EkspertId",
                table: "Ponude",
                column: "EkspertId");

            migrationBuilder.CreateIndex(
                name: "IX_Ponude_ProjektId",
                table: "Ponude",
                column: "ProjektId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ponude");
        }
    }
}
