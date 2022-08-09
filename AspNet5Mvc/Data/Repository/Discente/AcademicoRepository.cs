using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet5Mvc.Data.Repository.Discente
{
    public class AcademicoRepository
    {
		private readonly SqlContext _sqlContext;

		public AcademicoRepository(SqlContext sqlContext)
		{
			_sqlContext = sqlContext;
		}

		public IQueryable<Academico> ObterAcademicosClassificadosPorNome()
		{
			return _sqlContext.Academicos.OrderBy(b => b.Nome);
		}

		public async Task<Academico> ObterAcademicoPorId(long id)
		{
			return await _sqlContext.Academicos.FindAsync(id);
		}

		public async Task<Academico> GravarAcademico(Academico academico)
		{
			if (academico.AcademicoID == null) _sqlContext.Academicos.Add(academico);
			else _sqlContext.Update(academico);
			await _sqlContext.SaveChangesAsync();

			return academico;
		}

		public async Task<Academico> EliminarAcademicoPorId(long id)
		{
			Academico academico = await ObterAcademicoPorId(id);
			_sqlContext.Academicos.Remove(academico);
			await _sqlContext.SaveChangesAsync();

			return academico;
		}














	}
}
