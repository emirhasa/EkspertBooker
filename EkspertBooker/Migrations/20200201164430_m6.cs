using Microsoft.EntityFrameworkCore.Migrations;

namespace EkspertBooker.WebAPI.Migrations
{
    public partial class m6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "ProsjecnaOcjena",
                table: "Eksperti",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "BrojZavrsenihProjekata",
                table: "Eksperti",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "ProsjecnaOcjena",
                table: "Eksperti",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BrojZavrsenihProjekata",
                table: "Eksperti",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 0);
        }
    }
}
