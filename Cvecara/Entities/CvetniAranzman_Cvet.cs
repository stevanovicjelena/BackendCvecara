using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Entities
{
    public class CvetniAranzman_Cvet
    {
        [Key]
        public int cvetniAranzman_Cvet_ID { get; set; }
        [ForeignKey ("CvetniAranzman")]
        public int cvetniAranzmanID { get; set; }
        [NotMapped]
        public virtual CvetniAranzman cvetniAranzman { get; set; }
        [ForeignKey ("Cvet")]
        public int cvetID { get; set; }
        [NotMapped]
        public virtual Cvet cvet { get; set; }
        public int brojCvetova { get; set; }
    }
}
