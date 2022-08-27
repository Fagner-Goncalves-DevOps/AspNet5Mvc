using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Professor
    {
        public long? ProfessorID { get; set; }
        public string Nome { get; set; }

        //relacionamento 
        public virtual ICollection<CursoProfessor> CursosProfessores { get; set; }
    }
}
