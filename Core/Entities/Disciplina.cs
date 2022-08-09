using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Disciplina
    {
        public long? DisciplinaID { get; set; }
        public string Nome { get; set; }

        //relacionamentos
        public virtual ICollection<CursoDisciplina> CursosDisciplinas {	get; set;}

    }
}
