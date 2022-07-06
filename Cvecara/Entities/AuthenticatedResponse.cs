using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Entities
{
    public class AuthenticatedResponse
    {
        public string? Token { get; set; }
        public int? id { get; set; }
    }
}
