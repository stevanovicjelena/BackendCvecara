using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Entities
{
    public class User
    {
		[Key]
		public int userID { get; set; }
		public string imeUser { get; set; }
		public string prezimeUser { get; set; }
		public string telefon { get; set; }
		public string emailUser { get; set; }
		public string korisnickoImeUser { get; set; }
		public string lozinkaUser { get; set; }
		public string uloga { get; set; }
	}
}
