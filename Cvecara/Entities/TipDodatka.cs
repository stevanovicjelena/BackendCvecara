using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Entities
{
    public class TipDodatka
    {
        [Key]
        public int tipDodatkaID { get; set; }
        public string nazivTipaDodatka { get; set; }
        public string opisTipaDodatka { get; set; }
    }
}
