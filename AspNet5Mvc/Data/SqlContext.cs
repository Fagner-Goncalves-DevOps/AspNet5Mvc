using AspNet5Mvc.Models.Infra;
using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet5Mvc.Data
{
    public class SqlContext : IdentityDbContext<UsuarioDaAplicacao> //DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options) 
        {
        }

        public DbSet<Instituicao> Instituicoes { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Academico> Academicos { get; set; }
        public DbSet<Professor> Professores { get; set; }

        //public DbSet<CursoDisciplina> CursosDisciplinas { get; set; } não precisa passar aqui devido ja estar no Onmodelcreating


        //implementação basica inicial para o melhor entendimento
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CursoDisciplina>().HasKey(cd => new { cd.CursoID, cd.DisciplinaID });
            modelBuilder.Entity<CursoDisciplina>()
                            .HasOne(c => c.Curso)
                            .WithMany(cd => cd.CursosDisciplinas)//CursosDisciplinas
                            .HasForeignKey(c => c.CursoID);
            modelBuilder.Entity<CursoDisciplina>()
                            .HasOne(d => d.Disciplina)
                            .WithMany(cd => cd.CursosDisciplinas)
                            .HasForeignKey(d => d.DisciplinaID);
            // CursoProfessor
            modelBuilder.Entity<CursoProfessor>()
                             .HasKey(cd => new { cd.CursoID, cd.ProfessorID });

            modelBuilder.Entity<CursoProfessor>()
                            .HasOne(c => c.Curso)
                            .WithMany(cd => cd.CursosProfessores)
                            .HasForeignKey(c => c.CursoID);

            modelBuilder.Entity<CursoProfessor>()
                            .HasOne(d => d.Professor)
                            .WithMany(cd => cd.CursosProfessores)
                            .HasForeignKey(d => d.ProfessorID);

        }

        //pode fazer a configuração direto do dbcontext, normalmente e feito pela startup usando DI.
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        //{
        //    optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Modelo;Trusted_Connection=True;MultipleActiveResultSets=true");
        //}

    }
}
