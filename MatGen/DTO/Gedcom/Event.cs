using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Gedcom
{
    public class Event
    {
        public string Type { get; set; }
        public Fch? Fch { get; set; }
        public List<Citation> Citations { get; set; } = new();

        public Event(string type)
        {
            Type= type;
        }

    }
}
