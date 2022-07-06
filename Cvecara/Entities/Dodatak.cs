using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Entities
{
    public class Dodatak
    {
        [Key]
        public int dodatakID { get; set; }
        public string bojaDodatka { get; set; }
        public decimal cenaDodatka { get; set; }
        public int kolicina { get; set; }
        [ForeignKey ("TipDodatka")]
        public int tipDodatkaID { get; set; }
        [NotMapped]
        public virtual TipDodatka tipDodatka { get; set; }
    }
}
