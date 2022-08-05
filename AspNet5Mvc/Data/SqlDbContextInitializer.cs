
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet5Mvc.Data
{
    public class SqlDbContextInitializer
    {
        public static void Initialize(SqlContext context) 
        {
			//deleta o que ten antes de carregar novo
			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();

			if (context.Departamentos.Any())
			{
				return;
			}

			//Seed de carga instituição 
			var instituicoes = new Instituicao[]
			{
				new Instituicao {   Nome="UniParaná",   Endereco="Paraná"},
				new Instituicao {   Nome="UniAcre", Endereco="Acre"}
			};
			foreach (Instituicao i in instituicoes)
			{
				context.Instituicoes.Add(i);
			}
			context.SaveChanges();

			//Seed de carga departamentos 
			var departamentos = new Departamento[]
			{
				new Departamento    {   Nome="Ciência	da	Computação",    InstituicaoID=1},
				new Departamento    {   Nome="Ciência	de	Alimentos",     InstituicaoID=2}
			};
			foreach (Departamento d in departamentos)
			{
				context.Departamentos.Add(d);
			}
			context.SaveChanges();

			//MODELO SIMPLES
			//var departamentos = new Departamento[]
			//{
			//	new Departamento    {   Nome="Ciência	da	Computação"},
			//	new Departamento    {   Nome="Ciência	de	Alimentos"}
			//};
			//foreach (Departamento d in departamentos)
			//{
			//	context.Departamentos.Add(d);
			//}
			//context.SaveChanges();


		}


	}
}
