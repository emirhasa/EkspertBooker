using Microsoft.EntityFrameworkCore.Migrations;

namespace EkspertBooker.WebAPI.Migrations
{
    public partial class m9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EkspertKategorijaPretplateId",
                table: "EkspertiKategorijePretplate",
                newName: "EkspertKategorijaPretplataId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EkspertKategorijaPretplataId",
                table: "EkspertiKategorijePretplate",
                newName: "EkspertKategorijaPretplateId");
        }
    }
}
