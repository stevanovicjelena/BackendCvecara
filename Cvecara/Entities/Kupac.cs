using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Entities
{
    public class Kupac
    {
		[Key]
		public int kupacID { get; set; }
		public string imeKupca { get; set; }
		public string prezimeKupca { get; set; }
		public string telefon { get; set; }
		public string emailKupca { get; set; }
		public string korisnickoImeKupca { get; set; }
		public string lozinkaKupca { get; set; }
		public string uloga { get; set; }
	}
}
