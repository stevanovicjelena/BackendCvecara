using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Entities
{
    public class Lokacije
    {
        [Key]
        public int lokacijaID { get; set; }
        public string nazivLokacije { get; set; }
    }
}
