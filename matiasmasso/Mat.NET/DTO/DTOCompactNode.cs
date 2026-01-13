using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOCompactNode
    {
        public Guid Guid { get; set; }
        public string Nom { get; set; }
        public List<DTOCompactNode> Items { get; set; }
    }
}
