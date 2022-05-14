using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Entities
{
    public class Zaposleni
    {
		[Key]
		public int zaposleniID { get; set; }
		public string imeZaposlenog { get; set; }
		public string prezimeZaposlenog { get; set; }
		public string kontaktTelefon { get; set; }
		public string emailZaposlenog { get; set; }
		public string korisnickoImeZaposlenog { get; set; }
		public string lozinkaZaposlenog { get; set; }
	}
}
