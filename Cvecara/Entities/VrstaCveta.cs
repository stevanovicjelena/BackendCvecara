using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Entities
{
    public class VrstaCveta
    {
        [Key]
        public int vrstaCvetaID { get; set; }
        public string nazivVrste { get; set; }
        public string opisVrste { get; set; }
    }
}
