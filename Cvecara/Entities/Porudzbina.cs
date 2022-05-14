using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Entities
{
    public class Porudzbina
    {
		[Key]
		public int porudzbinaID { get; set; }
		public int kolicina { get; set; }
		public decimal cenaPorudzbine { get; set; }
		public DateTime datumPorudzbine { get; set; }
		[ForeignKey ("CvetniAranzman")]
		public int cvetniAranzmanID { get; set; }
		[ForeignKey ("Kupac")]
		public int zaposleniID { get; set; }
		[ForeignKey ("Kupac")]
		public int kupacID { get; set; }
		[ForeignKey ("Lokacija")]
		public int lokacijaID { get; set; }
	}
}
