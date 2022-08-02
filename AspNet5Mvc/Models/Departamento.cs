using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet5Mvc.Models
{
    public class Departamento
    {
        public long? DepartamentoID { get; set; }
        public string Nome { get; set; }

        //1 : N , 1 departamento para muitas instituições
        //relacionamento EF
        public long? InstituicaoID { get; set; }
        public Instituicao Instituicao { get; set; }


    }
}
