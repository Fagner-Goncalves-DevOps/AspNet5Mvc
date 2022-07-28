using AspNet5Mvc.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet5Mvc.Data
{
    public class SqlContext : DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options) 
        {
        }

        public DbSet<Instituicao> Instituicoes { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        
        //implementação basica inicial para o melhor entendimento
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Departamento>().ToTable("Departamento");

        }

        //pode fazer a configuração direto do dbcontext, normalmente e feito pela startup usando DI.
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        //{
        //    optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Modelo;Trusted_Connection=True;MultipleActiveResultSets=true");
        //}

    }
}
