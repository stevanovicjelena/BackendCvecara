using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Entities
{
    public class Cvet
    {
       [Key]
        public int cvetID { get; set; }
        public string bojaCveta { get; set; }
        public decimal cenaCveta { get; set; }
        public int kolicina { get; set; }
        [ForeignKey("VrstaCveta")]
        public int vrstaCvetaID { get; set; }
        [NotMapped]
        public virtual VrstaCveta vrstaCveta { get; set; }
    }
}
