using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EkspertBooker.WebAPI.Migrations
{
    public partial class m5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PoslodavacKomentar",
                table: "Ponude",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VrijemeOdbijanja",
                table: "Ponude",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VrijemePrihvatanja",
                table: "Ponude",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PoslodavacKomentar",
                table: "Ponude");

            migrationBuilder.DropColumn(
                name: "VrijemeOdbijanja",
                table: "Ponude");

            migrationBuilder.DropColumn(
                name: "VrijemePrihvatanja",
                table: "Ponude");
        }
    }
}
