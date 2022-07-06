using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Entities
{
    public class Pakovanje
    {
		[Key]
		public int pakovanjeID { get; set; }
		public string nazivPakovanja { get; set; }
		public string bojaPakovanja { get; set; }
		public string opisPakovanja { get; set; }
		public decimal cenaPakovanja { get; set; }
		public int kolicina { get; set; }
	}
}
