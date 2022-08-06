using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet5Mvc.Data.Repository.Cadastros
{
    public class InstituicaoRepository //Classe InstituicaoDAL Representação DAL-REPOSITORIO NOSSO
    {
        private readonly SqlContext _sqlContext;

        public InstituicaoRepository(SqlContext sqlContext) 
        {
            _sqlContext = sqlContext;
        }


        public IQueryable<Instituicao> ObterInstituicoesOrderPorNome() 
        {
            return _sqlContext.Instituicoes.OrderBy(c => c.Nome);
        }

        public async Task<Instituicao> ObterInstituicaoPorId(long? id) 
        {
            return await _sqlContext.Instituicoes
                .Include(d => d.Departamentos)
                .SingleOrDefaultAsync(m => m.InstituicaoID == id);
        }

        public async Task<Instituicao>  GravarInstituicao(Instituicao instituicao) 
        {
            if (instituicao.InstituicaoID == null) _sqlContext.Instituicoes.Add(instituicao);
            else _sqlContext.Update(instituicao);
            await _sqlContext.SaveChangesAsync();
            return instituicao;
        }

        public async Task<Instituicao> EliminarInstituicaoPorId(long? id) 
        {
            Instituicao instituicao = await ObterInstituicaoPorId(id);
            _sqlContext.Instituicoes.Remove(instituicao);
            await _sqlContext.SaveChangesAsync();
            return instituicao;
        }


    }
}
