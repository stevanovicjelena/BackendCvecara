using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Entities
{
	public class CvetniAranzman
	{
		[Key]
		public int cvetniAranzmanID { get; set; }
		public string nazivAranzmana { get; set; }
		public decimal cenaAranzmana { get; set; }
		public string opisAranzmana { get; set; }
		public int kolicina { get; set; }
		[ForeignKey("Pakovanje")]
		public int pakovanjeID { get; set; }
		[NotMapped]
		public virtual Pakovanje pakovanje {get; set;}
	}
}
