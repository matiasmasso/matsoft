using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class IdNom
    {
        public int Id { get; set; }
        public string? Nom { get; set; }

        public override string ToString()
        {
            return Nom ?? "";
        }
    }
}
