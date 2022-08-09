using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Curso
    {
        public long? CursoID { get; set; }
        public string Nome { get; set; }

        //relacionamento 
        public long? DepartamentoID { get; set; }

        public Departamento Departamento { get; set; }
        public virtual ICollection<CursoDisciplina> CursosDisciplinas { get; set; }
    }
}

