﻿// <auto-generated />
using System;
using AspNet5Mvc.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AspNet5Mvc.Migrations
{
    [DbContext(typeof(SqlContext))]
    [Migration("20220807182907_novastabelas")]
    partial class novastabelas
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Core.Entities.Curso", b =>
                {
                    b.Property<long?>("CursoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long?>("DepartamentoID")
                        .HasColumnType("bigint");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CursoID");

                    b.HasIndex("DepartamentoID");

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("Core.Entities.CursoDisciplina", b =>
                {
                    b.Property<long?>("CursoID")
                        .HasColumnType("bigint");

                    b.Property<long?>("DisciplinaID")
                        .HasColumnType("bigint");

                    b.HasKey("CursoID", "DisciplinaID");

                    b.HasIndex("DisciplinaID");

                    b.ToTable("CursoDisciplina");
                });

            modelBuilder.Entity("Core.Entities.Departamento", b =>
                {
                    b.Property<long?>("DepartamentoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long?>("InstituicaoID")
                        .HasColumnType("bigint");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepartamentoID");

                    b.HasIndex("InstituicaoID");

                    b.ToTable("Departamentos");
                });

            modelBuilder.Entity("Core.Entities.Disciplina", b =>
                {
                    b.Property<long?>("DisciplinaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DisciplinaID");

                    b.ToTable("Disciplinas");
                });

            modelBuilder.Entity("Core.Entities.Instituicao", b =>
                {
                    b.Property<long?>("InstituicaoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Endereco")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InstituicaoID");

                    b.ToTable("Instituicoes");
                });

            modelBuilder.Entity("Core.Entities.Curso", b =>
                {
                    b.HasOne("Core.Entities.Departamento", "Departamento")
                        .WithMany("Cursos")
                        .HasForeignKey("DepartamentoID");

                    b.Navigation("Departamento");
                });

            modelBuilder.Entity("Core.Entities.CursoDisciplina", b =>
                {
                    b.HasOne("Core.Entities.Curso", "Curso")
                        .WithMany("CursosDisciplinas")
                        .HasForeignKey("CursoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Disciplina", "Disciplina")
                        .WithMany("CursosDisciplinas")
                        .HasForeignKey("DisciplinaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Curso");

                    b.Navigation("Disciplina");
                });

            modelBuilder.Entity("Core.Entities.Departamento", b =>
                {
                    b.HasOne("Core.Entities.Instituicao", "Instituicao")
                        .WithMany("Departamentos")
                        .HasForeignKey("InstituicaoID");

                    b.Navigation("Instituicao");
                });

            modelBuilder.Entity("Core.Entities.Curso", b =>
                {
                    b.Navigation("CursosDisciplinas");
                });

            modelBuilder.Entity("Core.Entities.Departamento", b =>
                {
                    b.Navigation("Cursos");
                });

            modelBuilder.Entity("Core.Entities.Disciplina", b =>
                {
                    b.Navigation("CursosDisciplinas");
                });

            modelBuilder.Entity("Core.Entities.Instituicao", b =>
                {
                    b.Navigation("Departamentos");
                });
#pragma warning restore 612, 618
        }
    }
}
