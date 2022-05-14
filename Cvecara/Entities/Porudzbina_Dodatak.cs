using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Entities
{
    public class Porudzbina_Dodatak
    {
        [Key]
        public int porudzbina_Dodatak_ID { get; set; }
        [ForeignKey ("Porudzbina")]
        public int porudzbinaID { get; set; }
        [ForeignKey ("Dodatak")]
        public int dodatakID { get; set; }
        public int kolicinaDodatka { get; set; }
    }
}
