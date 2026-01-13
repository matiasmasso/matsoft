using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Gedcom
{
    public class Fch
    {
        public DateTime Value { get; set; }
        public string? DisplayValue { get; set; }
        public Modifiers Modifier { get; set; } = Modifiers.exact;

        public string? Location { get; set; }

        public List<Citation> Citations { get; set; } = new();

        public enum Modifiers
        {
            exact,
            approximate
        }


    }
}
