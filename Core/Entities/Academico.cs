﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Academico
    {
        public long? AcademicoID { get; set; }

		[StringLength(10, MinimumLength = 10)]
		[RegularExpression("([0-9]{10})")]
		[Required]
		public string RegistroAcademico { get; set; }

		[Required]
		public string Nome { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
		[Required]
		public DateTime? Nascimento { get; set; }
	}
}
