using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet5Mvc.Data.Repository.Cadastros
{
	public class DepartamentoRepository//Classe InstituicaoDAL Representação DAL-REPOSITORIO NOSSO
	{
		private readonly SqlContext _sqlContext;

		public DepartamentoRepository(SqlContext sqlContext)
		{
			_sqlContext = sqlContext;
		}

		public IQueryable<Departamento> ObterDepartamentosClassificadosPorNome()
		{
			return _sqlContext.Departamentos
				.Include(i => i.Instituicao)
				.OrderBy(b => b.Nome);
		}
		public async Task<Departamento> ObterDepartamentoPorId(long id)
		{
			var departamento = await _sqlContext.Departamentos.SingleOrDefaultAsync(m => m.DepartamentoID == id);
			_sqlContext.Instituicoes
				.Where(i => departamento.InstituicaoID == i.InstituicaoID)
				.Load(); ;
			return departamento;
		}

		public IQueryable<Departamento> ObterDepartamentoPorInstituicao(long instituicaoID) 
		{
			var departamentos = _sqlContext.Departamentos.Where(d => d.InstituicaoID == instituicaoID).OrderBy(d => d.Nome);
			return departamentos;
		}

		public async Task<Departamento> GravarDepartamento(Departamento departamento)
		{
			if (departamento.DepartamentoID == null) _sqlContext.Departamentos.Add(departamento);
			else _sqlContext.Update(departamento);
			await _sqlContext.SaveChangesAsync();
			return departamento;
		}
		public async Task<Departamento> EliminarDepartamentoPorId(long id)
		{
			Departamento departamento = await ObterDepartamentoPorId(id);
			_sqlContext.Departamentos.Remove(departamento);
			await _sqlContext.SaveChangesAsync();
			return departamento;
		}
	}
}
