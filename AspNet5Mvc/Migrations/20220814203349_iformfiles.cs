using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNet5Mvc.Migrations
{
    public partial class iformfiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Foto",
                table: "Academicos",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FotoMimeType",
                table: "Academicos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Academicos");

            migrationBuilder.DropColumn(
                name: "FotoMimeType",
                table: "Academicos");
        }
    }
}
