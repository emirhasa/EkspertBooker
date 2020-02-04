using Microsoft.EntityFrameworkCore.Migrations;

namespace EkspertBooker.WebAPI.Migrations
{
    public partial class m14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Cijena",
                table: "Ponude",
                nullable: true,
                oldClrType: typeof(float),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Cijena",
                table: "Ponude",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
