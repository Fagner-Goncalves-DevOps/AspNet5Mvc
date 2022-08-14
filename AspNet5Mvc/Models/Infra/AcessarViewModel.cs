using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet5Mvc.Models.Infra
{
    public class AcessarViewModel
    {
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Senha { get; set; }

		[Display(Name = "Lembrar de mim?")]
		public bool LembrarDeMim { get; set; }
	}
}
