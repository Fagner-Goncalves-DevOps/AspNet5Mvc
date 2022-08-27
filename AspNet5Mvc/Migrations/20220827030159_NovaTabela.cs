﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNet5Mvc.Migrations
{
    public partial class NovaTabela : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Professores",
                columns: table => new
                {
                    ProfessorID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professores", x => x.ProfessorID);
                });

            migrationBuilder.CreateTable(
                name: "CursoProfessor",
                columns: table => new
                {
                    CursoID = table.Column<long>(type: "bigint", nullable: false),
                    ProfessorID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoProfessor", x => new { x.CursoID, x.ProfessorID });
                    table.ForeignKey(
                        name: "FK_CursoProfessor_Cursos_CursoID",
                        column: x => x.CursoID,
                        principalTable: "Cursos",
                        principalColumn: "CursoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursoProfessor_Professores_ProfessorID",
                        column: x => x.ProfessorID,
                        principalTable: "Professores",
                        principalColumn: "ProfessorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CursoProfessor_ProfessorID",
                table: "CursoProfessor",
                column: "ProfessorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CursoProfessor");

            migrationBuilder.DropTable(
                name: "Professores");
        }
    }
}
