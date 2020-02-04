using Microsoft.EntityFrameworkCore.Migrations;

namespace EkspertBooker.WebAPI.Migrations
{
    public partial class m15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BrojZavrsenihProjekata",
                table: "Poslodavci",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "BrojRecenzija",
                table: "Poslodavci",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BrojRecenzija",
                table: "Eksperti",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrojRecenzija",
                table: "Poslodavci");

            migrationBuilder.DropColumn(
                name: "BrojRecenzija",
                table: "Eksperti");

            migrationBuilder.AlterColumn<int>(
                name: "BrojZavrsenihProjekata",
                table: "Poslodavci",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 0);
        }
    }
}
