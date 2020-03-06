using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EkspertBooker.WebAPI.Migrations
{
    public partial class m11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrilogEkstenzija",
                table: "ProjektDetaljiPrilozi",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadVrijeme",
                table: "ProjektDetaljiPrilozi",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrilogEkstenzija",
                table: "ProjektDetaljiPrilozi");

            migrationBuilder.DropColumn(
                name: "UploadVrijeme",
                table: "ProjektDetaljiPrilozi");
        }
    }
}
