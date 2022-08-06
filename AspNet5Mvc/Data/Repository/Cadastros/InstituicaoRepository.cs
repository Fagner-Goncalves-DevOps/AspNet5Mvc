using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet5Mvc.Data.Repository.Cadastros
{
    public class InstituicaoRepository
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


    }
}
