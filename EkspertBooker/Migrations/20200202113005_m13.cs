using Microsoft.EntityFrameworkCore.Migrations;

namespace EkspertBooker.WebAPI.Migrations
{
    public partial class m13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjekatId",
                table: "Ponude");

            migrationBuilder.AlterColumn<int>(
                name: "ProjektId",
                table: "Ponude",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OpisPonude",
                table: "Ponude",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpisPonude",
                table: "Ponude");

            migrationBuilder.AlterColumn<int>(
                name: "ProjektId",
                table: "Ponude",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ProjekatId",
                table: "Ponude",
                nullable: false,
                defaultValue: 0);
        }
    }
}
