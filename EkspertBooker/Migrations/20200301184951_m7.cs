using Microsoft.EntityFrameworkCore.Migrations;

namespace EkspertBooker.WebAPI.Migrations
{
    public partial class m7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EkspertStrucnaKategorijaId",
                table: "Eksperti",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Eksperti_EkspertStrucnaKategorijaId",
                table: "Eksperti",
                column: "EkspertStrucnaKategorijaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Eksperti_Kategorije_EkspertStrucnaKategorijaId",
                table: "Eksperti",
                column: "EkspertStrucnaKategorijaId",
                principalTable: "Kategorije",
                principalColumn: "KategorijaId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Eksperti_Kategorije_EkspertStrucnaKategorijaId",
                table: "Eksperti");

            migrationBuilder.DropIndex(
                name: "IX_Eksperti_EkspertStrucnaKategorijaId",
                table: "Eksperti");

            migrationBuilder.DropColumn(
                name: "EkspertStrucnaKategorijaId",
                table: "Eksperti");
        }
    }
}
