using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet5Mvc.Data.Repository.Docente
{
    public class ProfessorRepository
    {
        private readonly SqlContext _context;

        public ProfessorRepository(SqlContext context) 
        {
            _context = context;
        }
        public IQueryable<Professor> ObterInstituicoesClassificadasPorNome() 
        {
            return _context.Professores.OrderBy(x => x.Nome);
        }
    }
}
