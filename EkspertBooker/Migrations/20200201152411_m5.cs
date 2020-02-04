using Microsoft.EntityFrameworkCore.Migrations;

namespace EkspertBooker.WebAPI.Migrations
{
    public partial class m5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "ProsjecnaOcjena",
                table: "Poslodavci",
                nullable: true,
                oldClrType: typeof(float));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "ProsjecnaOcjena",
                table: "Poslodavci",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);
        }
    }
}
